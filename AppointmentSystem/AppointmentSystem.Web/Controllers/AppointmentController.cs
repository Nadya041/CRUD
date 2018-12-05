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
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;
        private readonly IActivityService activityService;
        private readonly UserManager<User> userManager;

        public AppointmentController(IAppointmentService appointmentService, IMapper mapper, IActivityService activityService,
            UserManager<User> userManager)
        {
            this.appointmentService = appointmentService;
            this.mapper = mapper;
            this.activityService = activityService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entity = await userManager.FindByEmailAsync(User.Identity.Name);

            IEnumerable<Appointment> items = appointmentService.GetAll().Where(src => src.UserId == entity.Id);
            List<AppointmentModel> appointmentModels = mapper.Map<List<AppointmentModel>>(items);

            return View(appointmentModels ?? new List<AppointmentModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            AppointmentModel appointmentModel = new AppointmentModel();
            IEnumerable<Activity> activities = activityService.GetAll();
            appointmentModel.Activities = activities.ToList();

            return View(appointmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentModel appointmentM)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Activity> activities = activityService.GetAll();
                appointmentM.Activities = activities.ToList();

                return View(appointmentM);
            }
            else
            {
                var entity = await userManager.FindByEmailAsync(User.Identity.Name);

                if (User.Identity.Name == entity.Email)
                {
                    if (appointmentM.StartDateTime < DateTime.Now || appointmentM.StartDateTime > appointmentM.EndDateTime ||
                        appointmentM.EndDateTime < DateTime.Now)
                    {
                        IEnumerable<Activity> activities = activityService.GetAll();
                        appointmentM.Activities = activities.ToList();

                        return View(appointmentM);
                    }
                    else
                    {
                        appointmentM.UserId = entity.Id;
                        Appointment appointment = mapper.Map<Appointment>(appointmentM);
                        appointment.Activities = new List<AppointmentActivity>();
                        if (appointmentM.ChosenActivities != null)
                        {
                            foreach (var item in appointmentM.ChosenActivities)
                            {
                                AppointmentActivity appointmentActivity = new AppointmentActivity();
                                Activity activity = activityService.GetById(item);
                                appointmentActivity.ActivityId = item;
                                appointmentActivity.Activity = activity;
                                appointmentActivity.Appointment = appointment;
                                appointment.Activities.Add(appointmentActivity);
                            }
                        }

                        appointmentService.Insert(appointment);
                    }
                }
                else
                {
                    IEnumerable<Activity> activities = activityService.GetAll();
                    appointmentM.Activities = activities.ToList();

                    return View(appointmentM);
                }
                    
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = appointmentService.GetById(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            AppointmentModel appointmentModel = mapper.Map<AppointmentModel>(item);
            List<Activity> activities = activityService.GetAll().ToList();
            appointmentModel.Activities = activities;
            appointmentModel.ChosenActivities = new List<int>();

            List<AppointmentActivity> aa = item.Activities.ToList();

            for (int i = 0; i < aa.Count; i++)
            {
                appointmentModel.ChosenActivities.Add(aa[i].ActivityId);
            }
            if (appointmentModel.ChosenActivities.Count == 0)
                appointmentModel.ChosenActivities = null;

            return View(appointmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentModel appointmentModel)
        {
            if (!ModelState.IsValid)
            {
                appointmentModel.Activities = activityService.GetAll().ToList();

                return View(appointmentModel);
            }
            else
            {
                var entity = await userManager.FindByEmailAsync(User.Identity.Name);
                var app = appointmentService.GetById(appointmentModel.Id);

                if (User.Identity.Name == entity.Email)
                {
                    if (appointmentModel.StartDateTime != app.StartDateTime && appointmentModel.StartDateTime < DateTime.Now
                        || appointmentModel.EndDateTime != app.EndDateTime && appointmentModel.EndDateTime < DateTime.Now
                        || appointmentModel.StartDateTime > appointmentModel.EndDateTime)
                    {
                        appointmentModel.Activities = activityService.GetAll().ToList();

                        return View(appointmentModel);
                    }
                    else
                    {
                        appointmentModel.UserId = entity.Id;
                        Appointment appointment = mapper.Map<Appointment>(appointmentModel);

                        if(appointmentModel.ChosenActivities != null)
                        {
                            foreach (var item in appointmentModel.ChosenActivities)
                            {
                                AppointmentActivity appointmentActivity = new AppointmentActivity();
                                Activity activity = activityService.GetById(item);
                                appointmentActivity.ActivityId = item;
                                appointmentActivity.Activity = activity;
                                appointmentActivity.Appointment = appointment;
                                appointment.Activities.Add(appointmentActivity);
                            }
                        }
                        
                        appointmentService.Edit(appointment);
                    }
                }
                else
                {
                    appointmentModel.Activities = activityService.GetAll().ToList();

                    return View(appointmentModel);
                }                   

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(AppointmentModel appointmentM)
        {
            var entity = await userManager.FindByEmailAsync(User.Identity.Name);
            if (User.Identity.Name == entity.Email)
            {
                appointmentM.UserId = entity.Id;
                Appointment appointment = mapper.Map<Appointment>(appointmentM);
                appointmentService.Delete(appointment);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var item = appointmentService.GetById(id);
            AppointmentModel appointmentModel = mapper.Map<AppointmentModel>(item);

            List<Activity> activities = activityService.GetAll().ToList();

            appointmentModel.Activities = new List<Activity>();
            List<AppointmentActivity> aa = item.Activities.ToList();

            for (int i = 0; i < aa.Count; i++)
            {
                appointmentModel.Activities.Add(aa[i].Activity);
            }

            return View(appointmentModel);
        }
    }
}