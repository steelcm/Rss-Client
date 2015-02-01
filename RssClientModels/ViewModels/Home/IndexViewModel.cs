using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssClientModels.ViewModels.Home
{
    public class IndexViewModel
    {
        public IList<FeedSummaryItem> Items { get; set; }
    }
}
