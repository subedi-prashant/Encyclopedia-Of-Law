using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Views.Shared.Components.SearchBar1
{
    public class SPager
    {
        public SPager()
        {
        }
        public string SearchText { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public int Totalsubjects { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public SPager(int totalsubjects, int page, int pagesize = 77)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalsubjects / (decimal)pagesize);
            int currentpage = page;

            int startpage = currentpage - 5;
            int endpage = currentpage + 4;

            if (startpage <= 0)
            {
                endpage = endpage - (startpage - 1);
                startpage = 1;
            }
            if (endpage > totalPages)
            {
                endpage = totalPages;
                if (endpage > 10)
                {
                    startpage = endpage - 9;
                }
            }
            Totalsubjects = totalsubjects;
            CurrentPage = currentpage;
            PageSize = pagesize;
            TotalPages = totalPages;
            StartPage = startpage;
            EndPage = endpage;
        }
    }
}
