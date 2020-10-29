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
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using IronPdf;

namespace Auth4.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
     


        // GET: Files
        public ActionResult Index(int? DeptClassId)
        {

            if (DeptClassId.Equals(null))
            {
                return Redirect("/Admin/Index");
            }
            FilesRepo filesRepo = new FilesRepo(context);
            IEnumerable<Files> enumerable = filesRepo.GetClassFiles(DeptClassId);
            FilesViewModel filesviewmodel = new FilesViewModel()
            {
                files = enumerable
            };
            ViewBag.DeptClassId = DeptClassId;

            return View(filesviewmodel);
        }

        [HttpPost]
        public ActionResult UploadFile(FormCollection form)
        {
               FilesRepo filesRepo = new FilesRepo(context);
        DeptRepo deptRepo = new DeptRepo(context);
            //string Username = form["txtFileName"];
            //file  = form.Get("profilepic");
            //string Password = form["password"];
            byte[] file = null;
            string fileContentType = null;
            var guid = Guid.NewGuid();
            string classid = form["txtClassDeptId"];
            var DeptClassId = Int16.Parse(classid);
            string fileName = null; 
     

            string folderName = @"C:\Proj";
            string name = deptRepo.GetName(DeptClassId);

            if (Request != null)
            {
                HttpPostedFileBase file1 = Request.Files["File"];

                if ((file1 != null) && (file1.ContentLength > 0) && !string.IsNullOrEmpty(file1.FileName))
                {
                    fileName = file1.FileName;

                    fileContentType = file1.ContentType;
                    byte[] fileBytes = new byte[file1.ContentLength];
                    file1.InputStream.Read(fileBytes, 0, Convert.ToInt32(file1.ContentLength));
                    file = fileBytes;
                }
            }



            // To create a string that specifies the path to a subfolder under your
            // top-level folder, add a name for the subfolder to folderName.
            //\proj\className\file.pdf or tx
            string pathString = System.IO.Path.Combine(folderName,name);
            string pathString1 = System.IO.Path.Combine(pathString, form["txtFileName"]);
            /// create the directory
           
            System.IO.Directory.CreateDirectory(pathString);

            if (!System.IO.File.Exists(pathString1))
            {
                // File.WriteAllBytes(Server.MapPath(filePath), imgArray);
                try
                {

                    System.IO.File.WriteAllBytes(pathString1, file);
                }catch(Exception e)
                {
                    e.ToString();
                    Console.WriteLine(e.ToString());
                }
           

            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.",pathString);
          
            }
          
            var data = new Files()
            {

                FileName = form["txtFileName"],
                ContentType = fileContentType,
                FilePathDirectory = pathString1,
                file = file,
                CreatedBy = (string)User.Identity.GetUserId(),
                DeptClassId = Int16.Parse(classid),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
            filesRepo.Add(data);



            return Redirect("/Files?DeptClassId=" + DeptClassId);
        }


        [HttpPost]
        public async Task<JObject> DeleteFile(int id)
        {
            FilesRepo filesRepo = new FilesRepo(context);
            ///create jsonObject
            JObject jObject = new JObject();
            /// get the file path
            string filepath = filesRepo.GetfilePath(id);
            if (String.IsNullOrWhiteSpace(filepath))
            {
                jObject.Add("status", 500);
                jObject.Add("message", "file path wasnt found");


            }
            /// delete from local file
            if (System.IO.File.Exists(filepath))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(filepath);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
           
                }
            }
           await filesRepo.DeleteFile(id);
            jObject.Add("status", 200);
            jObject.Add("message", "success");

            return jObject;
        }

        [HttpPost]
        public void Addsignature(int id)
        {
            FilesRepo filesRepo = new FilesRepo(context);


            //IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            //// add a footer too
            //Renderer.PrintOptions.Footer.DrawDividerLine = true;
            //Renderer.PrintOptions.Footer.FontFamily = "Arial";
            //Renderer.PrintOptions.Footer.FontSize = 10;
            //Renderer.PrintOptions.Footer.LeftText = "SIGNED BY";
            //Renderer.PrintOptions.Footer.RightText = "WANGOCHO";

            //// add footer to the pdf

            string filepath = filesRepo.GetfilePath(id);
            PdfDocument Pdf = PdfDocument.FromFile(filepath);
            SimpleHeaderFooter footer = new SimpleHeaderFooter();
            footer.LeftText = "signed by";
            footer.RightText ="nanii";
            footer.Spacing = 5;
            footer.DrawDividerLine = true;

            Pdf.AddFooters(footer, false, null);
            Pdf.SaveAs(filepath);
        }


        [HttpGet]
        public JObject GetPdfByte(int FileId)
        {
            FilesRepo filesRepo = new FilesRepo(context);
            byte[] bytes = System.IO.File.ReadAllBytes(filesRepo.GetfilePath(FileId));
            JObject jObject = new JObject();
            jObject.Add("byte", bytes);

            return jObject;
        }

        public ActionResult ViewPdf(int FileId)
        {


            TempData["File.Id"] = FileId.ToString();

            return View();
        }
        [HttpGet]
        public PartialViewResult ForwardFile(int fileId)
        {
            FilesRepo filesRepo = new FilesRepo(context);
            string fileowner = filesRepo.GetById(fileId).CreatedBy;



            ViewBag.Users = context.Users.Where(item => item.Id !=fileowner)
                                   .Select(c => new SelectListItem() { Text = c.UserName, Value = c.Id });

            return PartialView(filesRepo.GetById(fileId));

        }



    }


}