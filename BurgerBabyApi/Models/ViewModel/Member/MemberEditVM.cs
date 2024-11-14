using BurgerBabyApi.Models.EFModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BurgerBabyApi.Models.ViewModel
{
    public class MemberEditVM
    {
        public int Id { get; set; }
        [Display(Name = "名字")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "電話")]
        public string Phone { get; set; } = string.Empty;
        [Display(Name = "地址")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "電子郵件")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "規則")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = new Role();

    }
    public static class MemberEditVMExt
    {
        public static MemberEditVM ToEditVM(this Member member)
        {
            return new MemberEditVM
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Address = member.Address,
                Email = member.Email,
                Password = member.Password,
                RoleId = member.RoleId,
                Role = member.Role,

            };
        }

        public static Member ToEntity(this MemberEditVM memberEditVM)
        {
            return new Member
            {
                Id = memberEditVM.Id,
                Name = memberEditVM.Name,
                Phone = memberEditVM.Phone,
                Address = memberEditVM.Address,
                Email = memberEditVM.Email,
                Password = memberEditVM.Password,
                RoleId = memberEditVM.RoleId,
                Role = memberEditVM.Role,

            };
        }

    }
}
