using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;
using RssClientModels.ViewModels;
using RssClientWeb.Managers;

namespace RssClientWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedManager _feedManager;
 
        public HomeController(IFeedManager feedManager)
        {
            this._feedManager = feedManager;
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index(string inputURL)
        {
            if (string.IsNullOrEmpty(inputURL))
            {
                return null;
            }
            var feedDTO = _feedManager.AddFeed(new Uri(inputURL));
            return View(feedDTO);
        }
    }
}