using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppointmentSystem.Data.Entities;
using AppointmentSystem.Service;
using AppointmentSystem.Web.Models;

namespace AppointmentSystem.Web.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly IActivityService activityService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ActivityController(IActivityService activityService, IMapper mapper, 
            IUserService userService, UserManager<User> userManager)
        {
            this.activityService = activityService;
            this.mapper = mapper;
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            IEnumerable<Activity> items = activityService.GetAll();
            List<ActivityModel> activityModels = mapper.Map<List<ActivityModel>>(items);

            return View(activityModels ?? new List<ActivityModel>());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = activityService.GetById(id);
            ActivityModel activityModel = mapper.Map<ActivityModel>(item);

            return View(activityModel);
        }

        [HttpGet]       
        public IActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "LoggedUser");
            }
            var item = activityService.GetById(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            ActivityModel activityModel = mapper.Map<ActivityModel>(item);

            return View(activityModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActivityModel activityModel)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "LoggedUser");
            }

            if (!ModelState.IsValid)
            {
                return View(activityModel);
            }
            else
            {
                var entity = await userManager.FindByEmailAsync(User.Identity.Name);
                if (User.Identity.Name == entity.Email)
                {
                    Activity activity = mapper.Map<Activity>(activityModel);
                    activityService.Edit(activity);
                }
                else
                    return View(activityModel);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(ActivityModel activityModel)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "LoggedUser");
            }

            var entity = await userManager.FindByEmailAsync(User.Identity.Name);

            if (User.Identity.Name == entity.Email)
            {
                Activity activity = mapper.Map<Activity>(activityModel);
                activityService.Delete(activity);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "LoggedUser");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityModel activityModel)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "LoggedUser");
            }

            if (!ModelState.IsValid)
            {
                return View(activityModel);
            }
            else
            {
                var entity = await userManager.FindByEmailAsync(User.Identity.Name);

                if (User.Identity.Name == entity.Email)
                {                    
                    Activity activity = mapper.Map<Activity>(activityModel);
                    activityService.Insert(activity);
                }
                else
                    return View(activityModel);

                return RedirectToAction("Index");
            }
        }
    }
}