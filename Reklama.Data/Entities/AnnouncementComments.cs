//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reklama.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnnouncementComments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int AnnouncementId { get; set; }
    
        public virtual Announcement Announcement { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
