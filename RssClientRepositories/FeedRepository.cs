using RssClientModels.DataTransferObjects;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssClientRepositories
{
    public class FeedRepository
    {
        private readonly PooledRedisClientManager _redisManager;

        public FeedRepository()
        {
            this._redisManager = new PooledRedisClientManager("localhost:6379"); // inject this
        }

        public long Add(FeedDTO feed)
        {
            _redisManager.ExecAs<FeedDTO>(o =>
            {
                feed.Id = o.GetNextSequence();
                o.Store(feed);
            });
            return feed.Id;
        }

        public IList<FeedDTO> List()
        {
            IList<FeedDTO> allFeeds = new List<FeedDTO>();
            _redisManager.ExecAs<FeedDTO>(o =>
            {
                allFeeds = o.GetAll();
            });
            return allFeeds;
        }

        public FeedDTO Get(long id)
        {
            FeedDTO feed = null;
            _redisManager.ExecAs<FeedDTO>(o =>
            {
                feed = o.GetById(id);
            });
            return feed;
        }
    }
}
