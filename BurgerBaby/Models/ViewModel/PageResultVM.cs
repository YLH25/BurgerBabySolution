namespace BurgerBaby.Models.ViewModel
{
    public class PageResultVM<T>
    {
        public string? SearchString { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<T>? Items { get; set; }
        public List<string>? PagesStringList { get; set; }
        public string? SearchFliterOption { get; set; }
    }
}
