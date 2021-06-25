using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BaseController: ControllerBase
    {
        private IMediator _mediator;
        
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        //func to handle results from application layer        
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null)
                return NotFound();
            if(result.IsUnautorized == true)
                return Forbid();
            if(result.IsSuccessfull == true && result.Value != null)
                return Ok(result.Value);
            if(result.IsSuccessfull == true && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }

    }
}