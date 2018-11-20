using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;

namespace MySite.ContentSearch.Fields
{
    public class TagsComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, nameof(indexable));


            if (indexable is SitecoreIndexableItem indexableItem)
            {
                using (new Sitecore.Globalization.LanguageSwitcher(indexableItem.Item.Language.Name))
                {
                    MultilistField field = indexableItem.Item.Fields["Tags"];
                    return field.GetItems().Select(x => x.Fields["Tag"].Value) ?? new string[] { };
                }
            }

            Log.Warn($"{this} : unsupported IIndexable type : {indexable.GetType()}", this);
            return null;

        }


    }
}