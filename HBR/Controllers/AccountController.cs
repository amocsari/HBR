#region tmp ki lesz veve
using BLL;
using Common.Request;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HBR.Controllers
{
    public class AccountController: BaseController
    {
        private readonly IHbrDbContext _context;
        public AccountController(IHbrDbContext hbrDbContext)
        {
            _context = hbrDbContext;
        }

        [HttpPost]
        public async Task<string> LoginUser([FromBody]LoginRequest request)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password)
                ?? throw new HbrException("Sikertelen bejelentkezes");

            return user.UserId;
        }
    }
}
#endregion