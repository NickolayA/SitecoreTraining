using MySite.Models;
using Sitecore.ContentSearch.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite.ContentSearch.Repositories
{
    public interface IPostsRepository
    {
        SearchResults<VenueSearchResultItem> Get(VenuesSearchCriteria args);
    }
}
