using Microsoft.AspNetCore.Mvc;
using gradesApp.Models;
using System;

namespace gradesApp.Controllers
{
    public class GradesController : Controller
    {
        public IActionResult Index() {
            // first visit to app - construct model and pass to view
            CourseGrades courseGrades = new CourseGrades();
            courseGrades.setupMe();
            return View(courseGrades);
        }

        // [HttpPost]
        public IActionResult SelectCategory(CourseGrades courseGrades) {
            // only do this if we got here via a redirect from AddSubmit!
            if (TempData["categoryID"] != null) {
                courseGrades.categoryID = Convert.ToInt32(TempData["categoryID"]);
            }
            courseGrades.setupMe();
            return View("Index", courseGrades);
        }

        [Route("Grades/Details/{gradeID}")]
        // public IActionResult Details(CourseGrades courseGrades, string gradeID, string categoryID, string courseName, string courseDescription, string grade, string comments) {         
        public IActionResult Details(CourseGrades courseGrades, string gradeID) {         
            courseGrades.getDetails(gradeID);


            // ---------------------------------- CHALLENGE
            // courseGrades.getDetails(gradeID, categoryID, courseName, courseDescription, grade, comments);
            return View(courseGrades);
        }   

        [HttpPost]
        public IActionResult Add(CourseGrades courseGrades) {
            return View(courseGrades);
        }

        [HttpPost]
        public IActionResult AddSubmit(CourseGrades courseGrades, string courseName, string courseDescription, string grade, string comments) {
            // add the new grade to the database
            courseGrades.addGrade(courseName, courseDescription, grade, comments);

            // // refresh the data now that a new grade has been added
            // courseGrades.setupMe();
            // return View("Index", courseGrades);

            // with PRG pattern (POST / REDIRECT / GET)
            TempData["categoryID"] = courseGrades.categoryID;
            // TempData["categoryName"] = courseGrades.categoryName;
            return RedirectToAction("SelectCategory");
        }

    }
}
