using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using Aston.Business;
using Aston.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
    [Route("api/MovementRequest")]
    public class MovementRequestController : Controller
    {
        MovementRequestComponent service = new MovementRequestComponent();

        [HttpGet]
        [Route("GetMovementRequest")]
        public HttpResponseMessage GetMovementRequest(HttpRequestMessage request)
        {
            var result = service.GetMovementRequest();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpGet]
        [Route("GetMovementRequestByID/{id}")]
        public HttpResponseMessage GetMovementRequestByID(HttpRequestMessage request, int id)
        {
            var result = service.GetMovementRequestByID(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpPost]
        [Route("CreateMovementRequest")]
        public HttpResponseMessage CreateMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            var result = service.CreateMovementRequest(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpPost]
        [Route("ApproveMovementRequest")]
        public HttpResponseMessage ApproveMovementRequest(HttpRequestMessage request, [FromBody] MovementRequest obj)
        {
            var result = service.ApproveMovementRequest(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpPost]
        [Route("UpdateMovementRequest")]
        public HttpResponseMessage UpdateMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            var result = service.UpdateMovementRequest(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpPost]
        [Route("DeleteMovementRequest")]
        public HttpResponseMessage DeleteMovementRequest(HttpRequestMessage request, [FromBody] MovementRequest obj)
        {
            var result = service.DeleteMovementRequest(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
    }
}
