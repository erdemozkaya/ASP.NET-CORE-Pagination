using Microsoft.AspNetCore.Mvc;
using Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.ViewComponents.Paginate
{
    public class PaginateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PaginateModel paginateModel)
        {
            List<int> list = new List<int>();
            int pageLimit = 5, upperLimit, lowerLimit;
            var currentPage = lowerLimit = upperLimit = Math.Min(paginateModel.ActivePage, paginateModel.TotalCeilNumber);

            for (var b = 1; b < pageLimit && b < paginateModel.TotalCeilNumber;)
            {
                if (lowerLimit > 1)
                {
                    lowerLimit--; b++;
                }
                if (b < pageLimit && upperLimit < paginateModel.TotalCeilNumber)
                {
                    upperLimit++; b++;
                }
            }

            for (var i = lowerLimit; i <= upperLimit; i++)
            {
                if (i == currentPage)
                {
                    list.Add(i);
                }
                else
                {
                    list.Add(i);
                }
            }

            paginateModel.List = list;

            return View("index", paginateModel);
        }
    }
}
