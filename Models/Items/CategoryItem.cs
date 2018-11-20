using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.Models.Items
{
    public class CategoryItem
    {
        public virtual Guid Id { get; set; }

        public virtual string Category { get; set; }
    }
}