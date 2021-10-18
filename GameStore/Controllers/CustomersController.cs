using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GameStore.Controllers
{
  [Authorize] //new line
  public class CustomersController : Controller
  {
    private readonly GameStoreContext _db;
    private readonly UserManager<ApplicationUser> _userManager; //new line

    //updated constructor
    public CustomersController(UserManager<ApplicationUser> userManager, GameStoreContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    // [AllowAnonymous]
    public async Task<ActionResult> Index()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        var userCustomers = _db.Customers.Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userCustomers);
    }

    public ActionResult Create()
    {
      ViewBag.EmployeeId = new SelectList(_db.Employees, "EmployeeId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Customer customer, int EmployeeId)
    {
    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var currentUser = await _userManager.FindByIdAsync(userId);
    customer.User = currentUser;
    _db.Customers.Add(customer);
    _db.SaveChanges();
    if (EmployeeId != 0)
    {
        _db.CustomerEmployee.Add(new CustomerEmployee() { EmployeeId = EmployeeId, CustomerId = customer.CustomerId });
    }
    _db.SaveChanges();
    return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisCustomer = _db.Customers
          .Include(customer => customer.JoinEntities)
          .ThenInclude(join => join.Employee)
          .FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    public ActionResult Edit(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      ViewBag.EmployeeId = new SelectList(_db.Employees, "EmployeeId", "Name");
      return View(thisCustomer);
    }

    [HttpPost]
    public ActionResult Edit(Customer customer, int EmployeeId)
    {
      if (EmployeeId != 0)
      {
        _db.CustomerEmployee.Add(new CustomerEmployee() { EmployeeId = EmployeeId, CustomerId = customer.CustomerId });
      }
      _db.Entry(customer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddEmployee(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      ViewBag.EmployeeId = new SelectList(_db.Employees, "EmployeeId", "Name");
      return View(thisCustomer);
    }

    [HttpPost]
    public ActionResult AddEmployee(Customer customer, int EmployeeId)
    {
      if (EmployeeId != 0)
      {
      _db.CustomerEmployee.Add(new CustomerEmployee() { EmployeeId = EmployeeId, CustomerId = customer.CustomerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      _db.Customers.Remove(thisCustomer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteEmployee(int joinId)
    {
      var joinEntry = _db.CustomerEmployee.FirstOrDefault(entry => entry.CustomerEmployeeId == joinId);
      _db.CustomerEmployee.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}