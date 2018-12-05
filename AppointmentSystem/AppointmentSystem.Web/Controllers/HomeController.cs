using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppointmentSystem.Data.Entities;
using AppointmentSystem.Service;
using AppointmentSystem.Web.Models;
using AppointmentSystem.Web.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<User> myUserManager;        
        private readonly SignInManager<User> signInManager;
        private readonly IMailService mailService;        

        public HomeController(IUserService userService, IMapper mapper, UserManager<User> myUserManager,
            SignInManager<User> signInManager, IMailService mailService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.myUserManager = myUserManager;
            this.signInManager = signInManager;
            this.mailService = mailService; 
        }

        [HttpPost]
        public async Task<IActionResult> Index(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                User user = mapper.Map<User>(model);
                user.UserName = user.Email;

                var logInRes = await signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);
                if (logInRes.Succeeded)
                {
                    return RedirectToAction("Index", "LoggedUser");
                }
                else
                    return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RedirectIndex(string returnUrl)
        {
            LogInModel model = new LogInModel();
            model.RedirectUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RedirectIndex(LogInModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                User user = mapper.Map<User>(model);
                user.UserName = user.Email;

                var logInRes = await signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);
                if (logInRes.Succeeded)
                {
                    return Redirect(model.RedirectUrl);
                }
                else
                    return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var emailExists = userService.CheckEmailExistance(model.Email);

            if (emailExists)
            {
                return View(model);
            }
            else
            {
                User user = mapper.Map<User>(model);
                SetValues(user);

                await myUserManager.CreateAsync(user, model.Password);
                var addToRoleResult = await myUserManager.AddToRoleAsync(user, "User");

                await mailService.SendAsync(user.Email, "Registration", BodyMail(user.Name));

                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetValues(User user)
        {
            user.UserName = user.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
        }

        private string BodyMail(string name)
        {
            return "Hello, " + name + "! You successfully made your registration!";
        }
    }
}
