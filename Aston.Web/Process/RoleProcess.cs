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
    public class RoleProcess : ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
        public RoleProcess(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }
 
        public HttpResponseMessage GetRolePagination(int Skip)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/Role/GetRolePagination";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(Skip));
            return result;
        }
    }
}
