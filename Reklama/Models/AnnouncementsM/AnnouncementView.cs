using System;
using System.Collections.Generic;
using Domain.Entity.Announcements;
using Domain.Entity.Shared;

namespace Reklama.Models.AnnouncementsM
{
    public class AnnouncementView
    {
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }

        public Currency Currency { get; set; }

        public decimal? Price { get; set; }

        //public Section? Section { get; set; }
        public string Description { get; set; }

        public int ViewsCount { get; set; }

        public IList<ImageViewModel> Images { get; set; }

        public string PublishDate { get; set; }
    }
}