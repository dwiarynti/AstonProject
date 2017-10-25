using Aston.Entities;
using Aston.WebApi.Helper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.WebApi.Helpers
{
    public class DateExtension
    {
        private AppSettingExtension _appSetting;

        public DateExtension(AppSettingExtension appSetting)
        {
            _appSetting = appSetting;
        }

        public DateTime GetDateTime()
        {
            DateTime dateTime;
            if (!_appSetting.IsCurrentDate)
            {
                dateTime = new DateTime(_appSetting.Year, _appSetting.Month, _appSetting.Day, _appSetting.Hour, _appSetting.Minute, _appSetting.Second);
            }
            else
            {
                dateTime = DateTime.Now;
            }
            return dateTime;
        }

        public string GetDateTime(string format)
        {
            DateTime dateTime;
            if (!_appSetting.IsCurrentDate)
            {
                dateTime = new DateTime(_appSetting.Year, _appSetting.Month, _appSetting.Day, _appSetting.Hour, _appSetting.Minute, _appSetting.Second);
            }
            else
            {
                dateTime = DateTime.Now;
            }
            return dateTime.ToString(format);
        }
    }
}
