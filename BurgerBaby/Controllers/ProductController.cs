using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.ViewModel;
using BurgerBaby.Models.Services;
using BurgerBaby.Models.Services.Interface;
using BurgerBaby.Models.Repo.Interface;

namespace BurgerBaby.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IMemberRepository _memberRepository;
        public ProductController(IProductService productService, IProductRepository productRepository, IMemberRepository memberRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
            _memberRepository = memberRepository;
        }
        // GET: Product
        public async Task<IActionResult> Index(string? searchString, int? pageIndex, int? pageSize)
        {
            try
            {
                var pageReult = await _productService.GetPageResultVMAsync(searchString, pageIndex, pageSize);
                if (pageReult.Items == null)
                {
                    return View();
                }
                return View(pageReult);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    var errMessege = "沒有這筆資料";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var product = await _productRepository.GetProductByIdAsync((int)id);
                if (product == null)
                {
                    var errMessege = "沒有這筆資料";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                var productDetailVM = product.ToVM();
                return View(productDetailVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Product/Create
        public async Task<IActionResult> Create(int? memberId)
        {
            try
            {
                var members = await _memberRepository.GetMembersAsync();

                ViewData["MemberId"] = new SelectList(members, "Id", "Name", memberId);
                return View();
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Intro")] ProductVM productVM)
        {
            try
            {
                var members = await _memberRepository.GetMembersAsync();
                if (ModelState.IsValid)
                {
                    await _productService.CreateProductAsync(productVM.ToEntity().ToDto());
                    return RedirectToAction(nameof(Index));
                }
                return View(productVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var products = await _productRepository.GetProductsAsync();
                if (id == null || products == null)
                {
                    var errMessege = "沒有這個產品";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }

                var product = await _productRepository.GetProductByIdAsync((int)id);
                if (product == null)
                {
                    var errMessege = "沒有這個產品";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }
                return View(product.ToEditVM());
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,Intro")] ProductEditVM productEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = await _productRepository.GetProductByIdAsync(productEditVM.Id);
                    if (product == null)
                    {
                        var errMessege = "沒有這個產品";
                        return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                    }
                    await _productService.EditProductAsync(productEditVM.ToEntity().ToDto());
                    return RedirectToAction(nameof(Index));
                }

                return View(productEditVM);
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    var errMessege = "沒有這個產品";
                    return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
                }

                return View(product.ToDeleteVM());
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product != null)
                {
                   await _productService.DeleteProductAsync(product.ToDto());
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errMessege = ex.Message;
                return RedirectToAction("Error", "Home", new { errorMessege = errMessege });
            }
        }
    }
}
