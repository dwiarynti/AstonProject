using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Aston.Web.Process;
using Aston.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Aston.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private readonly DepartmentProcess _process;

        public DepartmentController(DepartmentProcess process)
        {
            _process = process;
        }

        [Route("GetDepartmentByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentByID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _process.GetDepartmentByID(id);
            return response;
        }

        [Route("GetDepartment")]
        [HttpGet]
        public HttpResponseMessage GetDepartment(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _process.GetDepartment();
            return response;
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public HttpResponseMessage CreateDepartment(HttpRequestMessage request, [FromBody] DepartmentViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            //obj.CreatedBy = User.Identity.Name;
            response = _process.CreateDepartment(obj);
            return response;
        }

        [HttpPost]
        [Route("UpdateDepartment")]
        public HttpResponseMessage UpdateDepartment(HttpRequestMessage request, [FromBody] DepartmentViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            //obj.UpdatedBy = User.Identity.Name;
            response = _process.UpdateDepartment(obj);
            return response;
        }

        [HttpPost]
        [Route("DeleteDepartment")]
        public HttpResponseMessage DeleteDepartment(HttpRequestMessage request, [FromBody] DepartmentViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            //obj.DeletedBy = User.Identity.Name;
            response = _process.DeleteDepartment(obj);
            return response;
        }

    }
}