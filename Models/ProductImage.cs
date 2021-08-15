using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
