using MySite.Models;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySite.Controllers.TeamsControllers
{
    public class TeamsSliderController : Controller
    {
        public ActionResult ShowSlider()
        {
            throw new Exception("I'm exception from TeamsSite");
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

            var slider = new Slider();
            slider.Images = multilistField.GetItems().ToList();
            slider.Height = "0";
            slider.Speed = speed;

            return PartialView("/Views/TeamsRenderings/TeamsSlider.cshtml", slider);
        }
    }
}