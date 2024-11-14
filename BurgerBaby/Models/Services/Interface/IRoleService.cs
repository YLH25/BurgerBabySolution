using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.ViewModel;

namespace BurgerBaby.Models.Services.Interface
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
