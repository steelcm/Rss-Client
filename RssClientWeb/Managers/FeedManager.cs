using AutoMapper;
using RssClientModels.DataTransferObjects;
using RssClientModels.ViewModels;
using RssClientRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace RssClientWeb.Managers
{
    public class FeedManager : IFeedManager
    {

        private readonly FeedRepository _feedRepository;

        public FeedManager(FeedRepository feedRepository)
        {
            this._feedRepository = feedRepository;
        }

        public FeedDTO Add(Uri uri)
        {
            var feedObj = Load(uri);
            var feedDTO = new FeedDTO()
            {
                Name = feedObj.Title.Text,
                Description = feedObj.Description.Text,
                Feed = uri,
                Link = feedObj.Links.Any(o => o.RelationshipType.Equals("alternate", StringComparison.InvariantCultureIgnoreCase)) ?
                    feedObj.Links.First(o => o.RelationshipType.Equals("alternate", StringComparison.InvariantCultureIgnoreCase)).Uri : null
            };
            var id = _feedRepository.Add(feedDTO);
            return feedDTO;
        }

        public IList<FeedDTO> List()
        {
            return _feedRepository.List();
        }

        public SyndicationFeed Load(Uri uri)
        {
            // todo move this into a xml repo
            using (var reader = XmlReader.Create(uri.ToString()))
            {
                var feed = SyndicationFeed.Load(reader);
                if (feed != null)
                    return feed;
                throw new NotImplementedException();
            }
        }


        public IList<FeedSummaryItem> Get(long siteId)
        {
            var feedDTO = _feedRepository.Get(siteId);
            var syndicationFeed = Load(feedDTO.Feed);
            return Mapper.Map<IList<FeedSummaryItem>>(syndicationFeed.Items);
        }
    }
}