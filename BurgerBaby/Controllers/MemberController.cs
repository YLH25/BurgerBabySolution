using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerBaby.Models.EFModel;
using Microsoft.AspNetCore.Authorization;
using BurgerBaby.Models.Services;
using BurgerBaby.Models.Services.Interface;
using BurgerBaby.Models.Repo.Interface;
using BurgerBaby.Models.ViewModel;

namespace BurgerBaby.Controllers
{
    [Authorize(Policy ="Administer" )]

    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMemberRepository _memberRepository;
        private readonly IRoleRepository _roleRepository;

        public MemberController(BurgerBabyContext context, IMemberService memberService, IMemberRepository memberRepository, IRoleRepository roleRepository)
        {
            _memberService = memberService;
            _memberRepository = memberRepository;
            _roleRepository = roleRepository;
        }
        public async Task<IActionResult> Index(string? searchString, int? pageIndex, int? pageSize)
        {
            try
            {
                var pageReult = await _memberService.GetPageResultVMAsync(searchString, pageIndex, pageSize);
                if (pageReult.Items == null)
                {
                    return View();
                }
                return View(pageReult);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }
        // GET: Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    var errMessege = "沒有這筆資料";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var member = await _memberRepository.GetMemberByIdAsync((int)id);
                if (member == null)
                {
                    var errMessege = "沒有這筆資料";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var memberVM = member.ToVM();
                return View(memberVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Member/Create
        public async Task<IActionResult> Create(int? roleId)
        {
            try
            {
                var roles = await _roleRepository.GetRolesAsync();
                ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName", roleId);
                return View();
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,Phone,Address,Email,Password,Name")] MemberCreateVM memberCreateVM)
        {
            try
            {
                var roles = await _roleRepository.GetRolesAsync();

                if (ModelState.IsValid)
                {
                    var existMember = await _memberRepository.GetMemberByEmailAsync(memberCreateVM.Email);
                    if (existMember != null) {
                        ModelState.AddModelError("Email", "這個Email已經註冊過了");
                    }
                    else
                    {
                        await _memberService.CreateMemberAsync(memberCreateVM.ToEntity().ToDto());
                        return RedirectToAction(nameof(Index));
                    }
                }
                 ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName", memberCreateVM.ToEntity().RoleId);
                return View(memberCreateVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var members = await _memberRepository.GetMembersAsync();
                if (id == null || members == null)
                {
                    var errMessege = "沒有這個會員";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }

                var member = await _memberRepository.GetMemberByIdAsync((int)id);
                if (member == null)
                {
                    var errMessege = "沒有這個會員";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var roles = await _roleRepository.GetRolesAsync();
                ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName", member.RoleId);
                return View(member.ToEditVM());
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,Phone,Address,Email,Password,Name")] MemberEditVM memberEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var member = await _memberRepository.GetMemberByIdAsync(memberEditVM.Id);
                    if (member == null)
                    {
                        var errMessege = "沒有這個會員";
                        return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                    }
                    await _memberService.EditMemberAsync(memberEditVM.ToEntity().ToDto());
                    return RedirectToAction(nameof(Index));
                }
                ViewData["RoleId"] = new SelectList(await _roleRepository.GetRolesAsync(), "Id", "RoleName", memberEditVM.RoleId);
                return View(memberEditVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var member = await _memberRepository.GetMemberByIdAsync(id);
                if (member == null)
                {
                    var errMessege = "沒有這個會員";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }

                return View(member.ToDeleteVM());
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var member = await _memberRepository.GetMemberByIdAsync(id);
                if (member != null)
                {
                    await _memberService.DeleteMemberAsync(member.ToDto());
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
