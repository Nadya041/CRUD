using AppointmentSystem.Data.Context;
using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Data.Repositories
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(AppointmentSystemContext dbContext)
            : base(dbContext)
        {

        }
    }
}
