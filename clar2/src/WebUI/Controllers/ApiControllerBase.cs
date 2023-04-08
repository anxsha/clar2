using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clar2.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase {
  private ISender? _mediator;

  protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}