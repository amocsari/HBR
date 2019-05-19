using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBR.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected string UserId => User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
    }
}
