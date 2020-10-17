using MVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmpDataController : Controller
    {
        public ViewResult AllEmployees()
        {
            var context = new database111Entities();
            var model = context.EmpTable111.ToList();
            return View(model);
        }

        public ViewResult Find(string id)
        {
            int empId = int.Parse(id);
            var context = new database111Entities();
            var model = context.EmpTable111.FirstOrDefault((e) => e.EmpId == empId);
            return View(model);

        }
        //ActionResult is the abstract class for all kinds of action returns....
        [HttpPost]
        public ActionResult Find(EmpTable111 emp)
        {
            var context = new database111Entities();
            var model = context.EmpTable111.FirstOrDefault((e) => e.EmpId == emp.EmpId);
            model.EmpName = emp.EmpName;
            model.EmpAddress = emp.EmpAddress;
            model.EmpSalary = emp.EmpSalary;
            context.SaveChanges();//Commits the changes made to the records...
            return RedirectToAction("AllEmployees");
        }

        public ViewResult NewEmployee()
        {
            var model = new EmpTable111();//No Values in it...
            return View(model);
        }

        [HttpPost]
        public ActionResult NewEmployee(EmpTable111 emp)
        {
            try
            {
                var context = new database111Entities();
                context.EmpTable111.Add(emp);
                context.SaveChanges();
                return RedirectToAction("AllEmployees");
            }
            catch
            {
                return RedirectToAction("AllEmployees");
            }

            //var context = new database111Entities();
            //context.EmpTable111.Add(emp);
            //context.SaveChanges();
            //return RedirectToAction("AllEmployees");
        }

        public ActionResult Delete(string id)
        {
            //convert string to int
            int empId = int.Parse(id);
            var context = new database111Entities();
            var model = context.EmpTable111.FirstOrDefault((e) => e.EmpId == empId);
            context.EmpTable111.Remove(model);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }
    }
}