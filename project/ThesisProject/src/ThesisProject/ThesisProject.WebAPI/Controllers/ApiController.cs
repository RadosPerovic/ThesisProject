using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ThesisProject.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected IMediator _mediator { get; private set; }
    public ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
