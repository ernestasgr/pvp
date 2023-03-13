using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using test.Models;

namespace test.Controllers
{
	public class ProfileController : Controller
    {
        private readonly AppDbContext db;

		public ProfileController(AppDbContext db)
		{
			this.db = db;
		}


        public async Task<ActionResult> Index()
        {
            var account = await db.Accounts.OrderBy(user => user.Nickname).FirstOrDefaultAsync();

            if(account == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var roleColumn = db.Roles.FirstOrDefault(r => r.Id == account.Role);

            var model = new AccountViewModel
            {
                Email = account.Email,
                Hash = account.Hash,
                Salt = account.Salt,
                Nickname = account.Nickname,
                RoleName = roleColumn.Role
            };

            return View(model);
        }
    }
}