using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAM2Zaliczenie.DAL;
using PAM2Zaliczenie.Models;
using PAM2Zaliczenie.BLL;
using System.Security.Claims;
using ExchangeRate;
using System.Globalization;

namespace PAM2Zaliczenie.Controllers
{
    public class TasksController : Controller
    {
        private readonly PAM_KillersDBContext _context;
       
        public TasksController(PAM_KillersDBContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
             
            if (User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value == 0.ToString())
            {

                return View(await _context.Tasks.Include(t => t.Employee).Include(t => t.TaskType).Include(t => t.User).ToListAsync());
            }
            else
            {
                Guid UserID = new Guid(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                return View(await _context.Tasks.Include(t => t.Employee).Include(t => t.TaskType).Include(t => t.User)
                    .Where(row => row.UserId == UserID)
                    .ToListAsync());
            }
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.TaskType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ///hardcoded USD!!!
            string currency = "USD";
            CurrencyChecker currencyChecker = new CurrencyChecker();
            string currencyValue = currencyChecker.ReturnData(currencyChecker.GetData(currency));
            CultureInfo culture = new CultureInfo("en-US");
            decimal tempCurrencyValue = Convert.ToDecimal(currencyValue, culture);
            ViewData["EmployeeId"] = new SelectList(_context.Employee.Select(row => new ObjectHelper()
            {
                EmpGuid = row.Id,
                EmpNamSur = row.Name + " " + row.Surname
            }).ToList(), "EmpGuid", "EmpNamSur");
          
            ViewData["TaskTypeID"] = new SelectList(_context.TaskType, "Id", "Name");
            ViewData["TaskInfo"] = _context.TaskType.Select(row => new ObjectHelper()
            {
                TaskGuid = row.Id.ToString(),
                ValueInCurr = $"{(row.Cost / tempCurrencyValue).ToString("0.00")} {currency}",
                TaskCostInfo = $"Work will be cost {row.Cost.ToString()} in PLN and {(row.Cost / tempCurrencyValue).ToString("0.00")} in {currency}"
            }).ToList<ObjectHelper>(); 
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskTypeId,EmployeeId,UserId,IsReady,StartTime")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.Id = Guid.NewGuid();
                //var asdf = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                tasks.UserId = new Guid(User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", tasks.EmployeeId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Name", tasks.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Login", tasks.UserId);
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", tasks.EmployeeId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Comment", tasks.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Login", tasks.UserId);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TaskTypeId,EmployeeId,UserId,IsReady,StartTime")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Name", tasks.EmployeeId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Comment", tasks.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Login", tasks.UserId);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.TaskType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(Guid id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
