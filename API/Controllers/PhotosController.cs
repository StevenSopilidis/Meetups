using System.Threading.Tasks;
using Application.Photos;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosController: BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromForm]IFormFile File)
        {
            return HandleResult<Photo>(await Mediator.Send(new Add.Command{File= File}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id=id}));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command{Id=id}));
        }
    }
}