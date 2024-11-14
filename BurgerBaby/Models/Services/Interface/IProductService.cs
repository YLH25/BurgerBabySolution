using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.ViewModel;

namespace BurgerBaby.Models.Services.Interface
{
    public interface IProductService
    {
        Task<PageResultVM<ProductVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize);
        Task CreateProductAsync(ProductDTO productDTO);
        Task EditProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(ProductDTO productDTO);
    }
}
