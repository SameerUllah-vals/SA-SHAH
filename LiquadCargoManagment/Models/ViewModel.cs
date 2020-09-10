using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class ViewModel
    {
        public Role Role { get; set; }
        public List<NavMenu> lstForms { get; set; }
    }

    public class WorkOrderViewModel
    {
        public WorkOrder WorkOrder { get; set; }
        public List<WorkOrder> lstWorkOrder { get; set; }
        public List<OrderDetail> lstOrderDetail { get; set; }
    }
}