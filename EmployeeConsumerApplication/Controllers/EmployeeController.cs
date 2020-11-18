using EmployeeConsumerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeConsumerApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        ServicesClient servicesClient = new ServicesClient();
        // GET: Location
        public ViewResult Index(string sortOrder, string search, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.listEmployee = servicesClient.getAllEmployee();

            if (search != null)
            {
                page = 1; // nếu search có giá trị trả về page = 1
            }
            else
            {
                search = currentFilter; //  nếu có thì render phần dữ liệu search ra
            }
            ViewBag.CurrentFilter = search;
            var employees = from s in servicesClient.getAllEmployee() select s;
            if (!String.IsNullOrEmpty(search)) // check nếu search string có thì in ra hoặc không thì không in ra
            {
                employees = employees.Where(emp => emp.Department.DepartmentName.Contains(search)); // contains là để check xem lastname hoặc firstName có chứa search string ở trên 
            }
            switch (sortOrder)
            {
                case "name desc":
                    employees = employees.OrderByDescending(emp => emp.Department.DepartmentName); // các case tương đương với các cột muốn sort
                    break;

                default:
                    employees = employees.OrderBy((emp => emp.Department.DepartmentName));
                    break;
            }

            return View(employees.ToList());

        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var Employee = servicesClient.getAllEmployee().Where(b => b.EmployeeID == id).FirstOrDefault();
            return View(Employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee newEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    servicesClient.AddEmployee(newEmployee);
                    return RedirectToAction("Index", "Employee");
                }

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
