﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequestDetail
    {
        public int ID { get; set; }
        public int MovementRequestID { get; set; }
        public string Description { get; set; }
        public string AssetCategoryCD { get; set; }
        public int Quantity { get; set; }
        public int RequestTo { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
