using API.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {
        private IMediator _mediator;

        //if _mediator is null get it via the HttpContext
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        //method for handilg request results
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null )
                return NotFound();
            if(result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if(result.IsSuccess && result.Value == null)
                return NotFound(result);
            return BadRequest(result.Error);
        }
        
        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            if(result == null )
                return NotFound();
            if(result.IsSuccess && result.Value != null)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize, result.Value.TotalCount,
                result.Value.TotalPages);
                return Ok(result.Value);
            }
            if(result.IsSuccess && result.Value == null)
                return NotFound(result);
            return BadRequest(result.Error);
        }
    }
}