using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected string UserId => User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
    }
}
