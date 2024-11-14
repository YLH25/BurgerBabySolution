using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using BurgerBabyApi.Models.Repo.Interface;
using BurgerBabyApi.Models.Services.Interface;
using BurgerBabyApi.Models.ViewModel;
using System.Drawing.Printing;

namespace BurgerBabyApi.Models.Services
{
    public class ProductService: IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PageResultVM<ProductVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 10;
            }

            var pageResult = await GetProductsBysearchStringAsync(searchString, pageIndex.Value, (int)pageSize.Value);

            if (pageResult.Items == null)
            {
                return new PageResultVM<ProductVM>();
            }

            var pagesStringList = new PageService().GetPagesStringList((int)pageIndex.Value, pageResult.TotalPages);
            var pageResultVM = new PageResultVM<ProductVM>
            {
                Items = pageResult.Items.Select(x => new ProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Imgs = x.Imgs,
                    Intro = x.Intro,

                }).OrderByDescending(x => x.Id).ToList(),
                SearchString = searchString,
                TotalPages = pageResult.TotalPages,
                PageSize = pageResult.PageSize,
                PageIndex = pageResult.PageIndex,
                PagesStringList = pagesStringList,
                SearchFliterOption = "Product"
            };
            return pageResultVM;
        }

        public async Task<PageResult<Product>> GetProductsBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            return await _productRepository.GetProductsBysearchStringAsync(searchString, pageIndex, pageSize);
        }

        public async Task CreateProductAsync(ProductDTO productDTO)
        {
            await _productRepository.CreateProductAsync(productDTO);
        }
        public async Task EditProductAsync(ProductDTO productDTO)
        {
            await _productRepository.EditProductAsync(productDTO);
        }
        public async Task DeleteProductAsync(ProductDTO productDTO)
        {
            await _productRepository.DeleteProductAsync(productDTO);
        }
    }
}
