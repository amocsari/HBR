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
        private readonly IHbrDbContext _hbrDbContext;
        public AccountController(IHbrDbContext hbrDbContext)
        {
            _hbrDbContext = hbrDbContext;
        }

        [HttpPost]
        public async Task<string> LoginUser(LoginRequest request)
        {
            var user = await _hbrDbContext.User.FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password)
                ?? throw new HbrException("Sikertelen bejelentkezes");

            return user.UserId;
        }
    }
}
#endregion