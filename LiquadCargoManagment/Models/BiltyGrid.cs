using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class BiltyGrid
    {


        //public long ID { get; set; }
        //public long OrderNo { get; set; }
        //public System.DateTime OrderDate { get; set; }
        //public Nullable<long> BillTo { get; set; }
        //public Nullable<long> Sender { get; set; }
        //public Nullable<double> QTY { get; set; }
        //public Nullable<double> TotalWeight { get; set; }



        public long ID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public long OrderNo { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<long> BillTo { get; set; }
        public Nullable<long> Sender { get; set; }
        public Nullable<long> VehicleID { get; set; }
        public Nullable<double> Freight { get; set; }
        public Nullable<double> VehicleExpensesGrandTotal { get; set; }
        public Nullable<double> DieselExpensesGrandTotal { get; set; }
        public Nullable<double> TotalLiter { get; set; }
        public Nullable<double> OrderDetailsTotalQTY { get; set; }
        public Nullable<double> OrderDetailsTotalWeight { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual CustomerCompany CustomerCompany { get; set; }
        public virtual CustomerCompany CustomerCompany1 { get; set; }
        public virtual Vehicle Vehicle { get; set; }





        public Nullable<long> BiltyID { get; set; }
        public long Receiver { get; set; }
        public long ProductBrokerID { get; set; }
        public Nullable<long> ProductTypeId { get; set; }
        public Nullable<long> ProductId { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> Unit { get; set; }
        public Nullable<double> QTY { get; set; }
        public Nullable<double> TotalWeight { get; set; }
        public string Remarks { get; set; }

        public virtual Area Area { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductBroker ProductBroker { get; set; }
    }
}