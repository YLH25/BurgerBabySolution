using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BurgerBaby.Models.Repo.Interface
{

    public class RoleRepository : IRoleRepository
    {
        private readonly BurgerBabyContext _context;
        public RoleRepository(BurgerBabyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetRolesAsync() { 
        
        return await _context.Roles.AsNoTracking().Include(x=>x.Members).Select(x=>new Role { 
        Id = x.Id,
        RoleName=x.RoleName,
        Members=x.Members
        }).ToListAsync();
        }
        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.AsNoTracking().Include(x => x.Members).Select(x => new Role
            {
                Id = x.Id,
                RoleName = x.RoleName,
                Members = x.Members
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageResult<Role>> GetRolesBysearchStringAsync(string? searchString, int pageIndex, int pageSize) {
            IQueryable<Role> query = _context.Roles.AsNoTracking().Include(x => x.Members).AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                var isInt = int.TryParse(searchString, out var i);
                if (isInt)
                {
                    query = query.Where(x => x.Id == i ||
                                             x.RoleName.Contains(searchString) ||
                                             x.Members.Any(x=>x.Name==searchString));
                }
                else
                {
                    query = query.Where(x => x.RoleName.Contains(searchString) ||
                                             x.Members.Any(x => x.Name == searchString));
                }
            }

            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageResult = new PageResult<Role>();
            pageResult.Items = items;
            pageResult.PageSize = pageSize;
            pageResult.PageIndex = pageIndex;
            pageResult.TotalItems = query.Count();
            pageResult.TotalPages = (int)Math.Ceiling((double)pageResult.TotalItems / pageResult.PageSize);

            return pageResult;
        }

        public async Task CreateRoleAsync(RoleDTO roleDTO)
        {
            AddRole(roleDTO.ToEntity());
            await SaveChangeAsync();
        }


        public async Task EditRoleAsync(RoleDTO roleDTO)
        {
            UpdateRole(roleDTO.ToEntity());
            await SaveChangeAsync();
        }
        public async Task DeleteRoleAsync(RoleDTO roleDTO)
        {
            RemoveRole(roleDTO.ToEntity());
            await SaveChangeAsync();
        }
        public void AddRole(Role role)
        {
            _context.Add(role);
        }
        public void UpdateRole(Role role)
        {
            _context.Update(role);
        }
        public void RemoveRole(Role role)
        {
            _context.Remove(role);
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
