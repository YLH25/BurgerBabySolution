using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using Microsoft.EntityFrameworkCore;

namespace BurgerBabyApi.Models.Repo.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member?> GetMemberByIdAsync(int id);
        Task<PageResult<Member>> GetMembersBysearchStringAsync(string? searchString, int pageIndex, int pageSize);
        Task CreateMemberAsync(MemberDTO memberDTO);
        Task EditMemberAsync(MemberDTO memberDTO);
        Task DeleteMemberAsync(MemberDTO memberDTO);
        Task<Member?> ValidateMemberAsync(string email, string password);

    }
}
