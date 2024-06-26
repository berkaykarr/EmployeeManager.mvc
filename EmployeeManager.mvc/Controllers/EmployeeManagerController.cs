﻿using EmployeeManager.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.Mvc.Controllers
{
    public class EmployeeManagerController : Controller
    {
        private AppDbContext db = null;
        public EmployeeManagerController(AppDbContext _db)
        {
            db = _db;
        }

        private void FillCountries()
        {
            List<SelectListItem> countries =
            (from c in db.Countries
             orderby c.Name ascending
             select new SelectListItem()
             {
                 Text = c.Name,
                 Value = c.Name
             }).ToList();
            ViewBag.Countries = countries;
        }

        [Authorize(Roles = "Manager")]
        public IActionResult List()
        {
            List<Employee> model = (from e in db.Employees
                                    orderby e.EmployeeID
                                    select e ).ToList();
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Insert()
        {
            FillCountries();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Insert(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                db.Employees.Add(model);
                /*.Employees.Add(model);*/
                db.SaveChanges();
                ViewBag.Message = "Employee inserted successfully";
            }
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Update(int id)
        {
            FillCountries();
            Employee model = db.Employees.Find(id);  //db.Employees.Find(id);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Update(Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                db.Employees.Update(model);
                db.SaveChanges();
                ViewBag.Message = "Employee updated successfully";
            }
            return View(model);
        }

        [ActionName("Delete")]
        [Authorize(Roles = "Manager")]
        public IActionResult ConfirmDelete(int id)
        {
            Employee model = db.Employees.Find(id);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int employeeID)
        {
            Employee model = db.Employees.Find(employeeID);
            db.Employees.Remove(model);
            db.SaveChanges();
            TempData["Message"] = "Employee deleted successfully";
            return RedirectToAction("List");
        }

    }
}
