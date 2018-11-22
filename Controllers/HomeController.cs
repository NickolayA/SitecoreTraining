using System.Web.Mvc;
using Sitecore.Data.Items;
using MySite.Models;
using System.Collections.Generic;
using System;
using Glass.Mapper.Sc.Web.Mvc;
using Glass.Mapper.Sc.IoC;

namespace MySite.Controllers
{
    //public class HomeController : Controller
    public class HomeController : GlassController
    {
        public ActionResult ShowDetails()
        {
            var post = GetContextItem<Post>();

            return PartialView("/Views/Renderings/DetailsPageBody.cshtml", post);
        }

        public ActionResult ListAllItems(string s = null)
        {
            var itemsModel = new List<Post>();
            var queryPostWithTitle = String.Format("{0}//*[@@templatename='Details Page' or @@templatename='Post Page' and contains(@Post title, '{1}')]", Sitecore.Context.Site.RootPath.Replace("-", "#-#"), s != null ? s : "");

            var itemsToModel = ContextItem.Database.SelectItems(queryPostWithTitle);
            
            foreach (Item item in itemsToModel)
            {
                var post = SitecoreContextFactory.Default.GetSitecoreContext().Cast<Post>(item);
                itemsModel.Add(post);
            }

            return PartialView("/Views/Renderings/ListPageBody.cshtml", itemsModel);
        }

    }
}


