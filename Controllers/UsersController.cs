using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAM2Zaliczenie.DAL;
using PAM2Zaliczenie.Models;

namespace PAM2Zaliczenie.Controllers
{
    public class UsersController : Controller
    {
        private readonly PAM_KillersDBContext _context;

        public UsersController(PAM_KillersDBContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "0, 1")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,EmailAddress,UserAccessLevel,Password")] Users users)
        {
            users.UserAccessLevel = 2;
            if (ModelState.IsValid)
            {
                users.Id = Guid.NewGuid();
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,EmailAddress,UserAccessLevel,Password")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: Users/SelfEdit/5
        public async Task<IActionResult> SelfEdit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/SelfEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelfEdit(Guid id, [Bind("Id,Login,EmailAddress,UserAccessLevel,Password")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Debug.WriteLine("Model state valid");
                try
                {
                    Debug.WriteLine("Przed zapisem do bazy");

                    _context.Update(users);
                    await _context.SaveChangesAsync();
                    Debug.WriteLine("Zapisano do bazy");
                }
                catch (DbUpdateConcurrencyException)
                {
                    Debug.WriteLine("DbUpdateConcurency");
                    if (!UsersExists(users.Id))
                    {
                        Debug.WriteLine("!UserExists");
                        return NotFound();
                    }
                    else
                    {
                        Debug.WriteLine("Else");
                        throw;
                    }
                }
                Debug.WriteLine("Redirect");
                return RedirectToAction(nameof(SelfDetails), new { users.Id });
            }
            Debug.WriteLine("return user");
            return View(users);
        }
        // GET: Users/SelfDetails/5
        public async Task<IActionResult> SelfDetails(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }
    }
}
