using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsServices.Models
{
    public class Story
    {
        public long Id { get; set; }
        public string By { get; set; }
        public long Descendants { get; set; }
        public long[] Kids { get; set; }
        public long Score { get; set; }
        public long Time { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
