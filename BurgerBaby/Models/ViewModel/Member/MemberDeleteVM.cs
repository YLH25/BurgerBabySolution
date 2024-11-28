using BurgerBaby.Models.EFModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BurgerBaby.Models.ViewModel
{
    public class MemberDeleteVM
    {
        public int Id { get; set; }
        [Display(Name = "名字")]
        public string? Name { get; set; }
        [Display(Name = "電話")]
        public string? Phone { get; set; }
        [Display(Name = "地址")]
        public string? Address { get; set; }
        [Display(Name = "電子郵件")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string? Email { get; set; }
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string? Password { get; set; }
        [Display(Name = "規則")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }


    }
    public static class MemberDeleteVMExt
    {
        public static MemberDeleteVM ToDeleteVM(this Member member)
        {
            return new MemberDeleteVM
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

        public static Member ToEntity(this MemberDeleteVM memberDeleteVM)
        {
            return new Member
            {
                Id = memberDeleteVM.Id,
                Name = memberDeleteVM.Name,
                Phone = memberDeleteVM.Phone,
                Address = memberDeleteVM.Address,
                Email = memberDeleteVM.Email,
                Password = memberDeleteVM.Password,
                RoleId = memberDeleteVM.RoleId,
                Role = memberDeleteVM.Role,
       
            };
        }
    }
}
