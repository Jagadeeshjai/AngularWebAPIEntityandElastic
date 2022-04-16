using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Published { get; set; }
        public string PDFPath { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}

