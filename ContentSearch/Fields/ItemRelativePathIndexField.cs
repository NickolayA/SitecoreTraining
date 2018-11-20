using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Sites;
using Sitecore.Web;
using System.Web;

namespace MySite.ContentSearch.Fields
{
    public class ItemRelativePathIndexField : IComputedIndexField
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



            
            var urlOptions = LinkManager.GetDefaultUrlOptions();
            urlOptions.Language = indexableItem.Item.Language;
            urlOptions.AlwaysIncludeServerUrl = false;

            //string itemUrl = LinkManager.GetItemUrl(indexableItem.Item, urlOptions);


            string itemUrl = LinkManager.GetItemUrl(indexableItem.Item, urlOptions);
            itemUrl = itemUrl.Substring(3);
            return itemUrl.Substring(itemUrl.IndexOf('/'));     
        }
    }
}