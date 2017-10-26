using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using Aston.Entities;
using System.Net;
using Aston.WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
 
    [Route("api/AssetLocation")]
    public class AssetLocationController : Controller
    {
        public AssetLocationComponent service = new AssetLocationComponent();
        public AssetOpnameTransactionComponent opnameservice = new AssetOpnameTransactionComponent();
        private DateExtension _dateExtension;

        public AssetLocationController(DateExtension dateExtension)
        {
            _dateExtension = dateExtension;
        }

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
        [Route("GetAssetLocationByMovementDetailID/{id}")]
        public HttpResponseMessage GetAssetLocationByMovementDetailID(HttpRequestMessage request, int id)
        {
            var result = service.GetAssetLocationByMovementDetailID(id);
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
            obj.CreatedDate = _dateExtension.GetDateTime();

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
                message = "The Move asset is more than request";
            }
            else if(result.statuscode == 4)
            {
                message = "The Location barcode is not same with the requested barcode";
            }
            else if(result.statuscode == 5)
            {
                message = "The item of barcode not found in system";
            }
            else if(result.statuscode == 6)
            {
                message = "The category of the barcode item is not match ";
            }
            else if(result.statuscode == 7)
            {
                message = "success";
            }
            else if(result.statuscode == 8)
            {
                message = "The Location is not found in  system";
            }
         
            else
            {
                message = "System error ,please contact your admin";
            }
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.status , message = message });
            return response;
        }
        [HttpPost]
        [Route("TransactionAsset")]
        public HttpResponseMessage TransactionAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            obj.UpdatedDate = _dateExtension.GetDateTime();

            var result = service.TransactionAsset(obj);
            var message = "";
            HttpResponseMessage response = new HttpResponseMessage();
            if (result.statuscode == 1)
            {
                message = "The object is null";
            }
            else if (result.statuscode == 2)
            {
                message = result.message;
            }
            else if (result.statuscode == 3)
            {
                message = "The Move asset more than request";
            }
            else if (result.statuscode == 4)
            {
                message = "The Location not same with the request";
            }
            else if (result.statuscode == 5)
            {
                message = "The barcode not found in system";
            }
            else if (result.statuscode == 6)
            {
                message = "The category of the item not correct ";
            }
            else if (result.statuscode == 7)
            {
                message = "success";
            }

            else
            {
                message = "System error ,please contact your admin";
            }
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.status, message = message, obj = result.listAsset });
            return response;
        }

        [HttpPost]
        [Route("CreateAssetLocation")]
        public HttpResponseMessage CreateAssetLocation(HttpRequestMessage request, [FromBody] AssetLocationViewModel obj)
        {
            obj.CreatedDate = _dateExtension.GetDateTime();

            var result = service.CreateAssetLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.status});
            return response;
        }

        [HttpPost]
        [Route("UpdateAssetLocation")]
        public HttpResponseMessage UpdateAssetLocation(HttpRequestMessage request, [FromBody] AssetLocation obj)
        {
            obj.UpdatedDate = _dateExtension.GetDateTime();

            var result = service.UpdateAssetLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }
        [HttpPost]
        [Route("DeleteAssetLocation")]
        public HttpResponseMessage DeleteAssetLocation(HttpRequestMessage request, [FromBody] AssetLocation obj)
        {
            obj.DeletedDate = _dateExtension.GetDateTime();

            var result = service.DeleteAssetLocation(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }

        [HttpGet]
        [Route("AssetLocation_Pagination/{Skip}")]
        public HttpResponseMessage AssetLocation_Pagination(HttpRequestMessage request, int Skip)
        {
            var result = service.AssetLocation_Pagination(Skip);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("GetAssetStockOpname")]
        public HttpResponseMessage GetAssetStockOpname(HttpRequestMessage request, [FromBody] AssetOpnameTransactionViewModel obj)
        {
            var result = opnameservice.GetAssetStockOpname(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true , obj = result });
            return response;
        }
    }
}
