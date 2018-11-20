using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using System;
using Sitecore.Links;
using System.IO;

namespace mysite.Handlers
{
    public class NewPostCreationEventHandler
    {
        private void SaveToFile(Item item)
        {
            var options = LinkManager.GetDefaultUrlOptions();
            options.AlwaysIncludeServerUrl = true;

            var resultString = String.Format("Author Name: {0}, Post Title: {1}, Link to the Post: {2}, Created Date: {3};",
                        item.Statistics.UpdatedBy, item.Fields["Post title"].Value, LinkManager.GetItemUrl(item, options), DateTime.Now.ToString());
            try
            {
                File.AppendAllText(Sitecore.IO.FileUtil.MapPath("~/AdditionFiles/postdata.txt"), resultString + Environment.NewLine);

            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Info(e.Message, this);
            }
        }

        protected void OnNewPostSaved(object sender, EventArgs args)
        {
            if ((args != null))
            {
                Item item = Event.ExtractParameter<Item>(args, 0);
                if (item != null && item.Database.Name.Equals("master") &&
                    item.TemplateID.Equals(new ID("{A655DEEF-241C-42B2-95D2-8DF853449790}"))) // change ID from reply site
                {
                    SaveToFile(item);
                }
            }
        }
    }
}