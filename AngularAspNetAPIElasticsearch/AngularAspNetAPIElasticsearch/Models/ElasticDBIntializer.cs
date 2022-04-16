using AngularAspNetAPIElasticsearch.DTOs;
using AutoMapper;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch.Models
{
    public class ElasticDBIntializer
    {
        public static void InitializeESDB()
        {
            var url = ConfigurationManager.AppSettings["elasticUrl"];
            var defaultIndex = ConfigurationManager.AppSettings["elasticIndex"];

            var settings = new ConnectionSettings(new Uri(url))
                .BasicAuthentication(ConfigurationManager.AppSettings["elasticUname"], ConfigurationManager.AppSettings["elasticPwd"])
                .DefaultIndex(defaultIndex);

            //AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
             
            CreateIndex(client, defaultIndex);
            InsertBulkDocuments(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<BookDTO>(m => m
                .Ignore(p => p.Id)
                .Ignore(p => p.PDFName)
            );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            //if indices exists delete and create new
            if (client.Indices.Exists(indexName).Exists)
            {
                client.Indices.Delete(Indices.Index(indexName));
            }
             
            var response=client.Indices.Create(indexName,
                index => index.Settings(se => se
                                    .NumberOfReplicas(1)
                                    .NumberOfShards(2)
                                    ).Map<BookDTO>(x => x.AutoMap()
                                    .Properties(p => p.Text(t => t.Name(n => n.Name).Fielddata(true)))
                                    .Properties(p => p.Text(t => t.Name(n => n.Authors).Fielddata(true)))// Data field true - search fast
                                    .Properties(p => p.Number(t => t.Name(n => n.Id).Index(false)))));//Index false - to apply sort function
        }

        private static void InsertBulkDocuments(IElasticClient client, string indexName)
        {
            using (var ctx = new DatabaseContext())
            {
                List<Book> efBooks = ctx.Books.OrderBy(o => o.Id).ToList();
                var booksDto = Mapper.Map<List<Book>, List<BookDTO>>(efBooks);
                var response=client.BulkAsync(b => b.Index(indexName).IndexMany(booksDto)); // Bulk push data to elastic
            };

        }
    }
}
