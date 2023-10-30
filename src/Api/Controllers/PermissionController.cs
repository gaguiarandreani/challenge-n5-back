using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecurityApi.Application.UseCase.PermissionOperation;
using System.Threading.Tasks;

namespace SecurityApi.Controllers;

[Route("api/permissions")]
public class PermissionController : ApiControllerBase
{
    public PermissionController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand request) => PerformAction(request);

    [HttpPut]
    public Task<IActionResult> ModifyPermission([FromBody] ModifyPermissionCommand request) => PerformAction(request);

    [HttpGet]
    public Task<IActionResult> GetPermissions() => PerformAction(new GetPermissionsQuery());
}