using Glass.Mapper.Sc.IoC;
using Glass.Mapper.Sc.Web.Mvc;
using MySite.Models;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class TopItemsController : GlassController
    {
        public ActionResult TopListRendering()
        {
            MultilistField posts = DataSourceItem.Fields["Top Posts"];

            var postsViewModel = new List<Post>();

            Database database = Sitecore.Context.Database;


            foreach (string itemId in posts)
            {
                Item itemToPost = database.GetItem(new ID(itemId));
                var post = SitecoreContextFactory.Default.GetSitecoreContext().Cast<Post>(itemToPost);
                postsViewModel.Add(post);
            }

            return PartialView("/Views/Renderings/TopItems.cshtml", postsViewModel);
        }

    }
}