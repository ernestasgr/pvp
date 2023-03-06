using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;
using Microsoft.AspNetCore.Http;

namespace test.Controllers
{
	public class AdminController : Controller
    {

        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Users(string searchId)
        {
            var users = _context.Accounts.ToList();
            if (!string.IsNullOrEmpty(searchId))
            {
                users = users.Where(u => u.IDAccount.ToString().Contains(searchId)).ToList();
            }

            return View(users);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProgrammingTasks()
        {
            return View();
        }
    }
}