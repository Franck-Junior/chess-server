using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Channel
    {
        public string Cluster { get; set; }
        public string ChannelName { get; set; }
        public string EventName { get; set; }
    }
}