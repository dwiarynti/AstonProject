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

namespace Aston.WebApi.Helper
{
    public class AppSettingHelper
    {
        private readonly AppSetting _serviceSettings;
        public AppSettingHelper(IOptions<AppSetting> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;           
        }
        public string CompanyCode { get { return _serviceSettings.companyCode; }  }
        public string applicationCode { get { return _serviceSettings.applicationCode; } }
        public string MainCategoryAsset { get { return _serviceSettings.MainCategoryAsset; } }
        public string MainCategoryLocation { get { return _serviceSettings.MainCategoryLocation; } }
        public string IsCurrentDate { get { return _serviceSettings.IsCurrentDate; } }
        public string Year { get { return _serviceSettings.Year; } }
        public string Month { get { return _serviceSettings.Month; } }
        public string Day { get { return _serviceSettings.Day; } }
        public string Hour { get { return _serviceSettings.Hour; } }
        public string Minute { get { return _serviceSettings.Minute; } }
        public string Second { get { return _serviceSettings.Second; } }

    }
}
