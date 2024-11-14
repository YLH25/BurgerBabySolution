
namespace BurgerBaby.Models.Infa
{
    public class PageResult<T> 
    {
        public IEnumerable<T>? Items { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
 
    }

}
