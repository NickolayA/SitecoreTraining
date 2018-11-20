using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.Models.Items
{
    public class TagItem
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        public virtual string Tag { get; set; }
    }
}