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
    
    public partial class ContainerType
    {
        public long ContainerTypeID { get; set; }
        public string ContainerType1 { get; set; }
        public Nullable<long> Size { get; set; }
        public Nullable<long> Wheels { get; set; }
        public string BodyType { get; set; }
        public string Code { get; set; }
        public string DimensionUnitType { get; set; }
        public Nullable<double> LowerDeckInnerLength { get; set; }
        public Nullable<double> LowerDeckInnerWidth { get; set; }
        public Nullable<double> LowerDeckInnerHeight { get; set; }
        public Nullable<double> UpperDeckInnerLength { get; set; }
        public Nullable<double> UpperDeckInnerWidth { get; set; }
        public Nullable<double> UpperDeckInnerHeight { get; set; }
        public Nullable<double> LowerDeckOuterLength { get; set; }
        public Nullable<double> LowerDeckOuterWidth { get; set; }
        public Nullable<double> LowerDeckOuterHeight { get; set; }
        public Nullable<double> UpperDeckOuterLength { get; set; }
        public Nullable<double> UpperDeckOuterWidth { get; set; }
        public Nullable<double> UpperDeckOuterHeight { get; set; }
        public Nullable<double> UpperPortionInnerLength { get; set; }
        public Nullable<double> UpperPortionInnerwidth { get; set; }
        public Nullable<double> UpperPortionInnerHeight { get; set; }
        public Nullable<double> LowerPortionInnerLength { get; set; }
        public Nullable<double> LowerPortionInnerWidth { get; set; }
        public Nullable<double> LowerPortionInnerHeight { get; set; }
        public string Description { get; set; }
        public Nullable<double> TareWeight { get; set; }
        public Nullable<double> TareWeightUnit { get; set; }
        public Nullable<double> CubicCapacity { get; set; }
        public string CubicCapacityUnit { get; set; }
        public Nullable<double> PayLoadWeight { get; set; }
        public string PayLoadWeightUnit { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedByUserID { get; set; }
        public Nullable<long> ModifiedByUserID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}