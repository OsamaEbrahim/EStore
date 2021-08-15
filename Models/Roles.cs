using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    [NotMapped]
    public class Roles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
