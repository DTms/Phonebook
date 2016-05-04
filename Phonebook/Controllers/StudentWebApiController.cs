using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class StudentWebApiController : ApiController
    {
        // GET: api/StudentWebApi
        public IEnumerable<Student> Get()
        {
            return DocumentDBManager<Student>.GetAllStudents();
        }

        // GET: api/StudentWebApi/5
        public Student Get(string id)
        {
            return DocumentDBManager<Student>.GetStudent(s => s.id == id);
        }

        // POST: api/StudentWebApi
        public void Post([FromBody]Student student)
        {
            if (!student.Equals(null))
            {
                DocumentDBManager<Student>.CreateStudentAsync(student);
            }
        }

        // PUT: api/StudentWebApi/5
        public void Put(string id, [FromBody]Student student)
        {
            if (!student.Equals(null) && !id.Equals(null))
            {
                DocumentDBManager<Student>.UpdateStudentAsync(id, student);
            }
        }

        // DELETE: api/StudentWebApi/5
        public void Delete(string id)
        {
            if (!id.Equals(""))
            {
                DocumentDBManager<Student>.DeleteStudentAsync(id);
            }
        }
    }
}
