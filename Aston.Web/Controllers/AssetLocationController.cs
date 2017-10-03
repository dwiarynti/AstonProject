using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aston.Web.Process;
using System.Net.Http;
using Aston.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Aston.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/AssetLocation")]
    public class AssetLocationController : Controller
    {
        private readonly AssetLocationProcess _assetLocationProcess;
        public AssetLocationController(AssetLocationProcess assetLocationProcess)
        {
            _assetLocationProcess = assetLocationProcess;
        }
        [Route("GetAssetLocationByLocationID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAssetInfo(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.GetAssetLocationByLocationID(id);
            return response;
        }
        [Route("GetAssetLocationByMovementDetailID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAssetLocationByMovementDetailID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.GetAssetLocationByMovementDetailID(id);
            return response;
        }

        [Route("GetAssetLocationByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAssetByID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.GetAssetLocationByID(id);
            return response;
        }

        [Route("GetAssetLocation")]
        [HttpGet]
        public HttpResponseMessage GetAsset(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.GetAssetLocation();
            return response;
        }

        [HttpPost]
        [Route("MoveAsset")]
        public HttpResponseMessage MoveAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.MoveAsset(obj);
            return response;
        }
        [HttpPost]
        [Route("TransactionAsset")]
        public HttpResponseMessage TransactionAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.TransactionAsset(obj);
            return response;
        }

        [HttpPost]
        [Route("CreateAssetLocation")]
        public HttpResponseMessage CreateAssetLocation(HttpRequestMessage request, [FromBody] AssetLocationViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.CreatedBy = User.Identity.Name;
            response = _assetLocationProcess.CreateAssetLocation(obj);
            return response;
        }

        [HttpPost]
        [Route("UpdateAssetLocation")]
        public HttpResponseMessage UpdateAssetLocation(HttpRequestMessage request, [FromBody] AssetLocation obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = User.Identity.Name;
            response = _assetLocationProcess.UpdateAssetLocation(obj);
            return response;
        }
        [HttpPost]
        [Route("DeleteAssetLocation")]
        public HttpResponseMessage MoveAsset(HttpRequestMessage request, [FromBody] AssetLocation obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.DeletedBy = User.Identity.Name;
            response = _assetLocationProcess.DeleteAssetLocation(obj);
            return response;
        }

        [Route("AssetLocation_Pagination/{Skip}")]
        [HttpGet]
        public HttpResponseMessage AssetLocation_Pagination(HttpRequestMessage request, int Skip)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetLocationProcess.AssetLocation_Pagination(Skip);
            return response;
        }

    }
}