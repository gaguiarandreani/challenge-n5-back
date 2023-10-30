using AutoMapper;
using Interfaces.Producers;
using Interfaces.Repositories;
using Nest;
using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Entities;
using SecurityApi.Domain.Requests;
using SecurityApi.Domain.Responses;
using SecurityApi.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IElasticClient _elasticClient;
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPermissionProducer _producer;

        public PermissionService(IElasticClient elasticClient, IPermissionRepository repository, IMapper mapper, IPermissionProducer producer)
        {
            _elasticClient = elasticClient;
            _repository = repository;
            _mapper = mapper;
            _producer = producer;
        }

        public async Task<IAppResponseBase<List<PermissionDto>>> GetPermissions(PermissionRequest request)
        {
            try
            {
                var result = (await _repository.GetAllAsync()).ToList();

                return new PermissionResponse
                {
                    Data = _mapper.Map<List<PermissionDto>>(result),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new PermissionResponse { Success = false };
            }
            finally
            {
                await SendToElasticAndKafka("Get");
            }
        }

        public async Task<AppResponseBase> ModifyPermission(PermissionRequest request)
        {
            try
            {
                var entity = _mapper.Map<Permission>(request.Permission);

                await _repository.ModifyAsync(entity);

                return new AppResponseBase { Success = true, Data = entity };
            }
            catch (Exception ex)
            {
                return new AppResponseBase { Success = false };
            }
            finally
            {
                await SendToElasticAndKafka("Modify", request.Permission);
            }
        }

        public async Task<AppResponseBase> RequestPermission(PermissionRequest request)
        {
            try
            {
                var entity = _mapper.Map<Permission>(request.Permission);

                var entityCreated = await _repository.CreateAsync(entity);

                return new AppResponseBase { Success = true, Data = entityCreated };
            }
            catch (Exception ex)
            {
                return new AppResponseBase { Success = false };
            }
            finally
            {
                await SendToElasticAndKafka("Request", request.Permission);
            }
        }

        private async Task SendToElasticAndKafka(string action, PermissionDto permission = null)
        {
            try
            {
                await _producer.ProduceAsync(new PermissionProducerRegistry { OperationName = action });
            }
            catch (Exception ex)
            {
            }

            try
            {
                await _elasticClient.IndexDocumentAsync(new PermissionElasticDoc { Action = action, Permission = permission });
            }
            catch (Exception ex)
            {
            }
        }
    }
}