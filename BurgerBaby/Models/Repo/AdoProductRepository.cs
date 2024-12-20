using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using BurgerBaby.Models.Repo.Interface;


namespace BurgerBaby.Models.Repo
{

    public class AdoProductRepository : IProductRepository
    {
        private readonly string connString;
        public AdoProductRepository(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var query = @"
        SELECT 
            p.Id, p.Name, p.Price, p.Intro, 
            i.Id AS ImgId, i.Product_Id, i.ImgName, i.SaveName, i.IsCover
        FROM Products p
        LEFT JOIN Imgs i ON p.Id = i.Product_Id where 1=1";
            SqlParameter[] parameters = new SqlParameter[] { };
            DataTable? dt = await Task.Run(() => new SqlDbHelper(connString).Select(query, parameters));
            if (dt == null)
            {
                return Enumerable.Empty<Product>();
            }
            var products = GetDataFromDT(dt)
                .ToList();
            return products;
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            string query = @"
        SELECT 
            p.Id, p.Name, p.Price, p.Intro, 
            i.Id AS ImgId, i.Product_Id, i.ImgName, i.SaveName, i.IsCover
        FROM Products p
        LEFT JOIN Imgs i ON p.Id = i.Product_Id
       where 1=1";
            SqlParameter[] parameters = new SqlParameter[] { };
            if (id > 0)
            {
                query = query + "and p.Id=@Id";
                parameters = new SqlParameterBuilder().AddInt("@Id", id).Build();
            }
            DataTable? dt = await Task.Run(() => new SqlDbHelper(connString).Select(query, parameters));
            if (dt == null)
            {
                return new Product();
            }
            var product = GetDataFromDT(dt).SingleOrDefault();
            return product == null ? new Product() : product;
        }

        public async Task<PageResult<Product>> GetProductsBysearchStringAsync(string? searchString, int pageIndex, int pageSize)
        {

            string factor = "and   p.Name like @productName";
            string fliterQue = "";
            if (searchString != null)
                fliterQue = factor;
            string query = @$"	 SELECT (SELECT COUNT(DISTINCT p.Id)
FROM Products p
LEFT JOIN Imgs i ON p.Id = i.Product_Id

WHERE 1=1 {fliterQue}) as TotalItems,  
        pd.Id, Name, pd.Price, pd.Intro, 
        i.Id AS ImgId, i.Product_Id, i.ImgName, i.SaveName, i.IsCover
        from (select  p.Id, p.Name, p.Price,  p.Intro
	from Products p where 1=1 {fliterQue} order by p.Id offset @offset rows fetch next @fetch rows only)  pd left join Imgs i on pd.Id=i.Product_Id";

            var parameters = new SqlParameter[] { };
            if (string.IsNullOrEmpty(searchString))
            {
                parameters = new SqlParameterBuilder()
                        .AddInt("@offset", (pageIndex - 1) * pageSize)
                        .AddInt("@fetch", pageSize)
                        .Build();
            }
            else
            {
                var isInt = int.TryParse(searchString, out var i);

                if (isInt)
                {
                    factor += "or p.Id = @productId ";
                    parameters = new SqlParameterBuilder()
                        .AddInt("@productId", i)
                        .AddNVarchar("@productName", 200, "%" + searchString + "%")
                        .AddInt("@offset", (pageIndex - 1) * pageSize)
                        .AddInt("@fetch", pageSize)
                        .Build();
                }
                else
                {
                    parameters = new SqlParameterBuilder()
                        .AddNVarchar("@productName", 200, "%" + searchString + "%")
                         .AddInt("@offset", (pageIndex - 1) * pageSize)
                        .AddInt("@fetch", pageSize)
                         .Build();
                }
            }
            DataTable? dt = await Task.Run(() => new SqlDbHelper(connString).Select(query, parameters));
            if (dt == null) { return new PageResult<Product>(); }
            var totalItems = dt.AsEnumerable().Select(x => x.Field<int>("TotalItems")).FirstOrDefault();
            var items = GetDataFromDT(dt).ToList();

            var pageResult = new PageResult<Product>();
            pageResult.Items = items;
            pageResult.PageSize = pageSize;
            pageResult.PageIndex = pageIndex;
            pageResult.TotalItems = totalItems;
            pageResult.TotalPages = (int)Math.Ceiling((double)pageResult.TotalItems / pageResult.PageSize);

            return pageResult;
        }


        public async Task CreateProductAsync(ProductDTO productDTO)
        {
            var product = productDTO.ToEntity();
            string query = "insert into Products (Name,Price,Intro) values (@Name,@Price,@Intro)";
            var parameters = new SqlParameterBuilder()
                .AddNVarchar("@Name", 200, product.Name)
                .AddDecimal("@Price", product.Price)
                .AddNVarchar("@Intro", 1000, product.Intro ?? string.Empty)
                .Build();

            await Task.Run(() => new SqlDbHelper(connString).ExecuteNonQuery(query, parameters));
        }

        public async Task EditProductAsync(ProductDTO productDTO)
        {
            var product = productDTO.ToEntity();
            var query = "update Products set Name=@Name,Price=@Price ,Intro=@Intro where Id=@Id";
            var parameters = new SqlParameterBuilder()
                .AddNVarchar("@Name", 200, product.Name)
                .AddDecimal("@Price", product.Price)
                .AddNVarchar("@Intro", 1000, product.Intro ?? string.Empty)
                .AddInt("@Id", product.Id).Build();

            await Task.Run(() => new SqlDbHelper(connString).ExecuteNonQuery(query, parameters));
        }
        public async Task DeleteProductAsync(ProductDTO productDTO)
        {
            var product = productDTO.ToEntity();
            var query = "delete from Products  where  Id=@Id";
            var parameters = new SqlParameterBuilder().AddInt("@Id", product.Id).Build();
            await Task.Run(() => new SqlDbHelper(connString).ExecuteNonQuery(query, parameters));
        }

        private IEnumerable<Product> GetDataFromDT(DataTable dt)
        {
            return dt.AsEnumerable()
                    .GroupBy(x => new
                    {
                        Id = x.Field<int>("Id"),
                    })
                    .Select(g => new Product
                    {
                        Id = g.Key.Id,
                        Name = g.First().Field<string>("Name"),
                        Price = g.First().Field<decimal>("Price"),
                        Intro = g.First().Field<string>("Intro"),
                        Imgs = g.Where(x => !x.IsNull("ImgId"))
                        .Select(x => new Img
                        {
                            Id = x.Field<int>("ImgId"),
                            ProductId = x.Field<int>("Product_Id"),
                            ImgName = x.Field<string>("ImgName"),
                            SaveName = x.Field<string>("SaveName"),
                            IsCover = x.Field<bool?>("IsCover"),
                        }).ToList()
                    });
        }
    }
}
