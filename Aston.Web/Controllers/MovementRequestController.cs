using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aston.Web.Process;
using System.Net.Http;
using Aston.Entities;

namespace Aston.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/MovementRequest")]
    public class MovementRequestController : Controller
    {
        private readonly MovementRequestProcces _movementProcess;

        public MovementRequestController(MovementRequestProcces movementProcess)
        {
            _movementProcess = movementProcess;
        }

        [Route("GetMovementRequestByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetMovementRequestByID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _movementProcess.GetMovementRequestByID(id);
            return response;
        }

        [Route("GetMovementRequest")]
        [HttpGet]
        public HttpResponseMessage GetMovementRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _movementProcess.GetMovementRequest();
            return response;
        }
        [HttpPost]
        [Route("CreateMovementRequest")]
        public HttpResponseMessage CreateMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.CreatedBy = "1";
            response = _movementProcess.CreateMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("UpdateMovementRequest")]
        public HttpResponseMessage UpdateMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = "1";
            response = _movementProcess.UpdateMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("ApproveMovementRequest")]
        public HttpResponseMessage ApproveMovementRequest(HttpRequestMessage request, [FromBody] MovementRequest obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = "1";
            response = _movementProcess.ApproveMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("DeleteMovementRequest")]
        public HttpResponseMessage DeleteMovementRequest(HttpRequestMessage request, [FromBody] MovementRequest obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.DeletedBy = "1";
            response = _movementProcess.DeleteMovementRequest(obj);
            return response;
        }
    }
}