using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.DataTransferObject
{
    public class VendorDTO
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public Nullable<long> VendorTypeID { get; set; }
        public string VendorTypeName { get; set; }
        public string OwnerContactName { get; set; }
        public string OwnerContactNo { get; set; }
        public string PrimaryContactPerson { get; set; }
        public string PrimaryContactNo { get; set; }
        public string SecContactPerson { get; set; }
        public string SecContactNo { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}