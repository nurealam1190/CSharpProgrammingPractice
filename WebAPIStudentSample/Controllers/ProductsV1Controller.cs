using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIStudentSample.Models;

namespace WebAPIStudentSample.Controllers
{
    public class ProductsV1Controller : ApiController
    {
        List<ProductV1> products = new List<ProductV1>()
        {
            new ProductV1() {Id=1, Name= "Software"},
            new ProductV1() {Id=2, Name= "Furniture"},
            new ProductV1() {Id=3, Name= "Electronic Device"}
        };

        //versioning using attribute based routing
        //[Route("api/v1/products")]
        public IEnumerable<ProductV1> Get()
        {
            return products;
        }
        //versioning using attribute based routing
       // [Route("api/v1/products/{id}")]
        public ProductV1 Get(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}
