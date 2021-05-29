using LMS_Mini_Project_.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Mini_Project_.Controllers
{
    
    public class HomeController : Controller
    {
        LMS_DBEntities db = new LMS_DBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Assignment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Assignment(Submission_tbl submission)
        {

            string fileName = Path.GetFileNameWithoutExtension(submission.Sub_File.FileName);
            string fileExtension = Path.GetExtension(submission.Sub_File.FileName);
            HttpPostedFileBase postedFile = submission.Sub_File;
            int FileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".doc" || fileExtension.ToLower() == ".docx" || fileExtension.ToLower() == ".rtf")
            {
                if (FileSize <= 1000000)
                {
                    fileName = fileName + fileExtension;

                    submission.Sub_File_path = "~/Files/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
                    submission.Sub_File.SaveAs(fileName);

                    db.Submission_tbl.Add(submission);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Message"] = "<script>alert('Submitted')</script>";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Message"] = "<script>alert('Failed')</script>";
                    }
                }
                else
                {
                    ViewData["SizeMessage"] = "<script>alert('File size exceed should be less than 1 MB')</script>";
                }
            }
            else
            {
                ViewData["ExtensionMessage"] = "<script>alert('File Extension not Supported')</script>";
            }


            return View();
        }

        public ActionResult Submissions()
        {
            int Id = Convert.ToInt32(Session["Id"]);
            var data = db.Submission_tbl.Where(x => x.St_Id == Id).ToList();
            return View(data);
        }

        public ActionResult Unsubmit(int id)
        {
            try
            {
                var del = db.Submission_tbl.Where(x => x.Sub_Id == id).FirstOrDefault();
                db.Submission_tbl.Remove(del);
                db.SaveChanges();
                return RedirectToAction("Submissions");
            }
            catch
            {
                return View();
            }
        }

        public FileResult Show(int id)
        {
            var data = db.Submission_tbl.Where(x => x.Sub_Id == id).ToList();

            string path = Server.MapPath("~/Files/");
            string fileName = Path.GetFileName(data.FirstOrDefault().Sub_File_path);

            string fullpath = Path.Combine(path, fileName);
            return File(fullpath, "application/pdf");
        }
    }
}