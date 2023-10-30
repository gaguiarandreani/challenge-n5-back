using AutoMapper;
using Interfaces.Producers;
using Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nest;
using SecurityApi.Application.Mappings;
using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Requests;
using SecurityApi.Infrastructure.Persistence;
using SecurityApi.Infrastructure.Persistence.Repositories;
using SecurityApi.Infrastructure.Services;
using SecurityApi.Interfaces.Services;
using SecurityApi.Middlewares;
using System.Reflection;

namespace PermissionTests.Permission
{
    public class PermissionServiceIntegrationTests 
    {
        [Fact]
        public async void PermissionsIntegration_HappyPathWorks()
        {
            var elastic = new Mock<IElasticClient>();
            var producer = new Mock<IPermissionProducer>();
            var config = new Mock<IConfiguration>();

            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(CommonMappingsProfile));
            services.AddSingleton(elastic.Object);
            services.AddSingleton(producer.Object);
            services.AddScoped<AppDbContext>();
            services.AddTransient<AppDbContextOptionsBuilder>();
            services.AddSingleton(config.Object);
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPermissionService, PermissionService>();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IPermissionService>();

            var createResponse = await service.RequestPermission(new PermissionRequest
            {
                Permission = new PermissionDto
                {
                    EmployeeLastName = "Aguiar",
                    EmployeeName = "Gustavo",
                    PermissionTypeId = 1
                }
            });

            Assert.True(createResponse.Success);

            const string MODIFIED_LAST_NAME = "Perez";
            const string MODIFIED_NAME = "Roberto";
            const int MODIFIED_PERMISSION_TYPE = 2;

            var modifyResponse = await service.ModifyPermission(new PermissionRequest
            {
                Permission = new PermissionDto
                {
                    EmployeeLastName = MODIFIED_LAST_NAME,
                    EmployeeName = MODIFIED_NAME,
                    PermissionTypeId = MODIFIED_PERMISSION_TYPE,
                    Id = createResponse.Data.Id
                }
            });

            Assert.True(modifyResponse.Success);

            var getResponse = await service.GetPermissions(new PermissionRequest());

            var found = getResponse.Data.FirstOrDefault(x => x.Id == createResponse.Data.Id);

            Assert.True(getResponse.Success);
            Assert.True(found != null);
            Assert.Equal(MODIFIED_LAST_NAME, found?.EmployeeLastName);
            Assert.Equal(MODIFIED_NAME, found?.EmployeeName);
            Assert.Equal(MODIFIED_PERMISSION_TYPE, found?.PermissionTypeId);
        }
    }
}