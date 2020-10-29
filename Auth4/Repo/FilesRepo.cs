using Auth4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Auth4.Repo
{
    public class FilesRepo
    {
        private ApplicationDbContext _context { get; set; }


        public FilesRepo(ApplicationDbContext context)
        {
            _context = context;

        }
        /// <summary>
        /// add files
        /// </summary>
        /// <param name="files"></param>
        public void Add(Files files)
        {
            try
            {
                _context.Files.Add(files);
                _context.SaveChanges();
            }catch(System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} throws Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
           
        }
        /// <summary>
        /// delete files asynchronously hence making it async Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteFile(int id)
        {
            var classFile = _context.Files.FirstOrDefault(x => x.Id == id);

            /// delete file
            _context.Files.Remove(classFile);
            await _context.SaveChangesAsync();
        }

        public string GetfilePath(int id)
        {
            string filepath = _context.Files.FirstOrDefault(e => e.Id == id).FilePathDirectory;

            return filepath;

        }

        public Files GetById(int id)
        {
            Files files = _context.Files.FirstOrDefault(e => e.Id == id);

            return files;
        }


        /// <summary>
        /// get alll files
        /// </summary>
        /// <param name="DeptclassId"></param>
        /// <returns></returns>
        public IEnumerable<Files> GetClassFiles(int? DeptclassId)
        {
          var classFiles =   _context.Files.Where(e => e.DeptClassId == DeptclassId).ToList<Files>();
           return classFiles;
        }

       
     }
}