using MySite.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class SocialNetworkLinksController : Controller
    {
        public ActionResult ShowComponent()
        {
            var rc = RenderingContext.CurrentOrNull;
            MultilistField multilistField = rc.Rendering.Item.Fields["SocialNetworks"];

            var socialNetworks = new List<SocialNetwork>();

            foreach(Item item in multilistField.GetItems())
            {
                var socialNetwork = new SocialNetwork();

                ImageField logo = item.Fields["Logo"];
                LinkField networkUrl = item.Fields["Link"];

                socialNetwork.LogoUrl = MediaManager.GetMediaUrl(logo.MediaItem);
                socialNetwork.NetworkUrl = networkUrl.GetFriendlyUrl();
                socialNetworks.Add(socialNetwork);
            }

            return PartialView("/Views/TeamsRenderings/SocialNetworkLinksRendering.cshtml", socialNetworks);

        }
    }
}