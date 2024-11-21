using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using Microsoft.EntityFrameworkCore;

namespace BurgerBaby.Models.Repo.Interface
{
    public interface IImgRepository
    {
        Task<Img?> GetImgByIdAsync(int id);
         Task AddImgAsync(ImgDTO imgDTO);
        Task<Img?> GetImgByIdAsNoTrackingAsync(int id);
        Task UpdateImgAsync(ImgDTO imgDTO);
        Task<PageResult<Img>> GetImgsBysearchStringAsync(string? searchString, int pageIndex, int pageSize);
        Task<IEnumerable<Img>> GetImgsAsync();
        Task<IEnumerable<Img>> GetImgsByProductIdAsync(int productId);

        Task DeleteImgAsync(ImgDTO imgDTO);
        Task SaveChangesAsync();
    }
}
