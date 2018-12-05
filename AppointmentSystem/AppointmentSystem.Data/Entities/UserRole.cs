using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Data.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public override int UserId { get; set; }      

        public override int RoleId { get; set; }  
    }
}
