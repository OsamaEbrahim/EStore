using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{

    public class Order
    {
        public int OrderId { get; set; }
        public int OrderStatusID { get; set; }
        public virtual OrderStatus status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<OrderDetail> Details { get; set; }
        public double Total { get; set; }
        public string BlockNo { get; set; }
        public string RoadNo { get; set; }
        public string BuildingNo { get; set; }
        public string FlatNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public Order()
        {
            OrderStatusID = 1;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Total = 0;
        }


    }
}
