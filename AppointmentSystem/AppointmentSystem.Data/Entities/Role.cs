using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Data.Entities
{
    public class Role : IdentityRole<int>, IBaseEntity
    {
        public override int Id { get; set; }        
    }
}
