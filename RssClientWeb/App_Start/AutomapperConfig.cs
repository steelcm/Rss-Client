using AutoMapper;
using RssClientModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;

namespace RssClientWeb
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<SyndicationItem, FeedSummaryItem>()
                .ForMember(o => o.Title, c => c.MapFrom(d => d.Title.Text))
                .ForMember(o => o.PublicationDateTime, c => c.MapFrom(d => d.PublishDate.DateTime))
                ;
        }
    }
}