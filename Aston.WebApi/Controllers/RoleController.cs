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
    [Route("api/Role")]
    public class RoleController : Controller
    {
        public RoleComponent service = new RoleComponent();
        private DateExtension _dateExtension;

        public RoleController(DateExtension dateExtension)
        {
            _dateExtension = dateExtension;
        }


        [HttpPost]
        [Route("GetRolePagination")]
        public HttpResponseMessage GetRolePagination(HttpRequestMessage request, [FromBody] int Skip)
        {
            var result = service.GetRolePagination(Skip);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = true, obj = result});
            return response;
        }
        
    }
}
