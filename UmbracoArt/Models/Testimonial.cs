using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoArt.Models
{
    public class Testimonial
    {
        public string Quote { get; set; }
        public string Name { get; set; }

        public Testimonial(string quote, string name)
        {
            Quote = quote;
            Name = name;
        }
    }
}