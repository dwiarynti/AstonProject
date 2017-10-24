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
        [Route("GetMovementRequestToMoveByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetMovementRequestToMoveByID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _movementProcess.GetMovementRequestToMoveByID(id);
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
        [Route("GetMovementRequestNeedApproval")]
        [HttpGet]
        public HttpResponseMessage GetMovementRequestNeedApproval(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _movementProcess.GetMovementRequestNeedApproval();
            return response;
        }
        [HttpPost]
        [Route("CreateMovementRequest")]
        public HttpResponseMessage CreateMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.CreatedBy = User.Identity.Name;
            response = _movementProcess.CreateMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("UpdateMovementRequest")]
        public HttpResponseMessage UpdateMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = User.Identity.Name;
            response = _movementProcess.UpdateMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("ApproveMovementRequest")]
        public HttpResponseMessage ApproveMovementRequest(HttpRequestMessage request, [FromBody] MovementRequest obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = User.Identity.Name;
            response = _movementProcess.ApproveMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("DeleteMovementRequest")]
        public HttpResponseMessage DeleteMovementRequest(HttpRequestMessage request, [FromBody] MovementRequest obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.DeletedBy = User.Identity.Name;
            response = _movementProcess.DeleteMovementRequest(obj);
            return response;
        }
        [HttpPost]
        [Route("DeleteMovementRequestDetail")]
        public HttpResponseMessage DeleteMovementRequestDetail(HttpRequestMessage request, [FromBody] MovementRequestDetail obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.DeletedBy = User.Identity.Name;
            response = _movementProcess.DeleteMovementRequestDetail(obj);
            return response;
        }
        [HttpPost]
        [Route("SearchMovementRequest")]
        public HttpResponseMessage SearchMovementRequest(HttpRequestMessage request, [FromBody] MovementRequestViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _movementProcess.SearchMovementRequest(obj);
            return response;
        }
    }
}