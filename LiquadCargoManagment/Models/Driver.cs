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
    
    public partial class Driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            this.AssignDrivers = new HashSet<AssignDriver>();
        }
    
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public Nullable<long> CellNo { get; set; }
        public Nullable<long> OtherContact { get; set; }
        public Nullable<long> HomeNo { get; set; }
        public string Address { get; set; }
        public Nullable<long> NIC { get; set; }
        public string IdentityMark { get; set; }
        public Nullable<System.DateTime> NICIssueDate { get; set; }
        public Nullable<System.DateTime> NICExpiryDate { get; set; }
        public string LicenseNo { get; set; }
        public string LicenseCategory { get; set; }
        public Nullable<System.DateTime> LicenseIssueDate { get; set; }
        public Nullable<System.DateTime> LicenseExpiryDate { get; set; }
        public string LicenseIssuingAuthority { get; set; }
        public string LicenseStatus { get; set; }
        public string EmergencyContactName { get; set; }
        public Nullable<long> EmergencyContactNo { get; set; }
        public string ContactRelation { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> DriverImageID { get; set; }
        public string Document { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignDriver> AssignDrivers { get; set; }
    }
}
