using System;
using System.Threading.Tasks;
using Application.Retweets;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RetweetController : BaseController
    {
        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateRetweet(Guid postId)
        {
            return HandleResult(await Mediator.Send(new Create.Command{PostId= postId}));
        }
    }
}