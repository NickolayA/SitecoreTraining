using Sitecore.Data.Items;
using System.Collections.Generic;


namespace MySite.Models
{
    public class Slider
    {
        public int Speed { get; set; }
        public string Height { get; set; }
        public List<Item> Images { get; set; }
    }
}