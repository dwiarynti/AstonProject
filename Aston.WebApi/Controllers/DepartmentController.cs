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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{


    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private DepartmentComponent service = new DepartmentComponent();
        private DateExtension _dateExtension;

        public DepartmentController(DateExtension dateExtension)
        {
            _dateExtension = dateExtension;
        }

        [HttpGet]
        [Route("GetDepartmentByID/{id}")]
        public HttpResponseMessage GetDepartmentByID(HttpRequestMessage request, int id)
        {
            var result = service.GetDepartmentByID(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = true, obj = result});
            return response;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public HttpResponseMessage GetDepartments(HttpRequestMessage request)
        {
            var result = service.GetDepartments();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = true, obj = result});
            return response;
        }

        [HttpGet]
        [Route("GetActiveDepartments")]
        public HttpResponseMessage GetActiveDepartments(HttpRequestMessage request)
        {
            var result = service.GetActiveDepartments();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("GetDepartmentPagination")]
        public HttpResponseMessage GetDepartmentPagination(HttpRequestMessage request, [FromBody] int Skip)
        {
            var result = service.GetDepartmentPagination(Skip);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public HttpResponseMessage CreateDepartment(HttpRequestMessage request, [FromBody] DepartmentViewModel obj)
        {
            //obj.CreatedDate = _dateExtension.GetDateTime();

            var result = service.CreateDepartment(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = result});
            return response;
        }

        [HttpPost]
        [Route("UpdateDepartment")]
        public HttpResponseMessage UpdateDepartment(HttpRequestMessage request, [FromBody] DepartmentViewModel obj)
        {
            //obj.UpdatedDate = _dateExtension.GetDateTime();

            var result = service.UpdateDepartment(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = result});
            return response;
        }

        [HttpPost]
        [Route("DeleteDepartment")]
        public HttpResponseMessage DeleteDepartment(HttpRequestMessage request, [FromBody] DepartmentViewModel obj)
        {
            //obj.DeletedDate = _dateExtension.GetDateTime();

            var result = service.DeleteDepartment(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new {success = result});
            return response;
        }

    }
}
