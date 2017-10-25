using Aston.Entities;
using Aston.WebApi.Helper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.WebApi.Helpers
{
    public class AppSettingExtension : AppSettingHelper
    {
        private readonly AppSetting _appSetting;
        public AppSettingExtension(IOptions<AppSetting> serviceSettings) : base(serviceSettings)
        {
            _appSetting = serviceSettings.Value;
        }

        public new string CompanyCode { get { return _appSetting.companyCode; }  }
        public new string applicationCode { get { return _appSetting.applicationCode; } }
        public new string MainCategoryAsset { get { return _appSetting.MainCategoryAsset; } }
        public new string MainCategoryLocation { get { return _appSetting.MainCategoryLocation; } }
        public new bool IsCurrentDate { get { return _appSetting.IsCurrentDate == "1" ? true : false; } }
        public new int Year { get { return int.Parse(_appSetting.Year); } }
        public new int Month { get { return int.Parse(_appSetting.Month); } }
        public new int Day { get { return int.Parse(_appSetting.Day); } }
        public new int Hour { get { return int.Parse(_appSetting.Hour); } }
        public new int Minute { get { return int.Parse(_appSetting.Minute); } }
        public new int Second { get { return int.Parse(_appSetting.Second); } }
    }
}
