using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch.DTOs
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public string Authors { get; set; }
        public string PDFName { get; set; }
         
    }
}
