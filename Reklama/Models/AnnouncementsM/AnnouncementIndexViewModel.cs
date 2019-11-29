using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Enums;


namespace Reklama.Models.AnnouncementsM
{
    public class AnnouncementIndexViewModel
    {
        public AnnouncementIndexViewModel()
        {
            AnnouncementList = new List<AnnouncementView>();
        }
        public Regions CurrentRegion { get; set; }
        public string UserPhone { get; set; }
        public string SearchString { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IList<AnnouncementView> AnnouncementList { get; set; }
        public int CurrentSectionId { get; set; }
        public IList<SectionViewModel> SectionsList { get; set; }
    }
}