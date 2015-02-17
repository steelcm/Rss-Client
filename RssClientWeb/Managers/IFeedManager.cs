using RssClientModels.DataTransferObjects;
using RssClientModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace RssClientWeb.Managers
{
    public interface IFeedManager
    {
        FeedDTO Add(String uri);
        SyndicationFeed Load(Uri uri);
        IList<FeedDTO> List();
        IList<FeedSummaryItem> Get(long siteId);

    }
}
