﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using BurgerBaby.Models.EFModel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#nullable enable
namespace BurgerBaby.Models.ViewModel
{
    public partial class ImgCreateVM
    {
        [Required]
        public int ProductId { get; set; }
        public bool? IsCover { get; set; }
        public IFormFile? FormFile { get; set; }

    }
    public static class ImgCreateVMExts
    {
        public static Img ToEntity(this ImgCreateVM imgCreateVM)
        {
            return new Img
            {
                ProductId = imgCreateVM.ProductId,
                IsCover=imgCreateVM.IsCover,
            };
        }
    }
}
