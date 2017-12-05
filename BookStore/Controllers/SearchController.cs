using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Helpers;
using BookStore.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using SearchResult = BookStore.Models.SearchResult;

namespace BookStore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string data)
        {
            return View(SearchHelper.Search(data));
        }
    }
}