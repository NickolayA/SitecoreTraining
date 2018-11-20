using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines.HttpRequest;

namespace MySite.PipelinesN
{
    public class Set404Status : Sitecore.Pipelines.HttpRequest.HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if(Sitecore.Context.Items["Is404Page"] != null && Sitecore.Context.Items["Is404Page"].ToString() == "true")
            {
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}