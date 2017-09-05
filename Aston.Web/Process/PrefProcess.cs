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
    public class PrefProcess :ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
        public PrefProcess(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }
        public HttpResponseMessage GetCategory()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/pref/GetCategory/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetLocationType()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/pref/GetLocationType/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetStatus()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/pref/GetStatus/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
    }
}
