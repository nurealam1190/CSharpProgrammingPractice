using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIStudentSample.Models;

namespace WebAPIStudentSample.Controllers
{
    public class CandidatesController : ApiController
    {
        static List<Candidate> candidates = new List<Candidate>()
        {
            new Candidate{Id=1, Name="Nure" },
                new Candidate{Id=2, Name="Alam"},
            new Candidate{Id=3, Name="Pame"}
        };


        // In below two method we are using HttpResponseMessage to create response. we can also IHttpActionResult to 
        //create response which is very easy and flexibale and scalable way of creating response

        // Using HttpResponseMessage

        //public HttpResponseMessage Get()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, candidates);
        //}
        //public HttpResponseMessage Get(int id)
        //{
        //    var candidate = Request.CreateResponse(HttpStatusCode.OK, candidates.FirstOrDefault(u=>u.Id==id));
        //    if(candidate == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound,"Candidate not found");
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, candidates.FirstOrDefault(u => u.Id == id));
        //    }
        //}

        // Using IHttpActionResult

        public IHttpActionResult Get()
        {
           
            return Ok(candidates);
        }
        public IHttpActionResult Get(int id)
        {
            var candidate = candidates.FirstOrDefault(u => u.Id == id);
            if (candidate == null)
            {
                
                //return NotFound();

                //to return message along with not found status.
                return Content(HttpStatusCode.NotFound, "candiate not found");
            }
            else
            {                

                return Ok(candidate);
            }
        }
    }
}

