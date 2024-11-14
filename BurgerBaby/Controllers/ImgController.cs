using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerBaby.Models.EFModel;
using Microsoft.AspNetCore.Authorization;
using BurgerBaby.Models.ViewModel;
using BurgerBaby.Models.Services.Interface;
using BurgerBaby.Models.Repo.Interface;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.Services;

namespace BurgerBaby.Controllers
{
    [Authorize]
    public class ImgController : Controller
    {
        private readonly IImgService _imgservice;
        private readonly IImgRepository _imgsRepository;
        private readonly IProductRepository _productRepository;
        public ImgController(IImgService imgService, IImgRepository imgRepository, IProductRepository productRepository)
        {
            _imgsRepository= imgRepository;
            _imgservice = imgService;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index(string? searchString, int? pageIndex, int? pageSize)
        {
            try
            {
                var pageReult=await _imgservice.GetPageResultVMAsync(searchString, pageIndex, pageSize);
                if (pageReult.Items == null)
                {
                    return View();
                }
                return View(pageReult);

            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
        }


        public async Task<IActionResult> Create(int? productId, bool? formDetail)
        {
            try {
                var products = await _productRepository.GetProductsAsync();
                if (productId.HasValue)
                {
                    var product = await _productRepository.GetProductByIdAsync(productId.Value);
                    var img = new ImgCreateVM();
                    img.ProductId = productId.Value;

                    ViewData["ProductId"] = new SelectList(products, "Id", "Name", productId);
                    if (formDetail == true)
                        ViewBag.FormDetail = formDetail;
                    return View(img);
                }
                ViewData["ProductId"] = new SelectList(products, "Id", "Name", productId);
                if (formDetail == true)
                    ViewBag.FormDetail = formDetail;
                return View();
            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
            
        }

        // POST: Img/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,FormFile,IsCover")] ImgCreateVM imgCreateVm, bool? formDetail)
        {
            try {
                var products = await _productRepository.GetProductsAsync();
                if (ModelState.IsValid && imgCreateVm.FormFile != null)
                {
                    var fileName = imgCreateVm.FormFile.FileName;

                    if (!_imgservice.IsAllowedFile(imgCreateVm.FormFile))
                    {
                        ModelState.AddModelError("", "請檢察上傳格式，只接受jpg, jpeg");
                        ViewData["ProductId"] = new SelectList(products, "Id", "Name", imgCreateVm.ProductId);
                        if (formDetail == true)
                            ViewBag.FormDetail = formDetail;
                        return View();
                    }
                    await _imgservice.CreateImgAsync(imgCreateVm.ToEntity().ToDto(), imgCreateVm.FormFile);
                    if (formDetail == true)
                        return RedirectToAction("Details", "Product", new { Id = imgCreateVm.ProductId });
                    else
                        return RedirectToAction(nameof(Index));
                }
                if (imgCreateVm.FormFile == null)
                    ModelState.AddModelError("", "沒有圖片");
                ViewData["ProductId"] = new SelectList(products, "Id", "Name", imgCreateVm.ProductId);
                if (formDetail == true)
                    ViewBag.FormDetail = formDetail;
                return View(imgCreateVm);
            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
        }

        // GET: Img/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try {
                var products = await _productRepository.GetProductsAsync();
                if (id == null)
                {
                    return RedirectToAction("Error", "Home", new { errorMessege = "沒有這筆資料" });
                }
                var img = await _imgsRepository.GetImgByIdAsync((int)id);
                if (img == null)
                    return RedirectToAction("Error", "Home", new { errorMessege = "沒有這筆資料" });
                var product = _productRepository.GetProductByIdAsync(img.ProductId);
                if (img.IsCover == null)
                    img.IsCover = false;
                var imgEditVm = img.ToEditVM();

                ViewData["ProductId"] = new SelectList(products, "Id", "Name", imgEditVm.ProductId);
                return View(imgEditVm);
            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
        }

        // POST: Img/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,ProductId,IsCover")] ImgEditVM imgEditVm, IFormFile? formFile)
        {
            try {
                var products = await _productRepository.GetProductsAsync();
                if (ModelState.IsValid)
                {
                    await _imgservice.EditImgAsync(imgEditVm.ToEntity().ToDto(), formFile);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProductId"] = new SelectList(products, "Id", "Name", imgEditVm.ProductId);
                return View(imgEditVm);
            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
            
        }

        // GET: Img/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try {
                var img = await _imgsRepository.GetImgByIdAsync(id);
                if (img == null)
                {
                    return RedirectToAction("Error", "Home", new { errorMessege = "沒有這筆資料" });
                }

                return View(img);
            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
           
        }
        // POST: Img/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try {
                var img = await _imgsRepository.GetImgByIdAsync(id);
                if (img != null)
                {
                    await _imgservice.DeleteImg(img.ToDto());
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errorMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errorMessege });
            }
        }
        //api
        [HttpPut("ChangeCover")]
        public async Task<IActionResult> ChangeCover(int imgId)
        {
            try
            {
                await _imgservice.ChangeCoverAsync(imgId);
                return Ok(new { resMessege = "封面更改成功" });
            }
            catch(Exception ex) {
                return BadRequest(new { resMessege =$"{ex.Message}" });
            }
            
        }
    }
}
