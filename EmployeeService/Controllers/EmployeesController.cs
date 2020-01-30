using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        [Authorize]
        public IEnumerable<Employee> Get()
        {
            EmployeeDBEntities entities = new EmployeeDBEntities();
            return entities.Employees.ToList();
        }

        [HttpPost]
        public HttpResponseMessage AddEmployee([FromBody] Employee emp)
        {
            try
            {
                EmployeeDBEntities ent = new EmployeeDBEntities();
                ent.Employees.Add(emp);
                ent.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                message.Headers.Location = new Uri(Request.RequestUri + emp.ID.ToString());

                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Get(int id)        
        {

            EmployeeDBEntities entities = new EmployeeDBEntities();
            var emp = entities.Employees.FirstOrDefault(e => e.ID == id);
            if (emp == null)
            {
                //var response = Request.CreateResponse(HttpStatusCode.OK, message);
                //response.Headers.Location = new Uri("https://localhost:44372/UpdateEmployee.html");
                return NotFound();
            }
            else
            {
                // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id + " doesn't exist in database");
                return Ok(emp);
            }

        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {

            EmployeeDBEntities ent = new EmployeeDBEntities();

            try
            {
                var entity = ent.Employees.FirstOrDefault(e => e.ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + id + " doesn't exist.");
                }
                else
                {

                    ent.Employees.Remove(entity);
                    ent.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        [HttpPut]
        public IHttpActionResult put([FromBody] Employee emp)
        {
            EmployeeDBEntities ent = new EmployeeDBEntities();
            try
            {
                var entity = ent.Employees.FirstOrDefault(e => e.ID == emp.ID);
                if (entity != null)
                {
                    //ent.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    entity.FirstName = emp.FirstName;
                    entity.LastName = emp.LastName;
                    entity.Gender = emp.Gender;
                    entity.Salary = emp.Salary;

                    ent.SaveChanges();

                    // return Request.CreateResponse(HttpStatusCode.OK);
                    return Ok();
                }
                else
                {

                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + emp.ID + " doesn't exist.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return BadRequest();
            }

        }
    }
}
