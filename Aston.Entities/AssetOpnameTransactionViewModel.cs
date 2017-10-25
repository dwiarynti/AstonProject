using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class AssetOpnameTransactionViewModel
    {
        public int ID { get; set; }
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string RecordDate { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
