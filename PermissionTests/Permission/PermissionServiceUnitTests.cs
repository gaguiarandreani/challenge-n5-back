using AutoMapper;
using Interfaces.Producers;
using Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nest;
using SecurityApi.Application.Mappings;
using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Requests;
using SecurityApi.Infrastructure.Services;
using SecurityApi.Interfaces.Services;

namespace PermissionTests.Permission
{
    public class PermissionServiceUnitTests
    {
        [Fact]
        public async void GetPermissions_HappyPathWorks()
        {
            var ITEM_ID = 1;

            var elastic = new Mock<IElasticClient>();
            var repository = new Mock<IPermissionRepository>();
            var producer = new Mock<IPermissionProducer>();

            repository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new List<SecurityApi.Domain.Entities.Permission>
            {
                new SecurityApi.Domain.Entities.Permission
                {
                    EmployeeLastName = "LastName",
                    EmployeeName = "FirstName",
                    PermissionTypeId = 2,
                    Id = ITEM_ID
                }
            }.AsEnumerable()));

            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(CommonMappingsProfile));
            services.AddSingleton(elastic.Object);
            services.AddSingleton(repository.Object);
            services.AddSingleton(producer.Object);
            services.AddTransient<IPermissionService, PermissionService>();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IPermissionService>();

            var response = await service.GetPermissions(new PermissionRequest());

            Assert.True(response.Success);
            Assert.Single(response.Data);
            Assert.Equal(ITEM_ID, response.Data.First().Id);
        }

        [Fact]
        public async void RequestPermission_HappyPathWorks()
        {
            var services = new ServiceCollection();

            var elastic = new Mock<IElasticClient>();
            var repository = new Mock<IPermissionRepository>();
            var producer = new Mock<IPermissionProducer>();

            services.AddAutoMapper(typeof(CommonMappingsProfile));

            var ITEM_ID = 1;

            var permissionItem = new SecurityApi.Domain.Entities.Permission
            {
                EmployeeLastName = "LastName",
                EmployeeName = "FirstName",
                PermissionTypeId = 2,
                Id = ITEM_ID
            };

            var permissionDto = new PermissionDto
            {
                EmployeeLastName = "LastName",
                EmployeeName = "FirstName",
                PermissionTypeId = 2,
                Id = ITEM_ID
            };

            repository.Setup(x => x.CreateAsync(It.Is<SecurityApi.Domain.Entities.Permission>(x => x.Id == ITEM_ID)))
                .Returns(Task.FromResult(permissionItem))
                .Verifiable(Times.Once);

            services.AddSingleton(elastic.Object);
            services.AddSingleton(repository.Object);
            services.AddSingleton(producer.Object);

            services.AddTransient<IPermissionService, PermissionService>();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IPermissionService>();

            var response = await service.RequestPermission(new PermissionRequest { Permission = permissionDto });

            Assert.True(response.Success);
            Assert.Equal(ITEM_ID, (response.Data as SecurityApi.Domain.Entities.Permission).Id);
        }

        [Fact]
        public async void ModifyPermission_HappyPathWorks()
        {
            var services = new ServiceCollection();

            var elastic = new Mock<IElasticClient>();
            var repository = new Mock<IPermissionRepository>();
            var producer = new Mock<IPermissionProducer>();

            services.AddAutoMapper(typeof(CommonMappingsProfile));

            var ITEM_ID = 1;

            var permissionItem = new SecurityApi.Domain.Entities.Permission
            {
                EmployeeLastName = "LastName",
                EmployeeName = "FirstName",
                PermissionTypeId = 2,
                Id = ITEM_ID
            };

            var permissionDto = new PermissionDto
            {
                EmployeeLastName = "LastName",
                EmployeeName = "FirstName",
                PermissionTypeId = 2,
                Id = ITEM_ID
            };

            repository.Setup(x => x.ModifyAsync(It.Is<SecurityApi.Domain.Entities.Permission>(x => x.Id == ITEM_ID)))
                .Returns(Task.FromResult(permissionItem))
                .Verifiable(Times.Once);

            services.AddSingleton(elastic.Object);
            services.AddSingleton(repository.Object);
            services.AddSingleton(producer.Object);

            services.AddTransient<IPermissionService, PermissionService>();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IPermissionService>();

            var response = await service.ModifyPermission(new PermissionRequest { Permission = permissionDto });

            Assert.True(response.Success);
            Assert.Equal(ITEM_ID, (response.Data as SecurityApi.Domain.Entities.Permission).Id);
        }
    }
}