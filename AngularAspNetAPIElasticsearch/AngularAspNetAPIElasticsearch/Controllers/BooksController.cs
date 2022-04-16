using AngularAspNetAPIElasticsearch.DTOs;
using AngularAspNetAPIElasticsearch.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AngularAspNetAPIElasticsearch.Controllers
{
    public class BooksController : ApiController
    {
        //returns the data from EF
        [HttpGet]
        public async Task<IHttpActionResult> GetBooks(int pageNo = 1,int pageSize=10, string search="")
        {
            search = !string.IsNullOrEmpty(search) ? search.ToLower() : "";
            int rowNo = (pageNo - 1) * pageSize;
            int count = 0;
            List<BookDTO> books = new List<BookDTO>();
            await Task.Run(() =>
            {
                using (var ctx = new DatabaseContext())
                {
                    count = ctx.Books.Where(s => search == "" || (s.Name.ToLower().Contains(search) || s.Authors.Any(i => i.Name.ToLower().Contains(search)))).Count();
                    List<Book> booksList = ctx.Books.Where(s => search == "" || (s.Name.ToLower().Contains(search) || s.Authors.Any(i => i.Name.ToLower().Contains(search)))).OrderBy(o => o.Id).Skip(rowNo).Take(pageSize).ToList();
                    var result = Mapper.Map<List<Book>, List<BookDTO>>(booksList);
                    books =result;
                }
            });

            return Ok(new { books, count });
        }

        //returns the data from elastic
        [HttpGet]
        public async Task<IHttpActionResult> GetESBooks(int pageNo = 1, int pageSize = 10, string search = "")
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["elasticUrl"]))
                .BasicAuthentication(ConfigurationManager.AppSettings["elasticUname"], ConfigurationManager.AppSettings["elasticPwd"])
                .DefaultIndex(ConfigurationManager.AppSettings["elasticIndex"]);
            ElasticClient esClient = new ElasticClient(settings);

            int rowNo = (pageNo - 1) * pageSize;
            int count = 0;
            //querystring query
            var countResponse = await esClient.CountAsync<BookDTO>(s => s
                            .Query(q => q
                              .QueryString(qs => qs
                                .Fields(fs => fs
                                        .Field(p => p.Name)
                                        .Field(p => p.Authors)
                                    )
                                .Query("*" + search + "*")
                              )
                            ));
            count = (int)countResponse.Count;
            var response = await esClient.SearchAsync<BookDTO>(s => s.From(rowNo).Size(pageSize)
                            .Query(q => q
                              .QueryString(qs => qs
                                .Fields(fs => fs
                                        .Field(p => p.Name)
                                        .Field(p => p.Authors)
                                    )
                                .Query("*"+ search + "*")
                              )
                            )
                            .Sort(ss => ss
                    .Ascending(f => f.Id)));
            return Ok(new { books= response.Documents.ToList(), count });
        }
    }
}


