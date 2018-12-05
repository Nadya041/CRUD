using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.Models
{
    public class UserIndexVM
    {
        public UserFilterVM Filter { get; set; }
        public List<User> Items { get; set; }
    }
}
