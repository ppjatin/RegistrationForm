using RegistrationForm.Models;
using RegistrationForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace RegistrationForm.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly UserRegistrationService _service;

        public UserRegistrationController(UserRegistrationService service)
        {
            _service = service;
        }

        public ActionResult Details()
        {
            var userRegistration = _service.GetUserRegistrationById(0);
            if (userRegistration == null)
            {
                return HttpNotFound();
            }
            return Json(userRegistration, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSate()
        {
            var statelist = _service.GetAllSate().Select(s => new
            {
                Value = s.Id, 
                Text = s.StateName 
            }).ToList();
            return Json(statelist, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCity(int id)
        {
            var citylist = _service.GetAllCity(id).Select(s => new
            {
                Id = s.Id,
                CityName = s.CityName
            }).ToList();
            return Json(citylist, JsonRequestBehavior.AllowGet);


        }

        public ActionResult AddEditUserRegistrationDetail(INUserRegistration user) {

            if (user.Id == 0) {
                _service.InsertUserRegistration(user);
                return Json(JsonRequestBehavior.AllowGet);
            }
            else
            {

                _service.UpdateUserRegistration(user);
                return Json(JsonRequestBehavior.AllowGet);
            }
            

        }


        public ActionResult DeleteUserRegistration(int id)
        {

            _service.DeleteUserRegistration(id);
            return Json(JsonRequestBehavior.AllowGet);
        }


       
    }
}