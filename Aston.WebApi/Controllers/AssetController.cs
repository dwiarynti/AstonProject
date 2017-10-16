using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using Aston.Entities;
using Aston.Entities.DataContext;
using System.Net;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aston.WebApi.Controllers
{
   
  
    [Route("api/Asset")]
    public class AssetController : Controller
    {
        public AssetComponent service = new AssetComponent();


        [HttpGet]
        [Route("GetAssetByCode/{barcode}")]
        public HttpResponseMessage GetAssetByCode(HttpRequestMessage request ,string barcode)
        {
            var result = service.GetAssetByCode(barcode);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpGet]
        [Route("GetAssetByID/{id}")]
        public HttpResponseMessage GetAssetByID(HttpRequestMessage request, int id  )
        {
            var result = service.GetAssetByID(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }
        [HttpGet]
        [Route("GetAssetByCategoryCode/{id}")]
        public HttpResponseMessage GetAssetByCategoryCode(HttpRequestMessage request, int id)
        {
            var result = service.GetAssetByCategoryCode(id);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpGet]
        [Route("GetAsset")]
        public HttpResponseMessage GetAsset(HttpRequestMessage request)
        {

            var result = service.GetAsset();
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }



        [HttpPost]
        [Route("CreateAsset")]
        public HttpResponseMessage CreateAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            var result = service.CreateAsset(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }

        [HttpPost]
        [Route("UpdateAsset")]
        public HttpResponseMessage UpdateAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            var result = service.UpdateAsset(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }

        [HttpPost]
        [Route("SearchAsset")]
        public HttpResponseMessage SearchAsset(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            var result = service.SearchAsset(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("DeleteAsset")]
        public HttpResponseMessage DeleteAsset(HttpRequestMessage request, [FromBody] Asset obj)
        {
            var result = service.DeleteAsset(obj);
            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result });
            return response;
        }

        [HttpPost]
        [Route("download")]
        public HttpResponseMessage download(HttpRequestMessage request, [FromBody] AssetViewModel obj)
        {
            var result = service.Download(obj);
            byte[] fileData = result;
            var fileContent = new ByteArrayContent(fileData);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK) {Content = fileContent};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Asset Report-"+obj.ReportName+".xlsx"
            };
            return response;
        }

    }
}
