using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using Microsoft.EntityFrameworkCore;

namespace BurgerBabyApi.Models.Repo.Interface
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
        void Update(Img img);
        void Delete(Img img);
        Task DeleteImgAsync(ImgDTO imgDTO);
        Task SaveChangesAsync();
    }
}
