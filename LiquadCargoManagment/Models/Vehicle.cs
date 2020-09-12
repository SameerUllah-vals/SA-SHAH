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
    
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.Bilties = new HashSet<Bilty>();
        }
    
        public long VehicleID { get; set; }
        public string Code { get; set; }
        public string RegNo { get; set; }
        public string ChasisNo { get; set; }
        public string EngineNo { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Height { get; set; }
        public string DimensionUnitType { get; set; }
        public long VehicleTypeID { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string BodyType { get; set; }
        public string ManufacturingYear { get; set; }
        public string ManufacturerName { get; set; }
        public Nullable<System.DateTime> PurchasingDate { get; set; }
        public Nullable<long> PurchasingAmount { get; set; }
        public string PurchaseFromName { get; set; }
        public string PurchaseFromDetail { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerNIC { get; set; }
        public Nullable<long> PrincipleCompanyId { get; set; }
        public Nullable<int> Comission { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> Amount2 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsOwnVehicle { get; set; }
        public string Make { get; set; }
        public Nullable<long> MaximumLoadingCapacity { get; set; }
        public string OwnershipStatus { get; set; }
        public string Documents { get; set; }
        public Nullable<long> LoadingLimitNHA { get; set; }
        public Nullable<long> OwnCompanyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bilty> Bilties { get; set; }
        public virtual OwnCompany OwnCompany { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}
