using System.Collections;

namespace BurgerBaby.Models.Services
{
    public class PageService
    {
        public List<string>? GetPagesStringList(int pageIndex, int totalPages)
        {

            var pagesList = new List<string>();

            pagesList.Add(pageIndex.ToString());
            for (var i = 1; i <= 7; i++)
            {
                if (pageIndex - i > 0)
                {
                    pagesList.Insert(0, ((pageIndex - i).ToString()));
                }
                if (pageIndex + i <= totalPages)
                {
                    pagesList.Add((pageIndex + i).ToString());
                }
                if (pagesList.Count() == 7)
                    break;
            }

            if (Convert.ToInt32(pagesList[0]) > 1)
            {
                pagesList[0] = "1..";
            }
            if (Convert.ToInt32(pagesList[pagesList.Count - 1]) < totalPages)
            {
                pagesList[pagesList.Count - 1] = $"..{totalPages}";
            }
            return pagesList;
        }

    }
}
