using Microsoft.AspNetCore.Mvc;
using Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.Controllers
{
    public class SharedController : Controller
    {
        #region sayfalama
        public IActionResult PaginateRefresh(PaginateModel paginateModel)
        {
            return ViewComponent("Paginate", new { paginateModel = paginateModel});
        }
        #endregion
    }
}
