using AutoMapper;
using Moq;
using NUnit.Framework;
using RssClientModels.DataTransferObjects;
using RssClientModels.ViewModels;
using RssClientRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace RssClientWeb.Managers
{
    [TestFixture]
    public class FeedManagerTests
    {
        private FeedManager _feedManager;

        [SetUp]
        public void Setup()
        {
            var mockNullFeedRepository = new Mock<FeedRepository>();
            this._feedManager = new FeedManager(mockNullFeedRepository.Object);
        }

        [Test]
        public void AddUri_GiveNullString_ReturnsNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _feedManager.Add(null));
        }

        [Test]
        public void AddUri_GiveEmptyString_ReturnsNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _feedManager.Add(String.Empty));
        }

        [Test]
        public void AddUri_GivenInvalidUri_ReturnsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _feedManager.Add("loremipsum"));
        }

        [Test]
        public async void AddUri_GivenValidUriWithout200_ReturnArgumentException()
        {
            //Assert.Throws<ArgumentException>(() => _feedManager.Add("loremipsum"));
        }
    }

    public class FeedManager : IFeedManager
    {

        private readonly FeedRepository _feedRepository;

        public FeedManager(FeedRepository feedRepository)
        {
            this._feedRepository = feedRepository;
        }

        public FeedDTO Add(String uri)
        {
            if (string.IsNullOrEmpty(uri)) // validate not null or empty
                throw new ArgumentNullException("uri", "The uri parameter cannot be null or empty");

            Uri feedUri;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out feedUri)) // validate is URI
                throw new ArgumentException("uri", "The uri parameter must be a valid absolute URI path");






             
            var feedObj = Load(feedUri);
            var feedDTO = new FeedDTO()
            {
                Name = feedObj.Title.Text,
                Description = feedObj.Description.Text,
                Feed = feedUri,
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