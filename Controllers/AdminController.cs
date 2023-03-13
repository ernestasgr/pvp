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

        [Route("Admin/Users")]
        [HttpGet]
        public async Task<IActionResult> Users(string searchId)
        {
            var users = _context.Accounts.ToList();
            if (!string.IsNullOrEmpty(searchId))
            {
                users = users.Where(u => u.IDAccount.ToString().Contains(searchId)).ToList();
            }

            return View("Users/Index", users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser() 
        {
            return View("Users/CreateUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/CreateUser")]
        public IActionResult CreateUser(Account user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Accounts.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Users", "Admin");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating user: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id) 
        {
            var account = await _context.Accounts.FindAsync(id);
            return View("Users/UpdateUser", account);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(Account updatedAccount)
        {
            var existingAccount = await _context.Accounts.FindAsync(updatedAccount.IDAccount);

            if (existingAccount != null)
            {
                existingAccount.Nickname = updatedAccount.Nickname;
                existingAccount.Email = updatedAccount.Email;
                existingAccount.Hash = updatedAccount.Hash;
                existingAccount.Salt = updatedAccount.Salt;
                existingAccount.Role = updatedAccount.Role;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View("Users/DeleteUser", account);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction("Users", "Admin");
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