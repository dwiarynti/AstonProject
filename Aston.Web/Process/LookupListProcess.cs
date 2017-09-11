using Aston.Entities;
using Aston.Web.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aston.Web.Process
{
    public class LookupListProcess :ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
        public LookupListProcess(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }
        public HttpResponseMessage GetCategory()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/LookupList/GetCategory/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetLocationType()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/LookupList/GetLocationType/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetStatus()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/LookupList/GetStatus/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetApprovalStatus()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/LookupList/GetApprovalStatus/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetDepartmentByID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/LookupList/GetDepartmentByID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetDepartment()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/LookupList/GetDepartment/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
    }
}
