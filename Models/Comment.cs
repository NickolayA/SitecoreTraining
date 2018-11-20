using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Globalization;
using System.Web;

namespace MySite.Models
{
    public class Comment : CustomItem
    {
        public Comment(Item item) : base(item) { }
        public string Author { get { return InnerItem["Author"]; } }
        public HtmlString Text { get { return new HtmlString(InnerItem["Text"]); } }
        public string Date { get { return DateTime.ParseExact(Sitecore.DateUtil.IsoDateToServerTimeIsoDate(InnerItem["Date"]), "yyyyMMdd'T'HHmmss", CultureInfo.InstalledUICulture).ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}