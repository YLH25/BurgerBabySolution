using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.ViewModel;
using System.Drawing.Printing;

namespace BurgerBaby.Models.Services.Interface
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
