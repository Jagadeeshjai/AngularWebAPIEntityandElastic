using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch.Models
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}