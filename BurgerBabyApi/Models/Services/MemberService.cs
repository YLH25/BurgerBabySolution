using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using BurgerBabyApi.Models.Repo.Interface;
using BurgerBabyApi.Models.Services.Interface;
using BurgerBabyApi.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace BurgerBabyApi.Models.Services
{
    public class MemberService : IMemberService
    {
        private IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;

        }
        public async Task<PageResultVM<MemberVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize) {
            if (pageIndex == null || pageSize == null)
            {
                pageIndex = 1;
                pageSize = 10;
            }

            var pageResult = await GetMembersBysearchStringAsync(searchString, (int)pageIndex.Value, (int)pageSize.Value);

            if (pageResult.Items == null)
            {
                return new PageResultVM<MemberVM>();
            }

            var ps = new PageService();

            var pagesStringList = ps.GetPagesStringList((int)pageIndex.Value, pageResult.TotalPages);
            var pageResultVM = new PageResultVM<MemberVM>
            {
                Items = pageResult.Items.Select(x => new MemberVM
                {
                    Id = x.Id,
                  Name = x.Name,
                    Phone = x.Phone,
                    Address=x.Address,
                    Email=x.Email,
                    Password=x.Password,
                    RoleId=x.RoleId,
                    Role=x.Role,
                }).OrderByDescending(x => x.Id).ToList(),
                SearchString = searchString,
                TotalPages = pageResult.TotalPages,
                PageSize = pageResult.PageSize,
                PageIndex = pageResult.PageIndex,
                PagesStringList = pagesStringList,
                SearchFliterOption = "Member"
            };
            return pageResultVM;
        }
        public async Task<PageResult<Member>> GetMembersBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            var members = await _memberRepository.GetMembersBysearchStringAsync(searchString, pageIndex, pageSize);
            return members;
        }
        public async Task CreateMemberAsync(MemberDTO memberDTO) {

            await _memberRepository.CreateMemberAsync(memberDTO);
        }

        public async Task EditMemberAsync(MemberDTO memberDTO) {
            await _memberRepository.EditMemberAsync(memberDTO);
        }
        public async Task DeleteMemberAsync(MemberDTO memberDTO) {
            await _memberRepository.DeleteMemberAsync(memberDTO);
        }
        public async Task<Member?> ValidateMemberAsync(string email, string password)
        {
            return await _memberRepository.ValidateMemberAsync(email,password);
        }
   
    }
}
