using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using Bds.TechTest.Models;



namespace Bds.TechTest.Services
{
    public class SearchService
    {
        public List<SearchResult> results;
        private GoogleSearch googleSearch;
        private BingSearch bingSearch;
        public SearchService()
        {
            results = new List<SearchResult>();
        }

        public void InitSerach(string keyword)
        {
            googleSearch = new GoogleSearch(keyword);
            bingSearch = new BingSearch(keyword);            
        }

        public List<SearchResult> GetSearchResults()
        {
            if (googleSearch.GetResults(results.Count))
            {
                results.AddRange(googleSearch.Results);
            }
            if(bingSearch.GetResults(results.Count))
            {
                results.AddRange(bingSearch.Results);
            }
            return results;
        }

        public abstract class SearchEngine
        {
            internal string Url;
            public List<SearchResult> Results;
            public abstract bool GetResults(int count);
        }

        /// <summary>
        /// For search for google
        /// </summary>
        public class GoogleSearch:SearchEngine
        {
            public GoogleSearch(string keyword)
            {
                Url = "https://www.google.co.uk/search?hl=en&q=" + keyword.Trim() + "&start=";
            }

            public override bool GetResults(int count)
            {
                bool retVal = false;
                Results = new List<SearchResult>();
                HtmlWeb web = new HtmlWeb();
                try
                {
                    Url = Url + count;
                    string htmlResponse = GetHtmlResponse(Url);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlResponse);
                    List<HtmlNode> linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@href,'/url')]").ToList();
                    foreach (HtmlNode hn in linkNodes)
                    {
                        string url = hn.Attributes["href"].Value.Replace("/url?q=", "").Split("&amp;")[0];
                        //Filter out results
                        if(url.Contains("google"))
                        {
                            continue;
                        }
                        string title = hn.SelectSingleNode("span")==null?string.Empty : hn.SelectSingleNode("span").InnerText;
                        Results.Add(new SearchResult(url, title));
                    }
                   
                    
                    retVal = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return retVal;
            }
        }

        /// <summary>
        /// For serach with Bing
        /// </summary>
        public class BingSearch : SearchEngine
        {
            public BingSearch(string keyword)
            {
                Url = "https://www.bing.com/search?q=" + keyword + "&first=";
            }
            public override bool GetResults(int count)
            {
                bool retVal = false;
                Results = new List<SearchResult>();
                HtmlWeb web = new HtmlWeb();
                try
                {
                    Url = Url + count;
                    string htmlResponse = GetHtmlResponse(Url);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlResponse);
                    List<HtmlNode> linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@href,'http')]").ToList();
                    foreach (HtmlNode hn in linkNodes)
                    {
                        string url = hn.Attributes["href"].Value.Replace("/url?q=", "").Split("&amp;")[0];
                        //Filter out results
                        if (url.Contains("microsoft"))
                        {
                            continue;
                        }
                        string title = hn.InnerText;
                        Results.Add(new SearchResult(url, title));
                    }
                    retVal = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return retVal;
            }
        }

        /// <summary>
        /// Get the html as string from the serach engine url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetHtmlResponse(string url)
        {
            Uri uri = new Uri(url);
            string htmlResponse = "";
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                htmlResponse = webClient.DownloadString(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return htmlResponse;
        }
    }
}


