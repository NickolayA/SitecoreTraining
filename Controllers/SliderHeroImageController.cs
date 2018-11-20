using MySite.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class SliderHeroImageController : Controller
    {
        public ActionResult ShowSlider()
        {
            var rc = RenderingContext.CurrentOrNull;

            MultilistField multilistField = rc.Rendering.Item.Fields["Images"];
            var parms = rc.Rendering.Parameters;

            int speed;
            if (!String.IsNullOrEmpty(parms["Speed"]))
            {
                var speedItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(new Guid(parms["Speed"])));
                speed = Int16.Parse(speedItem.Fields["SpeedNum"].Value);
            }
            else
            {
                speed = Int16.Parse(Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{FFA51B78-EE6B-484F-A0D8-950147D13F85}")).Fields["Slider Speed"].ToString());
            }
            var heightStyle = !String.IsNullOrEmpty(parms["Max Slide Height"]) ? String.Format("max-height: {0}px", parms["Max Slide Height"]) : null;

            var slider = new Slider();
            slider.Images = new List<Item>();
            slider.Images.Add(multilistField.GetItems().First());
            slider.Height = heightStyle;
            slider.Speed = speed;

            return PartialView("/Views/Renderings/Slider.cshtml", slider);
        }
    }
}