using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers
{
	public class AccountController : Controller
	{
		private readonly AppDbContext db;

		public AccountController(AppDbContext db)
		{
			this.db = db;
		}

		public async Task<IActionResult> Index(int delete = 0, int edit = 0)
		{
			if(delete != 0)
			{
				var account = await db.Accounts.FindAsync(delete);
				if(account != null)
				{
					db.Accounts.Remove(account);
					await db.SaveChangesAsync();
				}
			}
			else if (edit != 0)
			{
				var user = await db.Accounts.FindAsync(edit);
				if (user == null)
					return NotFound();
				return View(user);
			}
			return View(await db.Accounts.ToListAsync());
		}

		[HttpPost]
		public async Task<IActionResult> Index([FromForm] Account account)
		{
			if (account.IDAccount != 0)
			{
				var edit = await db.Accounts.FindAsync(account.IDAccount);
				if (edit == null)
                {
                    return NotFound();
                }
					
                edit.Nickname = account.Nickname;
                edit.Email = account.Email;
                edit.Hash = account.Hash;
				edit.Salt = account.Salt;
                edit.Role = account.Role;
				await db.SaveChangesAsync();
			}
			else
			{
				await db.Accounts.AddAsync(account);
				await db.SaveChangesAsync();
			}
			return LocalRedirect("/");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}