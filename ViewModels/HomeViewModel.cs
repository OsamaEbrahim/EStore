using EStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<Product> RecentlyAdded { get; set; }
        public ICollection<Product> LowInStock { get; set; }
        public ICollection<Product> MostSelling { get; set; }


    }
}
