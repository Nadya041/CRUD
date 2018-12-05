using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Data.Context;
using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSystem.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {        
        public UserRepository(AppointmentSystemContext dbContext)
            : base(dbContext)
        {
            
        }

        public bool CheckEmailExist(string email)
        {
            var check = DbSet.FirstOrDefaultAsync(u => u.Email == email).Result;
            if (check != null)
                return true;
            else
                return false;
        }
        

        public override void DeleteById(int id)
        {
            var entity = DbSet.AsNoTracking().SingleOrDefault(m => m.Id == id);

            DbSet.Remove(entity);
            Context.SaveChanges();                   
        }

        public override void Delete(User user)
        {
            User item = GetById(user.Id);            

            Context.Entry(item).State = EntityState.Deleted;
            Context.SaveChanges();
        }
       

        public override void EditById(int id, User item)
        {
            User user = GetById(id);
            
            user.Email = item.Email;
            user.UserName = item.Email;
            user.PasswordHash = item.PasswordHash;
            user.Phone = item.Phone;
            user.Name = item.Name;

            DbSet.Update(user);
            Context.SaveChanges();
        }
    }
}
