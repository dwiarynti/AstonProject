using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aston.Web.Process;
using System.Net.Http;

namespace Aston.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Pref")]
    public class PrefController : Controller
    {
        private readonly PrefProcess _prefProcess;

        public PrefController(PrefProcess prefProcess)
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

    }
}