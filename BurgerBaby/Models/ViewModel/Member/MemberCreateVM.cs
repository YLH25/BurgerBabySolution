using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BurgerBaby.Models.ViewModel
{
    public class MemberCreateVM
    {
        public int Id { get; set; }
        [Display(Name = "名字")]
        public string? Name { get; set; } 
        [Display(Name = "電話")]
        public string? Phone { get; set; } 
        [Display(Name = "地址")]
        public string? Address { get; set; } 
        [Display(Name = "電子郵件")]
        [EmailAddress(ErrorMessage ="請輸入有效的Email")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string? Email { get; set; } 
        [Required(ErrorMessage = "請輸入{0}")]
        public string? Password { get; set; }
        [Display(Name = "規則")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; } 


    }
}
public static class MemberCreateVMExt
{
    public static MemberCreateVM ToCreateVM(this Member member)
    {
        return new MemberCreateVM
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

    public static Member ToEntity(this MemberCreateVM memberCreateVM)
    {
        return new Member
        {
            Id = memberCreateVM.Id,
            Name = memberCreateVM.Name,
            Phone = memberCreateVM.Phone,
            Address = memberCreateVM.Address,
            Email = memberCreateVM.Email,
            Password = memberCreateVM.Password,
            RoleId = memberCreateVM.RoleId,
            Role = memberCreateVM.Role,
        };
    }

}