using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Data.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool CheckEmailExist(string email);
    }
}
