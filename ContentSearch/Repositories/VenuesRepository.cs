using MySite.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.ContentSearch.Repositories
{
    public class VenuesRepository : IPostsRepository
    {

        private readonly string IndexName = $"mysite_web_index";
        //private readonly string IndexName;
        private ISearchIndex _index;

        private ISearchIndex Index => _index ?? (_index = ContentSearchManager.GetIndex(IndexName));

        private IProviderSearchContext _context;

        private IProviderSearchContext Context => _context ?? (_context = Index.CreateSearchContext());


        public SearchResults<VenueSearchResultItem> Get(VenuesSearchCriteria args)
        {
            var searchPredicate = PredicateBuilder.True<VenueSearchResultItem>();

            
            if (!String.IsNullOrEmpty(args.TitleQuery))
            {
                searchPredicate = searchPredicate.And(vsri => vsri.Title.Contains(args.TitleQuery) || vsri.Tags.Contains(args.TitleQuery));
            }

            if (args.Categories.Any())
            {
                var categoryPredicate = PredicateBuilder.False<VenueSearchResultItem>();
                foreach (var category in args.Categories)
                {
                    categoryPredicate = categoryPredicate.Or(y => y.Category.Equals(category));
                }

                searchPredicate = searchPredicate.And(categoryPredicate);   
            }

            if (args.Tags.Any())
            {
                var tagsPredicate = PredicateBuilder.False<VenueSearchResultItem>();
                foreach(var tag in args.Tags)
                {
                    tagsPredicate = tagsPredicate.Or(vsri => vsri.Tags.Contains(tag));
                }

                searchPredicate = searchPredicate.And(tagsPredicate);
            }


            var result = Context.GetQueryable<VenueSearchResultItem>()
                .Where(searchPredicate)
                .Where(v => v.Language.Equals(args.Lang))
                .FacetOn(v => v.Tags)
                .FacetOn(v => v.Category)
                
                .Page(args.PageNumber-1, args.ItemsPerPage)
                .GetResults();

            return result;
            
        }
    }
}