using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Phonebook.Models;

namespace Phonebook.Controllers.Tests
{
    [TestClass()]
    public class StudentControllerTests
    {
        private StudentController studentController;

        [TestInitialize]
        public void SetupContext()
        {
            studentController = new StudentController();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Task.Run(async () =>
            {
                await DocumentDBManager<Student>.DeleteStudentAsync("0123");
                await DocumentDBManager<Student>.DeleteStudentAsync("01234");
                await DocumentDBManager<Student>.DeleteStudentAsync("01235");
                await DocumentDBManager<Student>.DeleteStudentAsync("012");
                await DocumentDBManager<Student>.DeleteStudentAsync("01");
                await DocumentDBManager<Student>.DeleteStudentAsync("011");
                await DocumentDBManager<Student>.DeleteStudentAsync("0111");
                await DocumentDBManager<Student>.DeleteStudentAsync("0");
                for (int i = 0; i < 10; ++i)
                {
                    await DocumentDBManager<Student>.DeleteStudentAsync(("555" + i.ToString()));
                }
            }).GetAwaiter().GetResult();
        }

        [TestMethod()]
        public void IndexTestViewResultNotNull()
        {
            ViewResult resultIndex = studentController.Index() as ViewResult;
            Assert.IsNotNull(resultIndex);
        }

        [TestMethod()]
        public void IndexViewEqualIndexCshtml()
        {
            ViewResult resultIndex = studentController.Index() as ViewResult;
            Assert.AreEqual("Home", resultIndex.ViewBag.Title);
        }

        [TestMethod()]
        public void CreateTestViewResultNotNull()
        {
            ViewResult resultCreate = studentController.Create() as ViewResult;
            Assert.IsNotNull(resultCreate);
        }

        public void CreateViewEqualCreateCshtml()
        {
            ViewResult resultCreate = studentController.Create() as ViewResult;
            Assert.AreEqual("Create", resultCreate.ViewBag.Title);
        }

        [TestMethod()]
        public void CreateTestNewStudentInDatabase()
        {
            var newStudent = new Student()
            {
                id = "01235",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();

            Student studentInDB = DocumentDBManager<Student>.GetStudent(s => s.id == "01235");
            Assert.AreEqual<Student>(studentInDB, newStudent);
        }

        [TestMethod()]
        public void EditTestViewResultNotNull()
        {
            var newStudent = new Student()
            {
                id = "012",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();
            ViewResult resultEdit = studentController.Edit("012") as ViewResult;
            Assert.IsNotNull(resultEdit);
        }

        [TestMethod()]
        public void EditViewEqualEditCshtml()
        {
            var newStudent = new Student()
            {
                id = "0123",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();
            ViewResult resultEdit = studentController.Edit("0123") as ViewResult;
            Assert.AreEqual("Edit", resultEdit.ViewBag.Title);
        }

        [TestMethod()]
        public void EditingStudentInDatabaseTest()
        {
            var newStudent = new Student()
            {
                id = "01234",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();
            ViewResult resultEdit = studentController.Edit("01234") as ViewResult;
            var studentEdit = resultEdit.ViewData.Model;
            Student editedStudent = (Student)studentEdit;
            editedStudent.Age = 25;
            Task.Run(async () =>
            {
                await studentController.Edit(editedStudent);
            }).GetAwaiter().GetResult();
            Student studentInDB = DocumentDBManager<Student>.GetStudent(s => s.id == "01234");

            Assert.AreEqual<Student>(editedStudent, studentInDB);
        }

        [TestMethod()]
        public void DeleteTestViewResultNotNull()
        {
            var newStudent = new Student()
            {
                id = "01",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();

            ViewResult resultDelete = studentController.Delete("01") as ViewResult;
            Assert.IsNotNull(resultDelete);
        }

        [TestMethod()]
        public void DeleteViewEqualDeleteCshtml()
        {
            var newStudent = new Student()
            {
                id = "011",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();

            ViewResult resultDelete = studentController.Delete("011") as ViewResult;
            Assert.AreEqual("Delete", resultDelete.ViewBag.Title);
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            var newStudent = new Student()
            {
                id = "0111",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();
            Task.Run(async () =>
            {
                await studentController.DeleteConfirmed("0111");
            }).GetAwaiter().GetResult();
            Student studentInDB = DocumentDBManager<Student>.GetStudent(s => s.id == "0111");
            Assert.IsNull(studentInDB);
        }

        [TestMethod()]
        public void DetailsTestViewResultNotNull()
        {
            var newStudent = new Student()
            {
                id = "0",
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                await studentController.Create(newStudent);
            }).GetAwaiter().GetResult();
            ViewResult resultDetails = studentController.Details("0") as ViewResult;
            Assert.IsNotNull(resultDetails);
        }

        [TestMethod()]
        public void SearchByLocationTestViewResult()
        {
            var newStudent = new Student()
            {
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                for (int i = 0; i < 10; ++i)
                {
                    newStudent.id = ("555" + i.ToString());
                    newStudent.Location = "Dublin" + i.ToString();
                    await studentController.Create(newStudent);
                }
            }).GetAwaiter().GetResult();

            for (int i = 0; i < 10; ++i)
            {
                ViewResult resultSearchByLocationSingle = studentController.SearchByLocation("Dublin" + i.ToString()) as ViewResult;
                Assert.IsNotNull(resultSearchByLocationSingle);
                var studentInDBSingle = resultSearchByLocationSingle.ViewData.Model;
                List<Student> studentsSingle = ((IEnumerable<Student>)studentInDBSingle).Cast<Student>().ToList();
                Assert.IsNotNull(studentsSingle);
                Assert.IsNotNull(studentsSingle[0]);
                Assert.AreEqual(studentsSingle[0].Location, ("Dublin" + i.ToString()));
            }
            ViewResult resultSearchByLocation = studentController.SearchByLocation("Dublin") as ViewResult;
            Assert.IsNotNull(resultSearchByLocation);
            var studentInDB = resultSearchByLocation.ViewData.Model;
            List<Student> students = ((IEnumerable<Student>)studentInDB).Cast<Student>().ToList();
            Assert.AreEqual(students.Count, 10);
        }

        [TestMethod()]
        public void SearchByAgeTestViewResult()
        {
            var newStudent = new Student()
            {
                Surname = "Smith",
                Name = "Jack",
                Age = 20,
                Location = "Dublin",
                PhoneNumber = 12345678
            };
            Task.Run(async () =>
            {
                for (int i = 0; i < 10; ++i)
                {
                    newStudent.id = ("555" + i.ToString());
                    newStudent.Age = i + 20;
                    await studentController.Create(newStudent);
                }
            }).GetAwaiter().GetResult();

            ViewResult resultSearchByAge = studentController.SearchByAge("20", "") as ViewResult;
            Assert.IsNotNull(resultSearchByAge);
            var studentInDB = resultSearchByAge.ViewData.Model;
            List<Student> students = ((IEnumerable<Student>)studentInDB).Cast<Student>().ToList();
            Assert.AreEqual(students.Count, 10);
        }
    }
}