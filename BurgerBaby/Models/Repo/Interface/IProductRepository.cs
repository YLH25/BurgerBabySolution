using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.EntityFrameworkCore;

namespace BurgerBaby.Models.Repo.Interface
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
