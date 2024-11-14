using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.Infa;
using BurgerBaby.Models.ViewModel;

namespace BurgerBaby.Models.Services.Interface
{
    public interface IImgService
    {
        Task<Img?> GetImgByIdAsync(int id);
        Task<IEnumerable<Img>> GetImgsAsync();
        Task<IEnumerable<Img>> GetImgsByProductIdAsync(int productId);
        Task ChangeCoverAsync(int productId);
        bool IsAllowedFile(IFormFile formFile);
       Task CreateImgAsync(ImgDTO imgDTO, IFormFile formFile);
        Task EditImgAsync(ImgDTO imgDTO, IFormFile? formFile);
        Task DeleteImg(ImgDTO imgDTO);
        Task<PageResultVM<ImgVM>> GetPageResultVMAsync(string? searchString, int? pageIndex, int? pageSize);


    }
}
