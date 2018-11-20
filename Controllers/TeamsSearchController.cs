using MySite.ContentSearch.Repositories;
using MySite.Models;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.ContentSearch.Linq;

namespace MySite.Controllers
{
    public class TeamsSearchController : Controller
    {
        private readonly TeamsRepository _repository;

        public TeamsSearchController()
        {
            _repository = new TeamsRepository();
        }

        private VenuesSearchResult FillResultsToShow(SearchResults<VenueSearchResultItem> results, VenuesSearchCriteria searchCriteria)
        {
            return new VenuesSearchResult
            {
                SearchResults = results.Select(r => new VenuePost
                {
                    Title = r.Document.Title,
                    Body = r.Document.Body,
                    Category = r.Document.Category,
                    Tags = r.Document.Tags,
                    ItemUrl = r.Document.ItemUrl,
                    ImagePath = r.Document.ImagePath
                }).ToList(),
                ItemsPerPage = searchCriteria.ItemsPerPage,
                Lang = searchCriteria.Lang,
                TotalResultsCount = results.TotalSearchResults,
                Facets = results.Facets
            };
        }

        private VenuesSearchCriteria FillVenuesSearchCriteria(string[] Categories, string[] Tags, string TitleQuery, string Lang, int ItemsPerPage = 1, int PageNumber = 1)
        {
            return new VenuesSearchCriteria
            {
                TitleQuery = TitleQuery,
                Lang = Lang,
                Categories = Categories ?? new string[] { },
                Tags = Tags ?? new string[] { },
                ItemsPerPage = ItemsPerPage,
                PageNumber = PageNumber
            };
        }

        public ActionResult ShowVenues()
        {

            var postsCountPerPage = RenderingContext.CurrentOrNull.Rendering.Item["PostsCountPerPage"];

            string lang = Sitecore.Context.Language.Name;

            var searchCriteria = FillVenuesSearchCriteria(null, null, null, lang, UInt16.Parse(postsCountPerPage));
            var results = _repository.Get(searchCriteria);

            var resultsToShow = FillResultsToShow(results, searchCriteria);

            return PartialView("/Views/TeamsRenderings/SearchPageBody.cshtml", resultsToShow);
        }

        [HttpPost]
        //public ActionResult ShowSearchedVenues( int PageNumber, int ItemsPerPage, string[] Categories = null, string[] Tags = null, string TitleQuery = null)
        public ActionResult ShowSearchedVenues(int PageNumber, int ItemsPerPage, string[] Categories, string[] Tags, string TitleQuery, string Lang)
        {

            var searchCriteria = FillVenuesSearchCriteria(Categories, Tags, TitleQuery, Lang, ItemsPerPage, PageNumber);

            var results = _repository.Get(searchCriteria);

            var resultsToShow = FillResultsToShow(results, searchCriteria);

            return PartialView("/Views/TeamsRenderings/SearchPageResults.cshtml", resultsToShow);
        }
    }
}