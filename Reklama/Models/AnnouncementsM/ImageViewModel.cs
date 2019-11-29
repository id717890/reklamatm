using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.AnnouncementsM
{
    public class ImageViewModel
    {
        public string Link { get; set; }
        public bool? IsTitular { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}