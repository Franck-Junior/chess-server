using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Player
    {
        public Guid GUID { get; set; }
        public Channel Channel { get; set; }
    }
}