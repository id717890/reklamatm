using Domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reklama.Models.AnnouncementsM
{
    public class MobileFilterParams
    {
        public MobileFilterParams()
        {
            TakeCount = 10;
            TakeFrom = 0;
        }
        public string SearchString { get; set; }
        public SectionType SectionType { get; set; }
        public int TakeFrom { get; set; }
        public int TakeCount { get; set; }
        public IList<Currency> AvailableCurrencies { get; set; }
    }
}