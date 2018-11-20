using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult ShowNavigation()
        {
            Item homepage = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var children = homepage.GetChildren();

            var navigationLablesSources = children.ToList();
            navigationLablesSources.Insert(0, homepage);

            return PartialView("/Views/Renderings/Navigation/Navigation.cshtml", navigationLablesSources);
        }

        public ActionResult ShowLanguageTumbler()
        {
            //var langItems = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{64C4F646-A3FA-4205-B98E-4DE2C609B60F}")).GetChildren();
            var langItems = Sitecore.Context.Item.Languages;
            return PartialView("/Views/Renderings/Navigation/LanguageTumbler.cshtml", langItems);
        }
    }
}