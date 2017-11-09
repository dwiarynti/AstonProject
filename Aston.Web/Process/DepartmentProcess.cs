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
    public class DepartmentProcess : ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
        public DepartmentProcess(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        public HttpResponseMessage GetDepartmentByID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/GetDepartmentByID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }

        public HttpResponseMessage GetDepartments()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/GetDepartments/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }

        public HttpResponseMessage GetActiveDepartments()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/GetActiveDepartments/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }


        public HttpResponseMessage GetDepartmentPagination(int Skip)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/GetDepartmentPagination";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(Skip));
            return result;
        }


        public HttpResponseMessage CreateDepartment(DepartmentViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/CreateDepartment";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }

        public HttpResponseMessage UpdateDepartment(DepartmentViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/UpdateDepartment";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }

        public HttpResponseMessage DeleteDepartment(DepartmentViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Department/DeleteDepartment";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }

    }
}
