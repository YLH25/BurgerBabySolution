﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using BurgerBaby.Models.EFModel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BurgerBaby.Models.ViewModel;
//TODO LOGINVM與MEMBERVM分開
public partial class MemberVM
{
    public int Id { get; set; }
    [Display(Name = "名字")]
    public string Name { get; set; }
    [Display(Name = "電話")]
    public string Phone { get; set; }
    [Display(Name = "地址")]
    public string Address { get; set; }
    [Display(Name = "電子郵件")]
    [Required(ErrorMessage = "請輸入{0}")]
    public string Email { get; set; }
    [Display(Name = "密碼")]
    [Required(ErrorMessage = "請輸入{0}")]
    public string Password { get; set; }
    [Display(Name = "規則")]
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }

    public IEnumerable<Img> Imgs { get; set; }
}

public static class MemberVMExt
{
    public static MemberVM ToVM(this Member member)
    {
        return new MemberVM
        {
            Id = member.Id,
            Name = member.Name,
            Phone = member.Phone,
            Address = member.Address,
            Email = member.Email,
            Password = member.Password,
            RoleId = member.RoleId,
            Role = member.Role,

        } ;
    }

    public static Member ToEntity(this MemberVM memberVM)
    {
        return new Member
        {
            Id = memberVM.Id,
            Name = memberVM.Name,
            Phone = memberVM.Phone,
            Address = memberVM.Address,
            Email = memberVM.Email,
            Password = memberVM.Password,
            RoleId = memberVM.RoleId,
            Role = memberVM.Role,

        };
    }

}