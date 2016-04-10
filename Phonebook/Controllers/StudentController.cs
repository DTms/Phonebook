using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class StudentController : Controller
    {
        // GET: All Students from DB
        public ActionResult Index()
        {
            var students = DocumentDBManager<Student>.GetAllStudents();
            return View(students);
        }
        // Create and save the database
        public ActionResult Create()
        {
            return View();
        }


        //This cat is DocumentDB Repository and uses CreateStudentAsync method to save a new student in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Surname,Name,Age,PhoneNumber,Location")] Student student)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBManager<Student>.CreateStudentAsync(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // Editing student data and save the database
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = (Student)DocumentDBManager<Student>.GetStudent(s => s.id == id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


        //This cat is DocumentDB Repository and uses UpdateStudentAsync method to update a new student in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Surname,Name,Age,PhoneNumber,Location")] Student student)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBManager<Student>.UpdateStudentAsync(student.id, student);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student item = DocumentDBManager<Student>.GetStudent(s => s.id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id")] string id)
        {
            await DocumentDBManager<Student>.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = DocumentDBManager<Student>.GetStudent(s => s.id == id);
            return View(student);
        }

        // Search by location
        public ActionResult SearchByLocation(string SearchByLocation)
        {
            var students = DocumentDBManager<Student>.Search(s => s.Location.Contains(SearchByLocation));
            return View(students);
        }
    }
}