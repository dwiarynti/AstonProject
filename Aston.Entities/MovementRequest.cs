﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequest
    {
        public int ID { get; set; }
        public string MovementDate { get; set; }
        public string Description { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
