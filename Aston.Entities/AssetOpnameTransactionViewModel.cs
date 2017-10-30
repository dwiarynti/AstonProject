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
        public string AssetBarcode { get; set; }
        public string AssetName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string RecordDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }


        public AssetTransactionViewModel AssetLatest { get; set; }
        public bool isOpname { get; set; }
        public List<string> AssetOpname { get; set; }

        public List<int> AssetIDList { get; set; }
        
    }
    public class AssetTransactionViewModel
    {
        public int AssetID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
   
   
}
