using System.Collections.Generic;

namespace UmbracoArt.Models
{
    public class Testimonials
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public List<Testimonial> Testimoniales { get; set; }

        // render if it has testimonials
        public bool HasTestimonials {get { return Testimoniales != null && Testimoniales.Count > 0; } }

        // use ColumnClass in _Testimonials partial view
        public string ColumnClass {
            get
            {
                switch(Testimoniales.Count)
                {
                    case 1:
                        return "col-md-12";
                    case 2:
                        return "col-md-6";
                    case 3:
                        return "col-md-4";
                    case 4:
                        return "col-md-3";
                    default:
                        return "col-md-3";
                }
            }       
        }

        public Testimonials(string title, string introduction, List<Testimonial> testimonials)
        {
            Title = title;
            Introduction = introduction;
            Testimoniales = testimonials;
        }
    }
}