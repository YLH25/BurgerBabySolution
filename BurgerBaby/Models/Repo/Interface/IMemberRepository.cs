using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.EntityFrameworkCore;

namespace BurgerBaby.Models.Repo.Interface
{
    public interface IMemberRepository
    {
         Task<Member?> GetMemberByEmailAsync(string email);
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member?> GetMemberByIdAsync(int id);
        Task<PageResult<Member>> GetMembersBysearchStringAsync(string? searchString, int pageIndex, int pageSize);
        Task CreateMemberAsync(MemberDTO memberDTO);
        Task EditMemberAsync(MemberDTO memberDTO);
        Task DeleteMemberAsync(MemberDTO memberDTO);
        Task<Member?> ValidateMemberAsync(string email, string password);

    }
}
