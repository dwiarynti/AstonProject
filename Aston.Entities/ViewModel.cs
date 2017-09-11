﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
   
    public class ViewModel
    {
        public string CompanyCode { get; set; }
        public string ApplicationCode { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string Number { get; set; }
        public string AssetName { get; set; }
        public string LocationName { get; set; }
        public string CategoryCDName { get; set; }
        public string ApprovalStatusName { get; set; }
        public string StatusCDName { get; set; }
        public string LocationTypeCDName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string RequestToName { get; set; }

    }
}
