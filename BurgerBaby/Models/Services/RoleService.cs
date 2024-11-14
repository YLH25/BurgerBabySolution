using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.Repo.Interface;
using BurgerBaby.Models.Services.Interface;
using BurgerBaby.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace BurgerBaby.Models.Services
{
    public class RoleService: IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<PageResultVM<RoleVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize) {
            if (pageIndex == null || pageSize == null)
            {
                pageIndex = 1;
                pageSize = 10;
            }

            var pageResult = await GetRolesBysearchStringAsync(searchString, (int)pageIndex.Value, (int)pageSize.Value);

            if (pageResult.Items == null)
            {
                return new PageResultVM<RoleVM>();
            }

            var ps = new PageService();

            var pagesStringList = ps.GetPagesStringList((int)pageIndex.Value, pageResult.TotalPages);
            var pageResultVM = new PageResultVM<RoleVM>
            {
                Items = pageResult.Items.Select(x => new RoleVM
                {
                    Id = x.Id,
                    RoleName=x.RoleName,
                    Members = x.Members,
                   
                }).OrderByDescending(x => x.Id).ToList(),
                SearchString = searchString,
                TotalPages = pageResult.TotalPages,
                PageSize = pageResult.PageSize,
                PageIndex = pageResult.PageIndex,
                PagesStringList = pagesStringList,
                SearchFliterOption = "Role"
            };
            return pageResultVM;
        }
        public async Task<PageResult<Role>> GetRolesBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            var Roles = await _roleRepository.GetRolesBysearchStringAsync(searchString, pageIndex, pageSize);
            return Roles;
        }
        public async Task<Role?> GetRoleByIdAsync(int id) {
            return await _roleRepository.GetRoleByIdAsync(id);
        }
        public async Task CreateRoleAsync(RoleDTO roleDTO) {
            await _roleRepository.CreateRoleAsync(roleDTO);
        }
        public async Task EditRoleAsync(RoleDTO roleDTO) {
            await _roleRepository.EditRoleAsync(roleDTO);
        }
        public async Task DeleteRoleAsync(RoleDTO roleDTO) {
            await _roleRepository.DeleteRoleAsync(roleDTO);
        }
    }
}
