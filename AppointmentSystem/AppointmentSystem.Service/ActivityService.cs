using AppointmentSystem.Data.Entities;
using AppointmentSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Service
{
    public class ActivityService : BaseService<Activity>, IActivityService
    {
        public ActivityService(IActivityRepository activityRepository)
            : base(activityRepository)
        {

        }
    }
}
