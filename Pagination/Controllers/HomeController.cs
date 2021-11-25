using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pagination.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region user pagination without JavaScript
        public IActionResult Users(int page = 1)
        {
            // küçük bir kontrol :)
            page = page <= 0 ? 1 : page;

            PaginateModel paginateModel = new PaginateModel
            {
                ActivePage = page,
                PaginateModelClickEvent = new PaginateModelClick
                {
                    IsJavaScript = false,
                    Url = Url.Action("Users","Home")
                }
            };

            paginateModel.Total = 21;

            var jsonStirng = System.IO.File.ReadAllText("data.json");
            List<Users> users = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonStirng))
                .Skip((int)paginateModel.NumberOfObjectsPerPage * (page - 1))
                .Take((int)paginateModel.NumberOfObjectsPerPage).ToList();
            return View(new Tuple<List<Users>, PaginateModel>(users,paginateModel));
        }
        #endregion

        #region user pagination with JavaScript
        public IActionResult UsersWithJavascript()
        {
            PaginateModel paginateModel = new PaginateModel
            {
                ActivePage = 1,
                PaginateModelClickEvent = new PaginateModelClick
                {
                    IsJavaScript = true,
                    PaginateClickName = "PaginateClick"
                }
            };

            paginateModel.Total = 21;

            var jsonStirng = System.IO.File.ReadAllText("data.json");
            List<Users> users = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonStirng))
                .Skip((int)paginateModel.NumberOfObjectsPerPage * (paginateModel.ActivePage - 1))
                .Take((int)paginateModel.NumberOfObjectsPerPage).ToList();
            return View(new Tuple<List<Users>, PaginateModel>(users, paginateModel));
        }

        [HttpPost]
        public IActionResult UsersWithJavascript(int page = 1)
        {
            page = page <= 0 ? 1 : page;
            PaginateModel paginateModel = new PaginateModel();

            paginateModel.Total = 21;

            var jsonStirng = System.IO.File.ReadAllText("data.json");
            List<Users> users = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonStirng))
                .Skip((int)paginateModel.NumberOfObjectsPerPage * (page - 1))
                .Take((int)paginateModel.NumberOfObjectsPerPage).ToList();
            return PartialView("JavaScriptRender/Paginate/UsersWithJavascriptRender", users);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
