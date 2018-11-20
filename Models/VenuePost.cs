using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.Models
{
    public class VenuePost
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Category { get; set; }
        //public string Category
        //{
        //    get { return Category; }
        //    set { Category = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(value)).Fields["Category"].Value; }
        //}
        public string[] Tags { get; set; }
        public string ItemUrl { get; set; }
        public string ImagePath { get; set; }
    }
}