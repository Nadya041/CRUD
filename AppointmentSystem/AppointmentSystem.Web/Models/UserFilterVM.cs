using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.Models
{
    public class UserFilterVM
    {
        [DisplayName("Name:")]
        public string Name { get; set; }
        [DisplayName("Email:")]
        public string Email { get; set; }

        public Expression<Func<User, bool>> GenerateFilter()
        {
            return i => (string.IsNullOrEmpty(Name) || i.Name.Contains(Name)) &&
                        (string.IsNullOrEmpty(Email) || i.Email.Contains(Email));
        }
    }
}
