using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
        [NotMapped]
        public IFormFile Thumbnail { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
