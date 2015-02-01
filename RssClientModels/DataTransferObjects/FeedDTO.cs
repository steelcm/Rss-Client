using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssClientModels.DataTransferObjects
{
    public class FeedDTO
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Uri Feed { get; set; }
        public Uri Link { get; set; }
    }
}
