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
    
    public partial class Banner
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; }
        public string LinkFlash { get; set; }
        public string LinkImage { get; set; }
        public string Url { get; set; }
    }
}
