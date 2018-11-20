using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;


namespace MySite.Models
{
    public class VenueSearchResultItem : SearchResultItem
    {
        [IndexField("post_title")]
        public string Title { get; set; }

        [IndexField("post_body")]
        public string Body { get; set; }

        [IndexField("category")]
        public string Category { get; set; }

        [IndexField("tags")]
        public string[] Tags { get; set; }

        [IndexField("item_url")]
        public string ItemUrl { get; set; }

        [IndexField("post_image")]
        public string ImagePath { get; set; }

        [IndexField("_language")]
        public string Lang{ get; set; }
    }
}