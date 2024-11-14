using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using BurgerBabyApi.Models.ViewModel;

namespace BurgerBabyApi.Models.Services.Interface
{
    public interface IProductService
    {
        Task<PageResultVM<ProductVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize);
        Task CreateProductAsync(ProductDTO productDTO);
        Task EditProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(ProductDTO productDTO);
    }
}
