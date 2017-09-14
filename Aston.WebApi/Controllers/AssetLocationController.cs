using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using Aston.Entities;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
 
    [Route("api/AssetLocation")]
    public class AssetLocationController : Controller
    {
        public AssetLocationComponent service = new AssetLocationComponent();

        [HttpGet]
        [Route("GetAssetLocationByLocationID/{id}")]
        public HttpResponseMessage GetAssetLocationByLocationID(HttpRequestMessage request, int id)
        {
            var result = service.GetAssetLocationByLocationID(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpGet]
        [Route("GetAssetLocationByID/{id}")]
        public HttpResponseMessage GetAssetLocationByID(HttpRequestMessage request, int id)
        {
            var result = service.GetAssetLocationByID(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpGet]
        [Route("GetAssetLocation")]
        public HttpResponseMessage GetAssetLocation(HttpRequestMessage request)
        {

            var result = service.GetAssetLocation();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("MoveAsset")]
        public HttpResponseMessage MoveAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            var result = service.MoveAsset(obj);
            var message = "";
            HttpResponseMessage response = new HttpResponseMessage();
            if(result.statuscode == 1)
            {
                message = "The object is null";
            }
            else if(result.statuscode == 2)
            {
                message = result.message;
            }
            else if(result.statuscode == 3)
            {
                
            }
            else if(result.statuscode == 4)
            {

            }
            else if(result.statuscode == 5)
            {

            }
            else if(result.statuscode == 6)
            {

            }
            else if (result.statuscode == 7)
            {

            }
            else
            {

            }
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result , message = message });
            return response;
        }
        [HttpPost]
        [Route("CreateAssetLocation")]
        public HttpResponseMessage CreateAssetLocation(HttpRequestMessage request, [FromBody] AssetLocationViewModel obj)
        {
            var result = service.CreateAssetLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result , obj = request});
            return response;
        }

        [HttpPost]
        [Route("UpdateAssetLocation")]
        public HttpResponseMessage UpdateAssetLocation(HttpRequestMessage request, [FromBody] AssetLocation obj)
        {
            var result = service.UpdateAssetLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }
        [HttpPost]
        [Route("DeleteAssetLocation")]
        public HttpResponseMessage DeleteAssetLocation(HttpRequestMessage request, [FromBody] AssetLocation obj)
        {
            var result = service.DeleteAssetLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }
    }
}
