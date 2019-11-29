using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using Domain.Enums;

namespace Reklama.Models.AnnouncementsM
{
    public class AnnouncementEdit
    {
        public int? Id { get; set; }
        
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        
        public int CurrencyId { get; set; }
        
        [Range(0 ,Int32.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }
        
        public HttpPostedFileBase[] Images { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public Regions Region { get; set; }

    }
}