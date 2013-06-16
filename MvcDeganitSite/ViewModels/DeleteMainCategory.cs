using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcDeganitSite.Models;

namespace MvcDeganitSite.ViewModels
{
    //used in MainCategoryController/Delete(int id)
    public class DeleteMainCategory
    {
        public MainCategory mainCategory { get; set; }
        public IEnumerable<Recipe> recipes { get; set; }
    }
}