using AngularAspNetAPIElasticsearch.DTOs;
using AngularAspNetAPIElasticsearch.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularAspNetAPIElasticsearch
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Book, BookDTO>()
                .ForMember(x=>x.Authors,opt=>opt.MapFrom(s=> String.Join(", ", s.Authors.Select(m => m.Name).ToArray())))
                .ForMember(x => x.PDFName, opt => opt.MapFrom(s => s.PDFPath));
            });
        }
    }
}   
