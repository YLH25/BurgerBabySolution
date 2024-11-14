using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Services.Interface;
using BurgerBaby.Models.Repo.Interface;
using BurgerBaby.Models.Services;
using System.Drawing.Printing;
using BurgerBaby.Models.ViewModel;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.AspNetCore.Authorization;

namespace BurgerBaby.Controllers
{
    [Authorize(Policy ="Administer")]
    public class RoleController : Controller
    {
        private readonly BurgerBabyContext _context;
        private readonly IRoleService _roleService;
        private readonly IRoleRepository _roleRepository;

        public RoleController(BurgerBabyContext context,IRoleService roleService,IRoleRepository roleRepository)
        {
            _context = context;
            _roleService = roleService;
            _roleRepository = roleRepository;
        }

        // GET: Role
        public async Task<IActionResult> Index(string? searchString,int? pageIndex,int? pageSize)
        {
            try
            {
                var pageReult = await _roleService.GetPageResultVMAsync(searchString, pageIndex, pageSize);
                if (pageReult.Items == null)
                {
                    return View();
                }
                return View(pageReult);

            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    var errMessege = "沒有這規則";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var role = await _roleRepository.GetRoleByIdAsync((int)id);
                if (role == null)
                {
                    var errMessege = "沒有這規則";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var roleVM = role.ToVM();
                return View(roleVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName")] RoleCreateVM roleCreateVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _roleService.CreateRoleAsync(roleCreateVM.ToEntity().ToDto());
                    return RedirectToAction(nameof(Index));
                }
                return View(roleCreateVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var roles = await _roleRepository.GetRolesAsync();
                if (id == null || roles == null)
                {
                    var errMessege = "沒有這個規則";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var member = await _roleRepository.GetRoleByIdAsync((int)id);
                if (member == null)
                {
                    var errMessege = "沒有這個規則";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                return View(member.ToEditVM());
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleName")] RoleEditVM roleEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await _roleRepository.GetRoleByIdAsync(roleEditVM.Id);
                    if (role == null)
                    {
                        var errMessege = "沒有這個規則";
                        return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                    }
                    await _roleService.EditRoleAsync(roleEditVM.ToEntity().ToDto());
                    return RedirectToAction(nameof(Index));
                }
               
                return View(roleEditVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var role = await _roleRepository.GetRoleByIdAsync(id);
                if (role == null)
                {
                    var errMessege = "沒有這個規則";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }

                return View(role.ToDeleteVM());
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var role = await _roleRepository.GetRoleByIdAsync(id);
                if (role != null)
                {
                    await _roleService.DeleteRoleAsync(role.ToDto());
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }
    }
}
