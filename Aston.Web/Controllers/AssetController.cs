using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Aston.Web.Process;
using Aston.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Aston.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Asset")]
    public class AssetController : Controller
    {
        private readonly AssetProcess _assetProcess;

        public AssetController(AssetProcess assetProcess)
        {
            _assetProcess = assetProcess;
        }

        [Route("GetAssetByCode/{barcode}")]
        [HttpGet]
        public HttpResponseMessage GetAssetInfo(HttpRequestMessage request,string barcode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetProcess.GetAssetByCode(barcode);
            return response;
        }

        [Route("GetAssetByID/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAssetByID(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetProcess.GetAssetByID(id);
            return response;
        }

        [Route("GetAsset")]
        [HttpGet]
        public HttpResponseMessage GetAsset(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _assetProcess.GetAsset();
            return response;
        }


        [HttpPost]
        [Route("CreateAsset")]
        public HttpResponseMessage CreateAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.CreatedBy ="1";
            response = _assetProcess.CreateAsset(obj);
            return response;
        }

        [HttpPost]
        [Route("UpdateAsset")]
        public HttpResponseMessage UpdateAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.UpdatedBy = "1";
            response = _assetProcess.UpdateAsset(obj);
            return response;
        }

        [HttpPost]
        [Route("DeleteAsset")]
        public HttpResponseMessage DeleteAsset(HttpRequestMessage request, [FromBody] Asset obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            obj.DeletedBy = "1";
            response = _assetProcess.DeleteAsset(obj);
            return response;
        }

    }
}