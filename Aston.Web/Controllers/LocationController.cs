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
    [Route("api/Location")]
    public class LocationController : Controller
    {
        private readonly LocationProcess _locationProcess;

        public LocationController(LocationProcess locationProcess)
        {
            _locationProcess = locationProcess;
        }

        [Route("GetLocationByCode/{barcode}")]
        [HttpGet]
        public HttpResponseMessage GetLocationByCode(HttpRequestMessage request, string barcode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _locationProcess.GetLocationByCode(barcode);
            return response;
        }

        [Route("GetLocationByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetLocationByID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _locationProcess.GetLocationByID(id);
            return response;
        }
        [Route("GetLocation")]
        [HttpGet]
        public HttpResponseMessage GetLocation(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _locationProcess.GetLocation();
            return response;
        }

        [HttpPost]
        [Route("CreateLocation")]
        public HttpResponseMessage CreateLocation(HttpRequestMessage request, [FromBody] Location obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.CreatedBy = "1";
            response = _locationProcess.CreateLocation(obj);
            return response;
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public HttpResponseMessage UpdateLocation(HttpRequestMessage request, [FromBody] Location obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = "1";
            response = _locationProcess.UpdateLocation(obj);
            return response;
        }

        [HttpPost]
        [Route("DeleteLocation")]
        public HttpResponseMessage DeleteLocation(HttpRequestMessage request, [FromBody] Location obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.DeletedBy = "1";
            response = _locationProcess.DeleteLocation(obj);
            return response;
        }

    }
}