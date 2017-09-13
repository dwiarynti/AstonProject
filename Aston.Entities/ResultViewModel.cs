using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class ResultViewModel
    {
        public bool resultstatus { get; set; }
        public string  resultmessage { get; set; }
        public MovementRequestViewModel movementrequest { get; set; }

        public List<AssetLocation> exceededasset { get; set; }
        //public List<Asset> exceededasset { get; set; }

        public List<Asset> wrongasset { get; set; }
        public List<AssetLocation> AssetWrongLocation { get; set; }


    }
}
