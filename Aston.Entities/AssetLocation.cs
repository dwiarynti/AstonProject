﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class AssetLocation
    {
        public int ID { get; set; }
        public int AssetID { get; set; }
        public int LocationID { get; set; }
        public Nullable<bool> OnTransition { get; set; }
        public Nullable<int> MovementRequestDetailID { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
