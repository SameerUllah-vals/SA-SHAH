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
    
    public partial class VehicleExpens
    {
        public long ID { get; set; }
        public Nullable<long> BiltyID { get; set; }
        public long ExpenseTypeId { get; set; }
        public Nullable<long> BankBranch { get; set; }
        public string PaymentMethod { get; set; }
        public string ChequeNo { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public Nullable<double> Amount { get; set; }
    
        public virtual Bank Bank { get; set; }
        public virtual ExpensesType ExpensesType { get; set; }
    }
}
