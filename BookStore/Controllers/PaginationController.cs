using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BookStore.Models.Pagination;

namespace BookStore.Controllers
{
    public class PaginationController : Controller
    {
        // GET: Pagination
        public ActionResult Index(int page = 1)
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{Request.Url.GetLeftPart(UriPartial.Authority)}//api//phones//{page}").Result;
            var ivm = response.Content.ReadAsAsync<IndexViewModel>().Result;
            return View(ivm);
        }


    }
}