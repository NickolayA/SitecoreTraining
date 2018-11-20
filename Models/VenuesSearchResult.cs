using Sitecore.ContentSearch.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.Models
{
    public class VenuesSearchResult
    {
        public List<VenuePost> SearchResults { get; set; }
        public FacetResults Facets { get; set; }
        public int TotalResultsCount { get; set; }
        public int ItemsPerPage { get; set; }
        public string Lang { get; set; }
    }
}