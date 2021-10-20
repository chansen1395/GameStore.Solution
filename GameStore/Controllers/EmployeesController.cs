using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GameStore.Controllers
{
  [Authorize]
  public class EmployeesController : Controller
  {
    private readonly GameStoreContext _db;

    public EmployeesController(GameStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Employee> model = _db.Employees.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
      _db.Employees.Add(employee);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisEmployee = _db.Employees
          .Include(employee => employee.JoinEntities)
          .ThenInclude(join => join.Customer)
          .FirstOrDefault(employee => employee.EmployeeId == id);
      return View(thisEmployee);
    }
    public ActionResult Edit(int id)
    {
      var thisEmployee = _db.Employees.FirstOrDefault(employee => employee.EmployeeId == id);
      return View(thisEmployee);
    }

    [HttpPost]
    public ActionResult Edit(Employee employee)
    {
      _db.Entry(employee).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisEmployee = _db.Employees.FirstOrDefault(employee => employee.EmployeeId == id);
      return View(thisEmployee);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEmployee = _db.Employees.FirstOrDefault(employee => employee.EmployeeId == id);
      _db.Employees.Remove(thisEmployee);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}