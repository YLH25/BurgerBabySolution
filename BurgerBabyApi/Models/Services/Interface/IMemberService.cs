using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using BurgerBabyApi.Models.ViewModel;
using System.Drawing.Printing;

namespace BurgerBabyApi.Models.Services.Interface
{
    public interface IMemberService
    {
       Task<PageResultVM<MemberVM>> GetPageResultVMAsync( string?searchString,int? pageIndex, int?pageSize);
        Task<PageResult<Member>> GetMembersBysearchStringAsync(string? searchString, int pageIndex, int pageSize);
        Task CreateMemberAsync(MemberDTO memberDTO);
        Task EditMemberAsync(MemberDTO memberDTO);
        Task DeleteMemberAsync(MemberDTO memberDTO);
        Task<Member?> ValidateMemberAsync(string email, string password);
        
    }
}
