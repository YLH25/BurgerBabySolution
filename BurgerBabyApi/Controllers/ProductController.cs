using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Repo.Interface;
using BurgerBabyApi.Models.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BurgerBabyApi.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductService productService, IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository= productRepository;
        }
        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<JsonResult>  Index([FromQuery] string? searchString, int? pageIndex, int? pageSize)
        {
            var pageReult = await _productService.GetPageResultVMAsync(searchString, pageIndex, pageSize);
            return Json(pageReult);
        }
       
        [HttpGet("product/{id}")]
        public async Task<JsonResult> Index(int id)
        {
            var product = await _productRepository.GetProductByIdAsync((int)id);
            if (product == null) 
                return Json(null);
            if (product.Imgs.Count()>0)
            {
                var imgsList=product.Imgs.ToList();
                var cover = product.Imgs.FirstOrDefault(x => x.IsCover == true);
                if (cover != null) {
                    imgsList.Remove(cover);
                    imgsList.Insert(0, cover);
                }
                product.Imgs = imgsList;
            }
            return Json(product);
        }
    }
}
