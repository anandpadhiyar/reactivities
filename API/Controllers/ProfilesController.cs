using Application.UsrProfile;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController<ProfilesController>
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            return HandleResult<UserProfile>(await Mediator.Send(new Details.Query { Username = username }));
        }
    }
}