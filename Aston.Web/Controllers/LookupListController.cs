using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aston.Web.Process;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace Aston.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/LookupList")]
    public class LookupListController : Controller
    {
        private readonly LookupListProcess _prefProcess;

        public LookupListController(LookupListProcess prefProcess)
        {
            _prefProcess = prefProcess;
        }

        [Route("GetCategory")]
        [HttpGet]
        public HttpResponseMessage GetCategory(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _prefProcess.GetCategory();
            return response;
        }
        [Route("GetLocationType")]
        [HttpGet]
        public HttpResponseMessage GetLocationType(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _prefProcess.GetLocationType();
            return response;
        }
        [Route("GetStatus")]
        [HttpGet]
        public HttpResponseMessage GetStatus(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _prefProcess.GetStatus();
            return response;
        }
        [Route("GetApprovalStatus")]
        [HttpGet]
        public HttpResponseMessage GetApprovalStatus(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _prefProcess.GetApprovalStatus();
            return response;
        }
        [Route("GetDepartment")]
        [HttpGet]
        public HttpResponseMessage GetDepartment(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _prefProcess.GetDepartment();
            return response;
        }
        [Route("GetDepartmentByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentByID(HttpRequestMessage request , int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _prefProcess.GetDepartmentByID(id);
            return response;
        }

    }
}