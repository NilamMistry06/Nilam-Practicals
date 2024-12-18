using System;
using System.Net;
using System.Security;
using System.Web.Mvc;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;

namespace SharePoint___ASPDOTNETWebAPP.Controllers
{
    public class HomeController : Controller
    {
        public ClientContext GetContext()
        {
            string siteUrl = "https://nilammistry.sharepoint.com/sites/dev";

            SecureString passWord = new SecureString();
            foreach (char c in "Nilam@123#".ToCharArray()) passWord.AppendChar(c);
            using (ClientContext context = new ClientContext(siteUrl))
            {
                context.Credentials = new SharePointOnlineCredentials("NilamMistry@nilammistry.onmicrosoft.com", passWord);
                return context;
            }
        }
        public ActionResult Index()
        {
            //test();
            //GetFields();
            //DeleteAListItem();
            //UpdateAListItem();
            //AddAListItem();
            foreach (ListItem listItem in GetListItems())
            {
                ViewBag.Content = ViewBag.Content + ", " + listItem["Title"];
            }
            return View();
        }

        public ListItemCollection GetListItems()
        {
            ClientContext context = GetContext();
            List list = context.Web.Lists.GetByTitle("Test List");
            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            ListItemCollection items = list.GetItems(query);
            context.Load(items);
            context.ExecuteQuery();
            return items;
        }

        public void AddAListItem()
        {
            ClientContext context = GetContext();
            List list = context.Web.Lists.GetByTitle("Test List");
            ListItemCreationInformation newItemInfo = new ListItemCreationInformation();
            ListItem newItem = list.AddItem(newItemInfo);

            newItem["Title"] = "Test New Item";
            newItem["Description"] = "This is a new item added via CSOM.";

            newItem.Update();
            context.ExecuteQuery();
        }

        public void UpdateAListItem()
        {
            ClientContext context = GetContext();
            List list = context.Web.Lists.GetByTitle("Test List");
            ListItem itemToUpdate = list.GetItemById(1);
            itemToUpdate["Title"] = "Updated New Title";
            itemToUpdate["Description"] = "Again Updated description for the item.";
            //itemToUpdate["Date & Time"] = DateTime.Now;

            itemToUpdate.Update();
            context.ExecuteQuery();
        }

        public void DeleteAListItem()
        {
            ClientContext context = GetContext();
            List list = context.Web.Lists.GetByTitle("Test List");
            ListItem itemToDelete = list.GetItemById(4);
            itemToDelete.DeleteObject();
            context.ExecuteQuery();
        }

        public void GetFields()
        {
            ClientContext context = GetContext();
            List list = context.Web.Lists.GetByTitle("Test List");
            context.Load(list.Fields);
            context.ExecuteQuery();

            foreach (Field field in list.Fields)
            {
                ViewBag.Content = ViewBag.Content + ", " + field.InternalName;
            }
        }
























        private const string TenantId = "b0db661d-de27-4a3a-8543-3337aa3e51ea"; // Directory (tenant) ID from Azure AD
        private const string ClientId = "c9761a64-c86a-40ca-9ca8-9a130c1b7c5e"; // Application (client) ID from Azure AD
        private const string ClientSecret = "62z8Q~CW68WEdXOLCyLmTInlZZo~p.OdJP13fbCb"; // Client Secret from Azure AD
        private const string SharePointSiteUrl = @"https://nilammistry.sharepoint.com"; // SharePoint site URL
        public void test()
        {
            //public ActionResult Index()
            //{
            // Authenticate using Azure AD app
            var authContext = ConfidentialClientApplicationBuilder.Create(ClientId)
                .WithClientSecret(ClientSecret)
                .WithAuthority($"https://login.microsoftonline.com/{TenantId}")
                .Build();

            var scopes = new string[] { $"{SharePointSiteUrl}/.default" };

            var authResult = authContext.AcquireTokenForClient(scopes).ExecuteAsync().Result;


            using (var clientContext = new ClientContext(SharePointSiteUrl))
            {
                clientContext.ExecutingWebRequest += (sender, e) =>
                {
                    e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + authResult.AccessToken;
                };
                Web web = clientContext.Web;

                clientContext.Load(web);

                clientContext.ExecuteQuery();

                ViewBag.Title = web.Title;

                ViewBag.Description = web.Description;
            }
            //    //===========================================

            //SecureString passWord = new SecureString();

            //foreach (char c in "Nilam@123#".ToCharArray()) passWord.AppendChar(c);

            //ClientContext context = new ClientContext(@"https://nilammistry.sharepoint.com/sites/dev");
            //context.Credentials = new SharePointOnlineCredentials("NilamMistry@nilammistry.onmicrosoft.com", passWord);

            //Web web = context.Web;

            //context.Load(web);

            //context.ExecuteQuery();

            //    ViewBag.Title = web.Title;

            //    ViewBag.Description = web.Description;


            //    //context = new ClientContext(@"https://nilammistry.sharepoint.com/sites/dev_api/web/lists");
            //    //context.Credentials = new SharePointOnlineCredentials("NilamMistry@nilammistry.onmicrosoft.com", passWord);
            //    //web = context.Web;

            //    //context.Load(web.Lists,lists => lists.Include(list => list.Title,list => list.Id));
            //    //context.ExecuteQuery();

            //    //foreach (List list in web.Lists)
            //    //{
            //    //    ViewBag.Content = ViewBag.Content + ", " + list.Title;
            //    //}


            //    return View();
        }
    }
}
