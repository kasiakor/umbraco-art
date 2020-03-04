using System;
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
        private const int MAXIMUM_TESTIMONIALS = 4;
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
           // render blog, get the home page
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");

            // gets these two properties on the home page
            string title = homePage.GetPropertyValue<string>("latestBlogPostsTitle");
            // comes as HTML
            string introduction = homePage.GetPropertyValue("latestBlogPostsIntroduction").ToString();

            //it has created the model
            LatestBlogPost model = new LatestBlogPost(title, introduction);

            // pass the model when rendering the blog
            return PartialView(PARTIAL_VIEW_FOLDER + "_Blog.cshtml", model);
        }
        public ActionResult RenderTestimonials()
        {
            // render blog, get the home page
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");

            // gets these two properties on the home page
            string title = homePage.GetPropertyValue<string>("testimonialsTitle");
            // comes as HTML
            string introduction = homePage.GetPropertyValue("testimonialsIntroduction").ToString();


            // get testimonials from Umbraco
            List<Testimonial> testimonials = new List<Testimonial>();

            //populate the list we need to get the value
            ArchetypeModel testimonialList = homePage.GetPropertyValue<ArchetypeModel>("testimonialList");
            if(testimonialList != null)
            {

                foreach(ArchetypeFieldsetModel testimonial in testimonialList.Take(MAXIMUM_TESTIMONIALS))
                {
                    string name = testimonial.GetValue<string>("name");
                    string quote = testimonial.GetValue<string>("quote");
                    testimonials.Add(new Testimonial(quote, name));
                }
            }

            // pass testimonials to Testimonials model
            //it has created the model
            Testimonials model = new Testimonials(title, introduction, testimonials);
            return PartialView(PARTIAL_VIEW_FOLDER + "_Testimonials.cshtml", model);
        }
    }
}