using Aston.Entities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace Aston.Web.Base
{
    public class ProcessComponent
    {
        private readonly AppSetting _serviceSettings;
   
        public ProcessComponent(IOptions<AppSetting> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;           
        }
        public  HttpResponseMessage REST(string requestUri, string restType)
        {
            return REST(requestUri, restType, null);
        }
        
        public  HttpResponseMessage REST(string requestUri, string restType, string postBody)
        {
            HttpResponseMessage result = default(HttpResponseMessage);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_serviceSettings.serviceUrl);
                
                if (restType == "GET")
                    result = client.GetAsync(requestUri).Result;
                else if (restType == "POST")
                {
                    result = client.PostAsync(requestUri, new StringContent(postBody, Encoding.UTF8,MediaType.Json)).Result;
                }
                else if (restType == "DELETE")
                    result = client.DeleteAsync(requestUri).Result;
                else
                    result = client.GetAsync(requestUri).Result;

                result.EnsureSuccessStatusCode();
            }
            return result;
        }

        public string CompanyCode()
        {
            return _serviceSettings.companyCode;
        }
        public string applicationCode()
        {
            return _serviceSettings.applicationCode;
        }

        public string MainCategoryAsset()
        {
            return _serviceSettings.MainCategoryAsset;
        }
        
        public string MainCategoryLocation()
        {
            return _serviceSettings.MainCategoryLocation;
        }

    }
}
