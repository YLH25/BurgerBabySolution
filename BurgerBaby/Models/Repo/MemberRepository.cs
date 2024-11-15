using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.EntityFrameworkCore;
using System;

namespace BurgerBaby.Models.Repo.Interface
{

    public class MemberRepository : IMemberRepository
    {
        private readonly BurgerBabyContext _context;
        public MemberRepository(BurgerBabyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _context.Members.Include(x => x.Role).Select(x=>new Member { 
            Id=x.Id,
Name=x.Name,
RoleId=x.RoleId,
Address=x.Address,
Password=x.Password,
Phone=x.Phone,
Role=x.Role,
Email=x.Email,
            }).ToListAsync();
        }
        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            var member = await _context.Members.Include(x => x.Role).Select(x => new Member
            {
                Id = x.Id,
                Name = x.Name,
                RoleId = x.RoleId,
                Address = x.Address,
                Password = x.Password,
                Phone = x.Phone,
                Role = x.Role,
                Email = x.Email,
            }).FirstOrDefaultAsync(x => x.Id == id);
            return member;
        }
        public async Task<PageResult<Member>> GetMembersBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            IQueryable<Member> query = _context.Members.Include(x => x.Role).AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                var isInt = int.TryParse(searchString, out var i);
                if (isInt)
                {
                    query = query.Where(x => x.Id == i ||
                                             x.Name.Contains(searchString) ||
                                             x.Address.Contains(searchString)||
                                             x.Role.RoleName.Contains(searchString)||
                                             x.Email.Contains(searchString));
                }
                else
                {
                    query = query.Where(x => x.Name.Contains(searchString) ||
                                             x.Address.Contains(searchString) ||
                                             x.Role.RoleName.Contains(searchString) ||
                                             x.Email.Contains(searchString));
                }
            }

            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageResult = new PageResult<Member>();
            pageResult.Items = items;
            pageResult.PageSize = pageSize;
            pageResult.PageIndex = pageIndex;
            pageResult.TotalItems = query.Count();
            pageResult.TotalPages = (int)Math.Ceiling((double)pageResult.TotalItems / pageResult.PageSize);

            return pageResult;
        }
        public async Task CreateMemberAsync(MemberDTO memberDTO) {
            AddMember(memberDTO.ToEntity());
            await SaveChangeAsync();
        }


        public async Task EditMemberAsync(MemberDTO memberDTO)
        {
            UpdateMember(memberDTO.ToEntity());
            await SaveChangeAsync();
        }
        public async Task DeleteMemberAsync(MemberDTO memberDTO)
        {
            RemoveMember(memberDTO.ToEntity());
            await SaveChangeAsync();
        }
        public void AddMember(Member member)
        {
            _context.Add(member);
        }
        public void UpdateMember(Member member)
        {
            _context.Update(member);
        }
        public void RemoveMember(Member member)
        {
            _context.Remove(member);
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Member?> ValidateMemberAsync(string email, string password) { 
        return await _context.Members.Where(x => x.Email == email).Where(x => x.Password == password).FirstOrDefaultAsync();
        }
  
    }
}
