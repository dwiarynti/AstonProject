using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class AssetViewModel : ViewModel
    {
        // Move Asset
        public string location { get; set; }
        public List<string> listAsset { get; set; }
        public int MovementRequestDetailID { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<bool> Ismovable { get; set; }
        //public Nullable<bool> isSearch { get; set; }
        public int Skip { get; set; }
        public string ReportName { get; set; }

        public AseetSearchResult Asset { get; set; }

    }

    public class AseetSearchResult
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public bool IsMovable { get; set; }
        public string Owner { get; set; }
        public string PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public int DepreciationDuration { get; set; }
        public string DisposedDate { get; set; }
        public string ManufactureDate { get; set; }
        public Nullable<int> CategoryCD { get; set; }
        public int StatusCD { get; set; }
        public int TotalRow { get; set; }
        public string CategoryCDName { get; set; }
        public string StatusCDName { get; set; }
        public double CurrentValue { get; set; }


    }


    public class AssetHistoryViewModel
    {
        public Asset Asset { get; set; }
        public List<HistoryViewModel> History { get; set; }
    }

    public class HistoryViewModel
    {
        public string MovementDate { get; set; }
        public string Description { get; set; }
        public string LocationName { get; set; }
        public string RequestBy { get; set; }
        public string ApprovedBy { get; set; }
        public string MovedBy { get; set; }
        public int TotalRow { get; set; }
    }
}
