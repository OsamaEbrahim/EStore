using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }

        public OrderDetail()
        {
            SubTotal = 0;
        }
    }


}
