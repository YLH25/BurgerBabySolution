﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace BurgerBabyApi.Models.EFModel
{
    public partial class ImgDTO
    {
        public int Id { get; set; }

        public string ImgName { get; set; }

        public string SaveName { get; set; }

        public int ProductId { get; set; }
        public bool?  IsCover{ get; set; }

    }
    public static class ImgExt
    {
        public static ImgDTO ToDto(this Img img)
        {
            return new ImgDTO
            {
                Id = img.Id,
                ImgName = img.ImgName,
                SaveName = img.SaveName,
                ProductId = img.ProductId,
                IsCover = img.IsCover,
            };
        }

        public static Img ToEntity(this ImgDTO imgDTO)
        {
            return new Img
            {
                Id = imgDTO.Id,
                ImgName = imgDTO.ImgName,
                SaveName = imgDTO.SaveName,
                ProductId = imgDTO.ProductId,
                IsCover=imgDTO.IsCover,
            };
        }
    }
}
