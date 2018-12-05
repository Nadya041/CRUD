using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Data.Context;
using AppointmentSystem.Data.Entities;
using AppointmentSystem.Service;
using AppointmentSystem.Web.Models;

namespace AppointmentSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager; 

        public UserController(IUserService userService, IMapper mapper, UserManager<User> userManager)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index(UserIndexVM model)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "LoggedUser");
            }

            model.Filter = model.Filter ?? new UserFilterVM();
            Expression<Func<User, bool>> filter = model.Filter.GenerateFilter();

            model.Items = userService.GetAll(filter).ToList();  

            return View(model ?? new UserIndexVM());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Profile(int id)
        {
            UserModel userModel = null;
            if (id != 0)
            {
                User user = userService.GetById(id);
                userModel = mapper.Map<UserModel>(user);
            }
            else
            {
                User user = await userManager.FindByEmailAsync(User.Identity.Name);
                userModel = mapper.Map<UserModel>(user);
            }

            return View(userModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            User item = await userManager.FindByIdAsync(id.ToString());
            UserModel userModel = mapper.Map<UserModel>(item);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            userModel.OldPasswordHash = userModel.PasswordHash;

            return View(userModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UserModel item)
        {       
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            else
            {
                var emailExists = true;
                var e = await userManager.FindByEmailAsync(item.Email);
                if (e != null)
                {
                    if (e.Id == item.Id)
                        emailExists = false;
                    else
                        emailExists = true;
                }

                if (emailExists)
                {
                    return View(item);
                }
                else
                {
                    item.UserName = item.Email;
                    User user = mapper.Map<User>(item);                   
                    user.SecurityStamp = Guid.NewGuid().ToString();       
                    
                    if (item.PasswordHash != item.OldPasswordHash)
                    {
                        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, item.PasswordHash);
                    }
                    else
                        user.PasswordHash = item.OldPasswordHash;                   
                                        
                    userService.EditById(item.Id, user);

                    if (User.IsInRole("Admin"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Profile");                   
                }
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(UserModel item)
        {
            User user = mapper.Map<User>(item);
            userService.Delete(user);

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index");
            else
                return RedirectToAction("LogOut", "LoggedUser");
        }

        private void SetValues(User user)
        {
            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}