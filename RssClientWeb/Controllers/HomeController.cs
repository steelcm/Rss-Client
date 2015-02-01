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
        public ActionResult Index()
        {
            var allFeeds = _feedManager.List();
            return View(allFeeds);
        }

        [HttpPost]
        public ActionResult Index(string newfeedurl)
        {
            if (string.IsNullOrEmpty(newfeedurl))
            {
                return null;
            }
            var feedDTO = _feedManager.Add(new Uri(newfeedurl));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult List(long id)
        {
            var viewModel = _feedManager.Get(id);
            return View(viewModel);
        }
    }
}