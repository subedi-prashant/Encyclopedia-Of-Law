using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Models
{
    public class SubjectPager
    {

        public string SearchText { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public int STotalItems { get; private set; }
        public int SCurrentPage { get; private set; }
        public int SPageSize { get; private set; }
        public int STotalPages { get; private set; }
        public int SStartPage { get; private set; }
        public int SEndPage { get; private set; }

        public SubjectPager()
        {
        }
        public SubjectPager(int totalItems, int page, int pagesize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pagesize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }


            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            STotalItems = totalItems;
            SCurrentPage = currentPage;
            SPageSize = pagesize;
            STotalPages = totalPages;
            SStartPage = startPage;
            SEndPage = endPage;


        }
    }
}
