using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class ResultViewModel
    {
        public bool status { get; set; }
        public int statuscode { get; set; }
        public string  message { get; set; }
        public MovementRequestViewModel movementRequest { get; set; }
        public List<string> listAsset { get; set; }
        public List<AssetOpnameTransactionViewModel> assetlocation { get; set; }
        public List<Asset> asset{ get; set; }
        public List<AssetOpnameTransactionViewModel> assetOpname { get; set; }
        


    }
}
