using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace BurgerBabyApi.Models.Repo.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<PageResult<Role>> GetRolesBysearchStringAsync(string? searchString, int pageIndex, int pageSize);
        Task<Role?> GetRoleByIdAsync(int id);
        Task CreateRoleAsync(RoleDTO RoleDTO);
        Task EditRoleAsync(RoleDTO RoleDTO);
        Task DeleteRoleAsync(RoleDTO RoleDTO);
    }
}
