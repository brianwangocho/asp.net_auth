using DevExpress.Web.Mvc;
using Auth4.Models;
using Auth4.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Auth4.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
      
        // GET: Files
        public ActionResult Index(int DeptClassId)
        {

            if (DeptClassId == null)
            {
                return Redirect("/Admin/Index");
            }
            FilesRepo filesRepo = new FilesRepo(context);
            FilesViewModel filesviewmodel = new FilesViewModel()
            {
                files = filesRepo.GetClassFiles(DeptClassId)
        };
            ViewBag.DeptClassId = DeptClassId;

            return View(filesviewmodel);
        }

        [HttpPost]
        public ActionResult UploadFile(FormCollection form)
        {
            FilesRepo filesRepo = new FilesRepo(context);
            //string Username = form["txtFileName"];
            //file  = form.Get("profilepic");
            //string Password = form["password"];
            byte[] file = null;
            string fileContentType = null;
            var guid = Guid.NewGuid();
            string classid = form["txtClassDeptId"];
            var DeptClassId = Int16.Parse(classid);
            if (Request != null)
            {
                HttpPostedFileBase file1 = Request.Files["File"];

                if ((file1 != null) && (file1.ContentLength > 0) && !string.IsNullOrEmpty(file1.FileName))
                {
                    string fileName = file1.FileName;
                    fileContentType = file1.ContentType;
                    byte[] fileBytes = new byte[file1.ContentLength];
                    file1.InputStream.Read(fileBytes, 0, Convert.ToInt32(file1.ContentLength));
                    file = fileBytes;
                }
            }

                var data = new Files()
            {
                   
                FileName = form["txtFileName"],
                ContentType = fileContentType,
                file = file,
                CreatedBy = (string)User.Identity.GetUserId(),
                DeptClassId = Int16.Parse(classid),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
            filesRepo.Add(data);

            return Redirect("/Files?DeptClassId=" + DeptClassId);
        }

     
    }


}