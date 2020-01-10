using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Web.Http.Cors;

namespace WebAPIDemo.Controllers
{
    //[EnableCorsAttribute("*", "*", "*")]  // This is to enable cors for all the method within this controller globally.
    
    public class EmployeesController : ApiController
    {
        //This method is modified below with query string

        //public IEnumerable<Employee> Get()
        //{

        //    EmployeeDBEntities entities = new EmployeeDBEntities();
        //    return entities.Employees.ToList();
        //}

        //Below method is for creating a response for input query string for gender either "All", "Male", "Female"
        //Anything other than this will throw an error 404 bad request.

        //[DisableCors] //this is disable cors for this method
        public HttpResponseMessage Get(string gender="All")
        {

            EmployeeDBEntities entities = new EmployeeDBEntities();
            switch(gender.ToLower())
            {
                case "all":
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());

                case "female":
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());

                case "male":
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());

                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Mentioned gender " +gender + " is invalid. It should be either Male,Female or All");
            }

            
        }

        //here even if we chnage the name of method and decorate with hhtpGet request will be processed.
        //[HttpGet]
        //public IEnumerable<Employee> LoadAllEmployees()
        //{

        //    EmployeeDBEntities entities = new EmployeeDBEntities();
        //    return entities.Employees.ToList();
        //}

        //Below method is working but not gave proper response if id doesn't exist.
        //public Employee Get(int id)
        //{

        //    EmployeeDBEntities entities = new EmployeeDBEntities();
        //    return entities.Employees.FirstOrDefault(e=> e.ID==id);
        //}

        public HttpResponseMessage Get(int id)
        {

            EmployeeDBEntities entities = new EmployeeDBEntities();
            var message = entities.Employees.FirstOrDefault(e => e.ID == id);
            if (message != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id + " doesn't exist in database");
            }

        }

        //here we have two method for get, one is above like Get(int id) and one is below like LoadEMployeeById(int id) and decorated with [httpGet]
        //In such case when we request it will throw error because there are multiple method Get available to serve for this request.
        
        //[HttpGet]
        //public HttpResponseMessage LoadEMployeeById(int id)
        //{

        //    EmployeeDBEntities entities = new EmployeeDBEntities();
        //    var message = entities.Employees.FirstOrDefault(e => e.ID == id);
        //    if (message != null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, message);
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id + " doesn't exist in database");
        //    }

        //}



        //By below post method is fine and it will add a new record in database but when investigate the response 
        //through fiddler you will see return type 204 No Content because return type of below method is void. It should return
        // 201 item created.
        //public void Post([FromBody] Employee emp)
        //{
        //    EmployeeDBEntities ent = new EmployeeDBEntities();
        //    ent.Employees.Add(emp);
        //    ent.SaveChanges();
        //}

        public HttpResponseMessage Post([FromBody] Employee emp)
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
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

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
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }        
                      

        }


        // This put method is using default parameter binding, meaning it is checking URI for simple type attrb like int id
        //and looking request body for completx type atrrb like EMployee emp. Here even if we will not mention[fromBody] attribute by default it will look for request body for parameter

        //public HttpResponseMessage Put(int id, [FromBody]Employee emp)
        //{
        //    EmployeeDBEntities ent = new EmployeeDBEntities();
        //    try
        //    {
        //        var entity = ent.Employees.FirstOrDefault(e => e.ID == id);
        //        if (entity != null)
        //        {
        //            entity.FirstName = emp.FirstName;
        //            entity.LastName = emp.LastName;
        //            entity.Gender = emp.Gender;
        //            entity.Salary = emp.Salary;

        //            ent.SaveChanges();

        //            return Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //        {

        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id + " doesn't exist.");
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }

        //}


        //Let's chnage the default parameter binding and force api to look into uRI for complex attb like Employee and
        //look into request body for simple attb like int id.
        //so while requesting we have to pass all param through url

        public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee emp)
        {
            EmployeeDBEntities ent = new EmployeeDBEntities();
            try
            {
                var entity = ent.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    entity.FirstName = emp.FirstName;
                    entity.LastName = emp.LastName;
                    entity.Gender = emp.Gender;
                    entity.Salary = emp.Salary;

                    ent.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id + " doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
