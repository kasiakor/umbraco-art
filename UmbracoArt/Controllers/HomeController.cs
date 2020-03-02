﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoArt.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using Archetype.Models;

namespace UmbracoArt.Controllers
{
    public class HomeController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Home/";
        public ActionResult RenderFeatured()
        {
            // create a model object which is a list of featured items
            List<FeaturedItem> model = new List<FeaturedItem>();

            // goes to homePage, goes up the tree and down to find home, first instance
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            // gets featuredItems property as an Archetype Model
            ArchetypeModel featuredItems = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");
            // loops through the Archetype model, through fieldsets there to get name, cat etc
            foreach(ArchetypeFieldsetModel fieldset in featuredItems)
            {
                string imageUrl = "";
                int imageId = fieldset.GetValue<int>("image");
                var mediaItem = Umbraco.Media(imageId);
                imageUrl = mediaItem.Url;
                int pageId = fieldset.GetValue<int>("page");
                IPublishedContent linkedToPage = Umbraco.TypedContent(pageId);
                string linkUrl = linkedToPage.Url;
            // pass them through here
                model.Add(new FeaturedItem(fieldset.GetValue<string>("name"), fieldset.GetValue<string>("category"), imageUrl, linkUrl));
            }
            return PartialView(PARTIAL_VIEW_FOLDER + "_Featured.cshtml", model);
        }
        public ActionResult RenderServices()
        {

            return PartialView(PARTIAL_VIEW_FOLDER + "_Services.cshtml");
        }
        public ActionResult RenderBlog()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Blog.cshtml");
        }
        public ActionResult RenderClients()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Clients.cshtml");
        }
    }
}