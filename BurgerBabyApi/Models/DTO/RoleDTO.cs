﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BurgerBabyApi.Models.EFModel;

public partial class RoleDTO
{
    public int Id { get; set; }

    public string RoleName { get; set; }
    public virtual ICollection<Member> Members { get; set; }

}
public static class RoleExt
{
    public static RoleDTO ToDto(this Role role)
    {
        return new RoleDTO
        {
            Id = role.Id,
            RoleName = role.RoleName,
            Members = role.Members
        };
    }

    public static Role ToEntity(this RoleDTO roleDTO)
    {
        return new Role
        {
            Id = roleDTO.Id,
            RoleName=roleDTO.RoleName,
            Members=roleDTO.Members
        };
    }
}