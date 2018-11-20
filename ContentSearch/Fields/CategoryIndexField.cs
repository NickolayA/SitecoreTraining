using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.ContentSearch.Fields
{
    public class CategoryIndexField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, "indexable");
            var indexableItem = indexable as SitecoreIndexableItem;

            if (indexableItem == null)
            {
                Log.Warn(string.Format("{0} : unsupported IIndexable type : {1}", this, indexable.GetType()), this);
                return null;
            }

            using (new Sitecore.Globalization.LanguageSwitcher(indexableItem.Item.Language.Name))
            {

               string itemId = indexableItem.Item["Category"];

               var database =  Sitecore.Configuration.Factory.GetDatabase("web");
               var newItem = database.GetItem(new ID(itemId));


               return newItem.Fields["Category"].Value;
            }
        }
    }
}