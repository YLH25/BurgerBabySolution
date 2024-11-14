using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using Microsoft.EntityFrameworkCore;

namespace BurgerBabyApi.Models.Repo.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<PageResult<Product>> GetProductsBysearchStringAsync(string? searchString, int pageIndex, int pageSize);
        Task CreateProductAsync(ProductDTO productDTO);
        Task EditProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(ProductDTO productDTO);

    }
}
