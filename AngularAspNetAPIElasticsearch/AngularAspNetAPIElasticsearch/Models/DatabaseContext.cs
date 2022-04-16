using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection") { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Many to Many relationship
            modelBuilder.Entity<Book>()
                        .HasMany<Author>(s => s.Authors)
                        .WithMany(c => c.Books)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("BookId");
                            cs.MapRightKey("AuthorId");
                            cs.ToTable("Book_Author_Mapping");
                        });

        }
    }
}