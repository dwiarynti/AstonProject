using Aston.Entities;
using Aston.Web.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aston.Web.Process
{
    public class MovementRequestProcces : ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
        public MovementRequestProcces(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }
        public HttpResponseMessage GetMovementRequest()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/GetMovementRequest/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetMovementRequestNeedApproval()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/GetMovementRequestNeedApproval/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetMovementRequestByID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/GetMovementRequestByID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetMovementRequestToMoveByID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/GetMovementRequestToMoveByID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage CreateMovementRequest(MovementRequestViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/CreateMovementRequest/";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage ApproveMovementRequest(MovementRequest obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/ApproveMovementRequest/";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage UpdateMovementRequest(MovementRequestViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/UpdateMovementRequest/";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage DeleteMovementRequest(MovementRequest obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/DeleteMovementRequest/";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage DeleteMovementRequestDetail(MovementRequestDetail obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/DeleteMovementRequestDetail/";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }

        public HttpResponseMessage SearchMovementRequest(MovementRequestViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/MovementRequest/SearchMovementRequest/";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
    }
}
