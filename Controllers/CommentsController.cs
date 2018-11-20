using System.Web;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Linq;
using MySite.Models;
using System.Collections.Generic;
using System;
using Sitecore.SecurityModel;
using Sitecore;
using Sitecore.Globalization;
using Sitecore.Publishing;

namespace MySite.Controllers
{
    public class CommentsController : Controller
    {
        
        public ActionResult ShowComments(string parentItemID = null)
        {

            var comments = new List<Comment>();

            //var childItems = Sitecore.Context.Item.Children.Reverse();

            //var queryComments = String.Format("//*[@@id='{0}']//*[@@templatename='Comment']", Sitecore.Context.Item.ID.ToString());
            var queryComments = String.Format("//*[@@id='{0}']//*[@@templatename='Comment' and @Author !='']", parentItemID ?? Sitecore.Context.Item.ID.ToString());
            var childItems = Sitecore.Context.Database.SelectItems(queryComments);

                foreach (Item childItem in childItems.OrderByDescending(chI => chI["Date"])) // sort by date
                {
                    comments.Add(new Comment(childItem));
                }

            return PartialView("/Views/Renderings/Comments/Comments.cshtml", comments);
        }


        [HttpPost]
        public ActionResult CreateComment(string parentItemID, string dateTime, string author, string commentContent)
        {

            using (new SecurityDisabler())
            {
                var dateTimeParsed = DateTime.Parse(dateTime);

                Database masterDB = Sitecore.Configuration.Factory.GetDatabase("master");
                Item parentItem = masterDB.GetItem(new ID(parentItemID));
                TemplateItem commentItem = masterDB.GetTemplate(new ID("{7C1A6D3D-6329-41C7-81D3-C8749059F639}"));
                var name = String.Format("Comment {0} {1}", author, ((DateTimeOffset)dateTimeParsed).ToUnixTimeSeconds());
                Item newItem = parentItem.Add(name, commentItem);

                newItem.Editing.BeginEdit();
                try
                {
                    newItem.Fields["Author"].Value = author;
                    newItem.Fields["Text"].Value = commentContent;
                    newItem.Fields["Date"].Value = DateUtil.ToIsoDate(dateTimeParsed);

                    newItem.Editing.EndEdit();
                } catch(Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Could not update item " + newItem.Paths.FullPath + ": " + ex.Message, this);
                    newItem.Editing.CancelEdit();
                }

                Database webDB = Sitecore.Configuration.Factory.GetDatabase("web");
                Language siteCoreLanguage = Context.Language;

                //Now we can create publish option
                PublishOptions options = new PublishOptions(masterDB, webDB, PublishMode.SingleItem, siteCoreLanguage, dateTimeParsed);

                //Activate the publish pipline

                Sitecore.Publishing.Publisher publisher = new Sitecore.Publishing.Publisher(options);
                publisher.Options.RootItem = newItem;
                publisher.Options.Deep = true;
                var publishResult = publisher.PublishWithResult();

            }

            return this.ShowComments(parentItemID);
            //return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}