using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.Repo.Interface;
using BurgerBaby.Models.Services.Interface;
using BurgerBaby.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace BurgerBaby.Models.Services
{
    public class ImgService : IImgService
    {
        private IImgRepository _imgRepository;

        public ImgService(IImgRepository imgRepository)
        {
            _imgRepository = imgRepository;

        }
        public async Task<Img?> GetImgByIdAsync(int id)
        {
            return await _imgRepository.GetImgByIdAsync(id);
        }
        public async Task<PageResult<Img>> GetImgsBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            var imgs = await _imgRepository.GetImgsBysearchStringAsync(searchString, pageIndex, pageSize);
            return imgs;
        }
        public async Task<IEnumerable<Img>> GetImgsAsync()
        {
            var imgs = await _imgRepository.GetImgsAsync();
            return imgs;
        }
        public async Task<IEnumerable<Img>> GetImgsByProductIdAsync(int productId)
        {
            return await _imgRepository.GetImgsByProductIdAsync(productId);
        }

        public async Task ChangeCoverAsync(int imgId)
        {
            var img = await GetImgByIdAsync(imgId);
            if (img != null)
            {
                var imgs = await GetImgsByProductIdAsync(img.ProductId);
                imgs = imgs.Where(x => x.IsCover == true);
                if (imgs != null)
                {
                    foreach (var i in imgs)
                    {
                        i.IsCover = false;
                     await   _imgRepository.UpdateImgAsync(i.ToDto());
                    }
                }
                img.IsCover = true;
             await   _imgRepository.UpdateImgAsync(img.ToDto());
                await _imgRepository.SaveChangesAsync();
            }
        }
        public bool IsAllowedFile(IFormFile formFile)
        {
            var fileName = formFile.FileName;
            var allowedExtension = new string[] { ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(fileName);
            return allowedExtension.Contains(fileExtension);
        }

        public async Task CopyFileAsync(Img img, IFormFile formFile)
        {

            var fileName = formFile.FileName;
            var fileExtension = Path.GetExtension(fileName);
            var newfileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(
           Directory.GetCurrentDirectory(), "wwwroot/savedata/",
           newfileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            img.ImgName = fileName;
            img.SaveName = newfileName;
        }
        public async Task CreateImgAsync(ImgDTO imgDTO, IFormFile formFile)
        {
            var img = imgDTO.ToEntity();

            await CopyFileAsync(img, formFile);
            await _imgRepository.AddImgAsync(img.ToDto());
        }
        public async Task EditImgAsync(ImgDTO imgDTO, IFormFile? formFile)
        {
            var img = imgDTO.ToEntity();
            var oldImg = await _imgRepository.GetImgByIdAsNoTrackingAsync(imgDTO.Id);
            if (oldImg != null)
            {
                if (formFile != null)
                {
                    var oldFilePath = "wwwroot/savedata/"+ oldImg.SaveName;
                    var exist = System.IO.File.Exists(oldFilePath);
                    if (exist == true)
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    await CopyFileAsync(img, formFile);
                }
                else
                {
                    img.SaveName = oldImg.SaveName;
                    img.ImgName = oldImg.ImgName;
                }
                if (img.IsCover == true)
                {
                    await ChangeCoverAsync(img.Id);
                }
                else
                {
                 await    _imgRepository.UpdateImgAsync(img.ToDto());
                    await _imgRepository.SaveChangesAsync();
                }
            }


        }

        public async Task DeleteImg(ImgDTO imgDTO)
        {
            var filePath = "wwwroot/savedata/" + imgDTO.ToEntity().SaveName;
            var exist = System.IO.File.Exists(filePath);
            if (exist == true)
            {
                System.IO.File.Delete(filePath);
            }
            await _imgRepository.DeleteImgAsync(imgDTO);
        }

        public async Task<PageResultVM<ImgVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageSize == null)
            {
                pageIndex = 1;
                pageSize = 10;
            }

            var pageResult = await GetImgsBysearchStringAsync(searchString, (int)pageIndex.Value, (int)pageSize.Value);
            if (pageResult == null||pageResult.Items==null)
                return new PageResultVM<ImgVM>();

            var ps = new PageService();

            var pagesStringList = ps.GetPagesStringList((int)pageIndex.Value, pageResult.TotalPages);

            var pageResultVM = new PageResultVM<ImgVM>
            {
                Items = pageResult.Items.Select(x => new ImgVM
                {
                    Id = x.Id,
                    ImgName = x.ImgName,
                    SaveName = x.SaveName,
                    ProductId = x.ProductId,
                    Product = x.Product
                }).OrderByDescending(x => x.Id).ToList(),
                SearchString = searchString,
                TotalPages = pageResult.TotalPages,
                PageSize = pageResult.PageSize,
                PageIndex = pageResult.PageIndex,
                PagesStringList = pagesStringList,
                SearchFliterOption = "Img"
            };

            return pageResultVM;
        }

    }
}
