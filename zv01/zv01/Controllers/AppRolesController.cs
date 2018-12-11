using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zv01.Data;
using zv01.Models;

namespace zv01.Controllers
{
    public class AppRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AppRolesController(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            

        }
       
        public async Task<IActionResult> SearchUser(string searchString, int role)
        {

            AppUser user = await _userManager.FindByEmailAsync(searchString);


            if (user != null)
            {
                if (role == 1)
                {

                    await _userManager.AddToRoleAsync(user, "Administrador");
                    await _context.SaveChangesAsync();

                }
                else if (role == 2)
                {
                    await _userManager.AddToRoleAsync(user, "Pica");
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToAction("Create", "Eventos");
                //View(await user.ToListAsync());
            }

        }

        public async Task<IActionResult> DeleteUser(string searchString)
        {
            AppUser user = await _userManager.FindByEmailAsync(searchString);

            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrador");
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Reservas");
                //Create();

            }
            else
            {
                return RedirectToAction("Create", "Eventos");
                //View(await user.ToListAsync());
            }
        }

        // GET: AppRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppRole.ToListAsync());
        }

        // GET: AppRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.AppRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // GET: AppRoles/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AdminPnl()
        {
            return View();
        }
        // POST: AppRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,CreationDate,Id,Name,NormalizedName,ConcurrencyStamp")] AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: AppRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.AppRole.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }
            return View(appRole);
        }

        // POST: AppRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,CreationDate,Id,Name,NormalizedName,ConcurrencyStamp")] AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRoleExists(appRole.Id))
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
            return View(appRole);
        }
        //
        // GET: AppRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.AppRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }
        
        // POST: AppRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appRole = await _context.AppRole.FindAsync(id);
            _context.AppRole.Remove(appRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppRoleExists(string id)
        {
            return _context.AppRole.Any(e => e.Id == id);
        }
    }
}
