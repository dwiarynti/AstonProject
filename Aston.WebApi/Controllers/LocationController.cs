using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using System.Net;
using Aston.Entities;
using Aston.WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
    [Route("api/location")]
    public class LocationController : Controller
    {
        public LocationComponent service = new LocationComponent();
        private DateExtension _dateExtension;

        public LocationController(DateExtension dateExtension)
        {
            _dateExtension = dateExtension;
        }

        [HttpGet]
        [Route("GetLocationByCode/{barcode}")]
        public HttpResponseMessage GetLocationByCode(HttpRequestMessage request, string barcode)
        {
            var result = service.GetLocationByCode(barcode);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpGet]
        [Route("GetLocationByID/{id}")]
        public HttpResponseMessage GetLocationByID(HttpRequestMessage request, int id)
        {
            var result = service.GetLocationByID(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpGet]
        [Route("GetLocation")]
        public HttpResponseMessage GetLocation(HttpRequestMessage request)
        {
            var result = service.GetLocation();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("CreateLocation")]
        public HttpResponseMessage CreateLocation(HttpRequestMessage request, [FromBody] LocationViewModel obj)
        {
            obj.CreatedDate = _dateExtension.GetDateTime();

            var result = service.CreateLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public HttpResponseMessage UpdateLocation(HttpRequestMessage request, [FromBody] LocationViewModel obj)
        {
            obj.UpdatedDate = _dateExtension.GetDateTime();

            var result = service.UpdateLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }
        [HttpPost]
        [Route("DeleteLocation")]
        public HttpResponseMessage DeleteLocation(HttpRequestMessage request, [FromBody] Location obj)
        {
            obj.DeletedDate = _dateExtension.GetDateTime();

            var result = service.DeleteLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }

        [HttpPost]
        [Route("SearchLocation")]
        public HttpResponseMessage SearchLocation(HttpRequestMessage request, [FromBody] LocationViewModel obj)
        {
            var result = service.SearchLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

    }
}
