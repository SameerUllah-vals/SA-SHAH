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
    
    public partial class SubcriptionModule
    {
        public long ID { get; set; }
        public Nullable<long> SubcriptionID { get; set; }
        public Nullable<long> MenuID { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Subcription Subcription { get; set; }
    }
}
