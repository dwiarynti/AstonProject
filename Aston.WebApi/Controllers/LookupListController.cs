using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using System.Net;
using Aston.WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
    [Route("api/LookupList")]
    public class LookupListController : Controller
    {
        LookupListComponent service = new LookupListComponent();
        private DateExtension _dateExtension;

        public LookupListController(DateExtension dateExtension)
        {
            _dateExtension = dateExtension;
        }

        [HttpGet]
        [Route("GetCategory")]
        public HttpResponseMessage GetCategory(HttpRequestMessage request)
        {
            var result = service.GetCategory();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpGet]
        [Route("GetLocationType")]
        public HttpResponseMessage GetLocationType(HttpRequestMessage request)
        {
            var result = service.GetLocationType();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpGet]
        [Route("GetStatus")]
        public HttpResponseMessage GetStatus(HttpRequestMessage request)
        {
            var result = service.GetStatus();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpGet]
        [Route("GetApprovalStatus")]
        public HttpResponseMessage GetApprovalStatus(HttpRequestMessage request)
        {
            var result = service.GetApprovalStatus();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        // Duplicate
        //[HttpGet]
        //[Route("GetDepartment")]
        //public HttpResponseMessage GetDepartment(HttpRequestMessage request)
        //{
        //    var result = service.GetDepartment();
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
        //    return response;
        //}
        //[HttpGet]
        //[Route("GetDepartmentByID/{id}")]
        //public HttpResponseMessage GetDepartmentByID(HttpRequestMessage request,int id)
        //{
        //    var result = service.GetDepartmentByID(id);
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
        //    return response;
        //}

    }
}
