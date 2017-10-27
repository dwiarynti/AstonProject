using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using Aston.Entities;
using Aston.Entities.DataContext;
using System.Net;
using System.Net.Http.Headers;
using Aston.WebApi.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        public UserComponent service = new UserComponent();
        private DateExtension _dateExtension;

        public UserController(DateExtension dateExtension)
        {
            _dateExtension = dateExtension;
        }


        [HttpPost]
        [Route("GetUserPagination")]
        public HttpResponseMessage GetUserPagination(HttpRequestMessage request, [FromBody] int Skip)
        {
            var result = service.GetUserPagintion(Skip);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = true, obj = result});
            return response;
        }
        
    }
}
