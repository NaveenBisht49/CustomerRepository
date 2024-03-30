using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_1.Data;
using Project_1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project_1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ProjectDbContext _context;
        private readonly IWebHostEnvironment _webenv;

        public CustomerController(ProjectDbContext context , IWebHostEnvironment webenv)
        {
           _context = context;
            _webenv = webenv;
        }

        public IActionResult Index()
        {
            if(TempData.Count > 0)
            {

                ViewBag.value = TempData["value"];
                return View();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(CustomerModel cm)
        {
            string f = "front";
            string b = "Back";
            var result11 = _context.CustomerMaster.Where(s => s.PhoneNumber.Equals(cm.PhoneNumber)).FirstOrDefault();
            if(result11 !=null)
            {
                TempData["value"] = true;
                return RedirectToAction("Index");
            }
            else {
                if(cm.checkbox == true)
            {
              string frontimage = UploadedFile(cm.Aadhar_front,cm,f);
              string Backimage = UploadedFile(cm.Aadhar_Back, cm,b);
              CustomerModel model = new CustomerModel();
                    model.Name = cm.Name;
                    model.PhoneNumber = cm.PhoneNumber;
                    model.City= cm.City;
                    model.State = cm.State;
                    model.Aadhar_Number = cm.Aadhar_Number;
                    model.Aadhar_frontpath = frontimage;
                    model.Aadhar_backpath = Backimage;

                    var result = _context.CustomerMaster.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Order", new { id = model.id });
            }
            else
            {
                    //string uniqueFileName = UploadedFile(model);
                    string frontimage = UploadedFile(cm.Aadhar_front, cm,f);
                    string Backimage = UploadedFile(cm.Aadhar_Back, cm,b);
                    CustomerModel model = new CustomerModel();
                    model.Name = cm.Name;
                    model.PhoneNumber = cm.PhoneNumber;
                    model.City = cm.City;
                    model.State = cm.State;
                    model.Aadhar_Number = cm.Aadhar_Number;
                    model.Aadhar_frontpath = frontimage;
                    model.Aadhar_backpath = Backimage;

                    var result = _context.CustomerMaster.Add(model);
                    _context.SaveChanges();
                return RedirectToAction("Intruder", new { tem = model.id });
            }
            }
        }
        

        public IActionResult Intruder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Intruder( int tem ,CustomerModel cm)
        {
            string f = "front";
            string b = "Back";
            TempData["id"]=tem;
            string frontimage = UploadedFile(cm.Aadhar_front, cm,f);
            string Backimage = UploadedFile(cm.Aadhar_Back, cm,b);
            CustomerModel model = new CustomerModel();
            model.Name = cm.Name;
            model.PhoneNumber = cm.PhoneNumber;
            model.City = cm.City;
            model.State = cm.State;
            model.Aadhar_Number = cm.Aadhar_Number;
            model.Aadhar_frontpath = frontimage;
            model.Aadhar_backpath = Backimage;

            var result = _context.CustomerMaster.Add(model);
            _context.SaveChanges(); 
            return RedirectToAction("Index", "Order", new { id = model.id });
        }

        private string UploadedFile(IFormFile file,CustomerModel cu,string name)
        {
            string uniqueFileName = null;

           
                string uploadsFolder = Path.Combine(_webenv.WebRootPath, "Image");
                uniqueFileName = cu.PhoneNumber + "_"+name+".jpg";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
          
            return uniqueFileName;
        }


    }
}
