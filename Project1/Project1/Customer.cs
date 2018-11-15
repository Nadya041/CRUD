using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1
{
    public class Customer
    {
        public int Id { get; set; }
        [Required,StringLength (45)]
        public string Name { get; set; }
    }
}
