using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BurgerBabyApi.Models.ViewModel
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
        [Required(ErrorMessage = "請輸入{0}")]
        public string? Email { get; set; }
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string? Password { get; set; }
        [Display(Name = "規則")]
        public int RoleId { get; set; } = 2;
        public virtual Role? Role { get; set; }
    }
}
public static class MemberCreateVMExt
{
    public static MemberCreateVM ToCreateVM(this Member member)
    {
        return new MemberCreateVM
        {
            Name = member.Name,
            Phone = member.Phone,
            Address = member.Address,
            Email = member.Email,
            Password = member.Password,
            RoleId = member.RoleId,
        };
    }

    public static Member ToEntity(this MemberCreateVM memberCreateVM)
    {
        return new Member
        {
            Name = memberCreateVM.Name,
            Phone = memberCreateVM.Phone,
            Address = memberCreateVM.Address,
            Email = memberCreateVM.Email,
            Password = memberCreateVM.Password,
            RoleId = memberCreateVM.RoleId,
        };
    }

}