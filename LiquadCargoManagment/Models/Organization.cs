//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiquadCargoManagment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organization
    {
        public long OrganizationID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<long> Telephone { get; set; }
        public Nullable<long> ContactNo { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> Modifiedby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
