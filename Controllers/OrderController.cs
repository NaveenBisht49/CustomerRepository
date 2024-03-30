using Microsoft.AspNetCore.Mvc;
using Project_1.Data;
using Project_1.Models;
using Project_1.ViewModel;

namespace Project_1.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProjectDbContext _context;

        public OrderController(ProjectDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
          public IActionResult Index(int id , OrderModel or)
            {
                OrderModel orm = new OrderModel();
                    if (TempData.Count > 0) 
                    { 
                        orm.amount = or.amount;
                        orm.CustomerId = Convert.ToInt32(TempData["id"]);
                        orm.IntruderId = id;
                        var result = _context.Ordertable.Add(orm);
                        _context.SaveChanges();
                TempData["Id"] = TempData["id"];
                       return RedirectToAction("printout");
                    }
                    else { 
                                orm.amount = or.amount;
                                orm.CustomerId = id;
                                orm.IntruderId = id;
                                var result1 = _context.Ordertable.Add(orm);
                                _context.SaveChanges();
                TempData["Id"] = id;
                                return RedirectToAction("printout");
                    }
                    
        }

        public IActionResult printout()
        {
            var result = _context.CustomerMaster
                     .Join(
                         _context.Ordertable,
                         customer => customer.id,
                         order => order.CustomerId,
                         (customer, order) => new CustomerViewModel { Customer = customer, Order = order }
                     )
                     .Where(x =>x.Customer.id == Convert.ToInt32(TempData["Id"])).FirstOrDefault();
            return View(result);
        }

    }
}
