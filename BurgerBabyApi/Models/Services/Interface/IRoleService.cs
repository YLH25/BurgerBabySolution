using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using BurgerBabyApi.Models.ViewModel;

namespace BurgerBabyApi.Models.Services.Interface
{
    public interface IRoleService
    {
       Task<PageResultVM<RoleVM>>   GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize);
        Task<Role?> GetRoleByIdAsync(int id);
        Task CreateRoleAsync(RoleDTO roleDTO);
        Task EditRoleAsync(RoleDTO roleDTO);
        Task DeleteRoleAsync(RoleDTO roleDTO);
    }
}
