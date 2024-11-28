﻿using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace BurgerBaby.Models.Repo.Interface
{

    public class ImgRepository : IImgRepository
    {
        private readonly BurgerBabyContext _context;
        public ImgRepository(BurgerBabyContext context)
        {
            _context = context;
        }
        public async Task<Img?> GetImgByIdAsync(int id)
        {
            return await _context.Imgs.AsNoTracking().Include(i => i.Product).Select(x=>new Img { 
            Id = x.Id,
            Product = x.Product,
            ProductId = x.ProductId,
            ImgName = x.ImgName,
            SaveName = x.SaveName,
            IsCover = x.IsCover,
            }).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Img>> GetImgsAsync() {
            return await _context.Imgs.AsNoTracking().Include(i => i.Product).Select(x => new Img
            {
                Id = x.Id,
                Product = x.Product,
                ProductId = x.ProductId,
                ImgName = x.ImgName,
                SaveName = x.SaveName,
                IsCover = x.IsCover,
            }).ToListAsync() ;
        }
        public async Task<PageResult<Img>> GetImgsBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            IQueryable<Img> query = _context.Imgs.AsNoTracking().Include(i => i.Product);

            if (!string.IsNullOrEmpty(searchString))
            {
                var isInt = int.TryParse(searchString, out var i);
                if (isInt)
                {
                    query = query.Where(x => x.Id == i ||
                                             x.Product.Name.Contains(searchString) ||
                                             x.SaveName == searchString ||
                                             x.ImgName.Contains(searchString));
                }
                else
                {
                    query = query.Where(x => x.Product.Name.Contains(searchString) ||
                                             x.SaveName == searchString ||
                                             x.ImgName.Contains(searchString));
                }
            }
            else { 
            query=_context.Imgs.AsNoTracking().Include(i => i.Product); 
            }
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageResult = new PageResult<Img>();
            pageResult.Items = items;
            pageResult.PageSize = pageSize;
            pageResult.PageIndex=pageIndex;
            pageResult.TotalItems = query.Count();
            pageResult.TotalPages = (int)Math.Ceiling((double)pageResult.TotalItems / pageResult.PageSize);

            return pageResult;
        }
        public async Task<IEnumerable<Img>> GetImgsByProductIdAsync(int productId)
        {
            return await _context.Imgs.AsNoTracking().Include(i => i.Product).Where(x => x.ProductId == productId).Select(x=>new Img { 
            Id = x.Id,
            ImgName = x.ImgName,
            IsCover = x.IsCover,
            ProductId = productId,
            SaveName = x.SaveName,
            }).ToListAsync();
        }
        public async Task AddImgAsync(ImgDTO imgDTO)
        {
            var img = imgDTO.ToEntity();
            _context.Add(img);
            await _context.SaveChangesAsync();
        }

        public async Task<Img?> GetImgByIdAsNoTrackingAsync(int id)
        {
           
            return await _context.Imgs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UpdateImgAsync(ImgDTO imgDTO)
        {

            Update(imgDTO.ToEntity());
            await SaveChangesAsync();
        }
        private void Update(Img img)
        {
                _context.Update(img);
        }
        public async Task DeleteImgAsync(ImgDTO imgDTO)
        {
            var img = await _context.Imgs.FirstOrDefaultAsync(x => x.Id == imgDTO.Id);
            if (img != null)
                Delete(img);
            await SaveChangesAsync();
        }
        private void Delete(Img img)
        {
            _context.Remove(img);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
