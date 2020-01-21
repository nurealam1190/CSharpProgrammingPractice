using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIStudentSample.Models;

namespace WebAPIStudentSample.Controllers
{
   // [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student () { Id=1,Name="Nure"},
            new Student() { Id = 2,Name = "John"},
            new Student() { Id = 3,Name = "Smith"}
        };
        //[Route("api/students")] --without route prefix
        [Route("")]  //with Route prefix
        public IEnumerable<Student> Get()
        {
            return students;
        }
        [Route("api/students/{id}", Name ="GetStudentById")]// -- without route prefix
       // [Route("{id:int}")] //with Route prefix
      //  [Route("{id:int:min(1)}")] //for minimum value
       // [Route("{id:int:min(1):max(3)}")] //for minimum value
       // [Route("{id:int:range(1,3)}")] //other than using min and max we use range() to specify the range
        public Student Get(int id)
        {
            return students.FirstOrDefault(u => u.Id == id);
        }


        [Route("{name:alpha}")] //with Route prefix
        public Student Get(string name)
        {
            return students.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
        }

        //[Route("api/students/{id}/courses")] --without route prefix
        [Route("{id}/courses")] //with Route prefix
        public IEnumerable<string> GetStudentCourses(int id)
        {
            if (id == 1)
            {
                return new List<string>() { "Math", "Physics", "Chemistry" };
            }
            else if (id == 2)
            {
                return new List<string>() { "Geography", "History", "Urdu" };
            }
            else
            {
                return new List<string>() { "Hindi", "English", "French" };
            }
        }

        [Route("~/api/teachers")]
        public IEnumerable<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher() { Id=1, Name="Smith"},
                new Teacher() { Id=2, Name="Smith"},
                new Teacher() { Id=3, Name="Smith"}

            };
            return teachers;
        }

        //post method
       
        public HttpResponseMessage Post(Student student)
        {
            students.Add(student);

            var response = Request.CreateResponse(HttpStatusCode.Created);
            // This line is to generate a link using Request property for the created record lik "https://44372/api/students/4" 
            //response.Headers.Location = new Uri(Request.RequestUri + student.Id.ToString());

            // This line is to generate a link using name property of Route of get method property for the created record lik "https://44372/api/students/4" 
            response.Headers.Location = new Uri(Url.Link("GetStudentById", new { id =student.Id}));
            return response;
        }
    }
}
