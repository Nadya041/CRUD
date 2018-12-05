using AutoMapper;
using AppointmentSystem.Data.Entities;
using AppointmentSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegistrationModel>();

            CreateMap<RegistrationModel, User>();

            CreateMap<User, LogInModel>();

            CreateMap<LogInModel, User>();

            CreateMap<User, UserModel>();

            CreateMap<UserModel, User>();

            CreateMap<Appointment, AppointmentModel>();

            CreateMap<AppointmentModel, Appointment>();

            CreateMap<Activity, ActivityModel>();

            CreateMap<ActivityModel, Activity>();

            CreateMap<AppointmentActivity, Activity>();
        }
    }
}
