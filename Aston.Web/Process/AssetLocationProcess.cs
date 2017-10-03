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
    public class AssetLocationProcess : ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
        public AssetLocationProcess(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }
        public HttpResponseMessage GetAssetLocationByLocationID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/GetAssetLocationByLocationID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetAssetLocationByMovementDetailID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/GetAssetLocationByMovementDetailID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetAssetLocationByID(int id)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/GetAssetLocationByID/" + id;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage GetAssetLocation()
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/GetAssetLocation/";
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
        public HttpResponseMessage MoveAsset(AssetViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/MoveAsset";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage TransactionAsset(AssetViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/TransactionAsset";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }

        public HttpResponseMessage CreateAssetLocation(AssetLocationViewModel obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/CreateAssetLocation";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }

        public HttpResponseMessage UpdateAssetLocation(AssetLocation obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/UpdateAssetLocation";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage DeleteAssetLocation(AssetLocation obj)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/DeleteAssetLocation";
            result = REST(requestUri, RESTConstants.POST, JsonConvert.SerializeObject(obj));
            return result;
        }
        public HttpResponseMessage AssetLocation_Pagination(int Skip)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            string requestUri = "api/AssetLocation/AssetLocation_Pagination/" + Skip;
            result = REST(requestUri, RESTConstants.GET);
            return result;
        }
    }
}
