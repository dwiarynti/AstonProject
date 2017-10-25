using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class Asset
    {
        public Asset()
        {
            this.AssetLocation = new HashSet<AssetLocation>();
        }
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
        public int CategoryCD { get; set; }
        public int StatusCD { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }

        public virtual ICollection<AssetLocation> AssetLocation { get; set; }
        public virtual ICollection<AssetOpnameTransaction> AssetOpnameTransaction { get; set; }
    }
}
