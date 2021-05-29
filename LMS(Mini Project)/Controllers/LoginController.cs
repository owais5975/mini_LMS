using LMS_Mini_Project_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LMS_Mini_Project_.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        LMS_DBEntities db = new LMS_DBEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Student_tbl student)
        {
            var data = db.Student_tbl.Where(x => x.St_Email == student.St_Email && x.St_Password == student.St_Password).FirstOrDefault();
            var Id = db.Student_tbl.Where(x => x.St_Email == student.St_Email && x.St_Password == student.St_Password).ToList();
            if(data != null)
            {
                Session["Id"] = Id.FirstOrDefault().St_Id;
                Session["Name"] = Id.FirstOrDefault().St_Name;

                FormsAuthentication.SetAuthCookie(student.St_Email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (!string.IsNullOrEmpty(student.St_Email) && !string.IsNullOrEmpty(student.St_Password))
                {
                    ViewData["Status"] = "<script>alert('Email / Password is Incorrect')</script>";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}