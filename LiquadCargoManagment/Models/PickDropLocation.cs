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
    
    public partial class PickDropLocation
    {
        public long PickDropID { get; set; }
        public string PickDropCode { get; set; }
        public Nullable<long> AreaID { get; set; }
        public string Address { get; set; }
        public Nullable<long> ProvinceID { get; set; }
        public Nullable<long> LocationTypeID { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedByUserID { get; set; }
        public Nullable<long> ModifiedByUserID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Description { get; set; }
        public string PickDropLocationName { get; set; }
        public Nullable<bool> IsPort { get; set; }
        public string LAT { get; set; }
        public string LON { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> CityID { get; set; }
    }
}
