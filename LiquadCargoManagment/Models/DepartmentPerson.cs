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
    
    public partial class DepartmentPerson
    {
        public long PersonalID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BusinessEmail { get; set; }
        public Nullable<long> GroupID { get; set; }
        public Nullable<long> CompanyID { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public string Designation { get; set; }
        public string Cell { get; set; }
        public string PhoneNo { get; set; }
        public string OtherContact { get; set; }
        public string AddressOffice { get; set; }
        public string AddressOther { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsIndividual { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
