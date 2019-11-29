using System.Data.Entity;
using Domain.Repository.Admin;
using Domain.Entity.Admin;
using Infrastructure.Helpers;
using Microsoft.Ajax.Utilities;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.Admin
{
    public class PopularAnnoucementModel: BaseModel<PopularAnnouncement>, IPopularAnnouncementRepository
    {
        int? region =null;
        public PopularAnnoucementModel()
        {
            region = ConfigHelper.GetRegion;
        }
        public override IQueryable<PopularAnnouncement> Read()
        {
            var query = Context.Set<PopularAnnouncement>()
                .Include(x => x.Announcement)
                .Include(x=>x.Announcement.City);
                
            if (region.HasValue)
            {
                query = query.Where(a => a.Announcement != null && a.Announcement.City!=null && a.Announcement.City.Id == region);
            }
            return query;
        }
    }
}