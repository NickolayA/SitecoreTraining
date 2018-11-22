using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using MySite.Models.Items;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System.Collections.Generic;
using System.Linq;

namespace MySite.Models
{
    public class Post
    {
        [SitecoreField("Post title")]
        public virtual string Title { get; set; }
        [SitecoreField("Post body")]
        public virtual string Body { get; set; }
        [SitecoreField("Post image")]
        public virtual Image Image { get; set; }
        public virtual CategoryItem Category { get; set; }
        public virtual IEnumerable<TagItem> Tags { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string UrlToPost { get; set; }


    }
}
//    public class Post : CustomItem
//    {
//        public Post(Item item) : base(item)
//        {
            
//        }

//        public Item itemAlias { get { return InnerItem; } }
//        public string Title { get { return InnerItem["Post title"]; } }
//        public string Body { get { return InnerItem["Post body"]; } }
//        public string Url { get { return LinkManager.GetItemUrl(InnerItem); } }
//        //public string Category { get { return InnerItem["Category"]; } }

//        public string Category
//        {
//            get
//            {
//                return Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(InnerItem["Category"])).Fields["Category"].Value;
//            }
//        }

//        public IEnumerable<string> Tags { get
//            {
//                MultilistField field = InnerItem.Fields["Tags"];
//                var items = field.GetItems();
//                return items.Select(t => t.Fields["Tag"].Value);
//            }
//        }

       


//}
//}