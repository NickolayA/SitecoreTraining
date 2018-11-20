using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySite.Models
{
    public class VenuesSearchCriteria
    {
        public string TitleQuery;
        public string Lang;
        public string[] Categories;
        public string[] Tags;
        public int PageNumber;
        public int ItemsPerPage;
    }
}