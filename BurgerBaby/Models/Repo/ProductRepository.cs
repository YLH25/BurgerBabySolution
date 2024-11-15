using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.EntityFrameworkCore;
using System;

namespace BurgerBaby.Models.Repo.Interface
{

    public class ProductRepository : IProductRepository
    {
        private readonly BurgerBabyContext _context;
        public ProductRepository(BurgerBabyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(x => x.Imgs).Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Imgs = x.Imgs,
                Intro = x.Intro
            }).ToListAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Include(x => x.Imgs).Select(x=>new Product { 
            Id = x.Id,
            Name = x.Name,  
            Price = x.Price,
            Imgs = x.Imgs,
            Intro =x.Intro
            }).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<PageResult<Product>> GetProductsBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {
            IQueryable<Product> query = _context.Products.Include(x => x.Imgs).AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                var isInt = int.TryParse(searchString, out var i);
                if (isInt)
                {
                    query = query.Where(x => x.Id == i ||
                                            
                                             x.Name.Contains(searchString));
                }
                else
                {
                    query = query.Where(x => x.Name.Contains(searchString));
                }
            }
            var pageResult = new PageResult<Product>();
            var totalItems = await query.CountAsync();
            var items =await  query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            pageResult.Items = items;
            pageResult.TotalItems = totalItems;
            pageResult.PageSize = pageSize;
            pageResult.PageIndex = pageIndex;
           
            pageResult.TotalPages = (int)Math.Ceiling((double)pageResult.TotalItems / pageResult.PageSize);

            return pageResult;
        }

        public async Task CreateProductAsync(ProductDTO productDTO)
        {
            AddProduct(productDTO.ToEntity());
           await  SaveChangeAsync();
        }

        public async Task EditProductAsync(ProductDTO productDTO)
        {
            UpdateProduct(productDTO.ToEntity());
            await SaveChangeAsync();
        }
        public async Task DeleteProductAsync(ProductDTO productDTO)
        {
            RemoveProduct(productDTO.ToEntity());
            await SaveChangeAsync();
        }
        public void AddProduct(Product product)
        {
            _context.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            _context.Update(product);
        }
        public void RemoveProduct(Product product)
        {
            _context.Remove(product);
        }
        public async Task SaveChangeAsync() {
          await  _context.SaveChangesAsync();
        }
    }
}
