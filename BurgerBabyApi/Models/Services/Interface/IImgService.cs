using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa;
using BurgerBabyApi.Models.ViewModel;

namespace BurgerBabyApi.Models.Services.Interface
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
