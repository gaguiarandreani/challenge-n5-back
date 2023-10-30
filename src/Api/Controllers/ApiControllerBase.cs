using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityApi.Domain.Responses;
using System.Threading.Tasks;

namespace SecurityApi.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> PerformAction<TRequest>(TRequest request) where TRequest : IBaseRequest
    {
        var response = await _mediator.Send(request) as IAppResponseBase;

        if (response.Success)
        {
            return Ok(response);
        }

        return StatusCode(response.StatusCode ?? StatusCodes.Status500InternalServerError, response);
    }
}