using Encyclopedia_Of_Laws.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Views.Shared.Components.SearchBar1
{
    public class SearchBar1ViewComponent : ViewComponent
    {
        public SearchBar1ViewComponent()
        {

        }
        public SPager SearchPager { get; set; }
        public IViewComponentResult Invoke(SPager SearchPager)
        {
            return View("Default1", SearchPager);
        }
    }
}
