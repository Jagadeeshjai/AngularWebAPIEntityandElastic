using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch.Models
{
    public class DatabaseInitializer:  DropCreateDatabaseAlways<DatabaseContext>//when app launches create the database from scratch
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
            ElasticDBIntializer.InitializeESDB();// sync the data from sql to elastic
        }

        //creates data in Book and Author tables and link with least two authors.
        protected override void Seed(DatabaseContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;

            string[] authors = new string[] { "W. Frank Ableson", "Charlie Collins", "Robi Sen" , "Gojko Adzic" , "Tariq Ahmed", "Faisal Abid", "Dan Orlando", "John C. Bland II", "Joel Hooks" };
             
            IList<Author> authorsList = new List<Author>();
            for (var i = 0; i < authors.Length; i++)
            {
                authorsList.Add(new Author { Id = i + 1, Name = authors[i] });
            }

            context.Authors.AddRange(authorsList);
            
            int seedValue = 1;
            int limitValue = 20000; // create 20k news records

            IList<Book> booksList = new List<Book>();
            Random rand = new Random();
            while (seedValue <= limitValue)
            {
                Book book = new Book() { Id = 1, Name = "Testing Book  " + seedValue.ToString(), Published = rand.Next(100) <= 28, PDFPath= "pdf_sample_"+ rand.Next(1, 3) + ".pdf" };
                book.Authors=authorsList.OrderBy(i => rand.Next()).Take(rand.Next(2, 4)).ToList();
                booksList.Add(book);
                seedValue++;
            }

            context.Books.AddRange(booksList);
            context.ChangeTracker.DetectChanges();
            base.Seed(context);

        }


    }

}


