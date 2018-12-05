using AppointmentSystem.Data.Entities;
using AppointmentSystem.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository userRepository;       

        public UserService(IUserRepository userRepository)
            :base(userRepository)
        {
            this.userRepository = userRepository;            
        }

        public bool CheckEmailExistance(string email)
        {
            if (userRepository.CheckEmailExist(email))
                return true;
            else
                return false;
        }
    }
}
