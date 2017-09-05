using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class AppSetting
    {
        public string serviceUrl { get; set; }
        public string companyCode { get; set; }
        public string applicationCode { get; set; }
        public string MainCategoryAsset { get; set; }
        public string MainCategoryLocation { get; set; }
    }
}
