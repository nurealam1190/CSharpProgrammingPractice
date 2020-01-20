using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIStudentSample.Models;

namespace WebAPIStudentSample.Controllers
{
    public class ProductsV2Controller : ApiController
    {
        List<ProductV2> products = new List<ProductV2>()
        {
            new ProductV2(){Id=1, FirstName="Titan", LastName="Watch" },
            new ProductV2(){Id=2, FirstName="Dell", LastName="Laptop" },
            new ProductV2(){Id=3, FirstName="Ferari", LastName="Car" }
        };

        //versioning using attribute based routing
       // [Route("api/v2/products")]
        public IEnumerable<ProductV2> Get()
        {
            return products;

        }
        //versioning using attribute based routing
       // [Route("api/v2/products/{id}")]
        public ProductV2 Get(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }
    }
}
