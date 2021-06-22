using Bds.TechTest.Models;
using Bds.TechTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bds.TechTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchService _SearchService = new SearchService();
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SearchKeyword")]
        public ActionResult SearchKeyword(SearchModel search)
        {
            List<SearchResult> results = new List<SearchResult>();
            if (string.IsNullOrEmpty(search.Keyword))
            { return View("Index"); }
            _SearchService.InitSerach(search.Keyword);
            results = _SearchService.GetSearchResults();         
            return Ok(results);
        }
    }
}
