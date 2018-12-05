using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Service
{
    public interface IUserService : IBaseService<User>
    {
        bool CheckEmailExistance(string email);
    }
}
