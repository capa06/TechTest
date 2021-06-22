using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bds.TechTest.Models
{
    public class SearchResult
    {
        public string Url { get; set; }
        public string Title { get; set; }

        public SearchResult(string url, string title)
        {
            Url = url;
            Title = title;
        }
    }
}
