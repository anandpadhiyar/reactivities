using Application.Photos;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotoController : BaseApiController<PhotoController>
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Add.Command command)
        {
            return HandleResult<Photo>(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult<Unit>(await Mediator.Send(new Delete.Command { publicId = id }));
        }

        [HttpPost("{id}/setmain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult<Unit>(await Mediator.Send(new SetMain.Command { publicId = id }));
        }

    }
}