using Aston.Business.Data;
using Aston.Entities;
using Aston.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml.Utils;
using System.Globalization;
using OfficeOpenXml.Style;

namespace Aston.Business
{
    public class AssetComponent
    {
        AstonContext _context = new AstonContext();
        AssetExtensions _asset = new AssetExtensions();
        LocationExtensions _location = new LocationExtensions();
        MovementRequestExtensions _movementRequest = new MovementRequestExtensions();
        GenerateCodeComponent _generatecode = new GenerateCodeComponent();
        LookupListComponent _pref = new LookupListComponent();
        public AssetViewModel GetAssetByCode (string barcode)
        {
            AssetViewModel result = new AssetViewModel();
         
            var asset = _asset.GetAssetInfoByCode(barcode);
            var categoryCDName = _pref.GetLookupByCategoryCode(asset.CategoryCD);
            var statusCDName = _pref.GetLookupByStatusCode(asset.StatusCD);

            result.Asset.Code = asset.Code;
            result.Asset.Description = asset.Description;
            result.Asset.No = asset.No;
            result.Asset.Name = asset.Name;
            result.Asset.IsMovable = asset.IsMovable;
            result.Asset.Owner = asset.Owner;
            result.Asset.PurchaseDate = asset.PurchaseDate;
            result.Asset.PurchasePrice = asset.PurchasePrice;
            result.Asset.DepreciationDuration = asset.DepreciationDuration;
            result.Asset.DisposedDate = asset.DisposedDate;
            result.Asset.ManufactureDate = asset.ManufactureDate;
            result.Asset.CategoryCD = asset.CategoryCD;
            result.Asset.CategoryCDName = categoryCDName.Value;
            result.Asset.StatusCD = asset.StatusCD;
            result.Asset.StatusCDName = statusCDName.Value;
            return result;
        }

       public List<AssetViewModel> GetAsset()
        {
            List<AssetViewModel> result = new List<AssetViewModel>();
            var asset = _asset.GetAsset();
            foreach(var item in asset)
            {
                AssetViewModel model = new AssetViewModel();
                model.Asset = new AseetSearchResult();
                var categoryCDName = _pref.GetLookupByCategoryCode(item.CategoryCD);
                var statusCDName = _pref.GetLookupByStatusCode(item.StatusCD);

                model.Asset.ID = item.ID;
                model.Asset.Code = item.Code;
                model.Asset.Description = item.Description;
                model.Asset.No = item.No;
                model.Asset.Name = item.Name;
                model.Asset.IsMovable = item.IsMovable;
                model.Asset.Owner = item.Owner;
                model.Asset.PurchaseDate = item.PurchaseDate;
                model.Asset.PurchasePrice = item.PurchasePrice;
                model.Asset.DepreciationDuration = item.DepreciationDuration;
                model.Asset.DisposedDate = item.DisposedDate;
                model.Asset.ManufactureDate = item.ManufactureDate;
                model.Asset.CategoryCD = item.CategoryCD;              
                model.Asset.CategoryCDName = categoryCDName.Value;             
                model.Asset.StatusCD = item.StatusCD;
                model.Asset.StatusCDName = statusCDName.Value;

                result.Add(model);

            }
            return result;

        }

        public AssetViewModel GetAssetByID(int id)
        {
            AssetViewModel result = new AssetViewModel();
            result.Asset = new AseetSearchResult();

            var asset = _asset.GetAssetInfoByID(id);
            var categoryCDName = _pref.GetLookupByCategoryCode(asset.CategoryCD);
            var statusCDName = _pref.GetLookupByStatusCode(asset.StatusCD);

            result.Asset.Code = asset.Code;
            result.Asset.Description = asset.Description;
            result.Asset.No = asset.No;
            result.Asset.Name = asset.Name;
            result.Asset.IsMovable = asset.IsMovable;
            result.Asset.Owner = asset.Owner;
            result.Asset.PurchaseDate = asset.PurchaseDate;
            result.Asset.PurchasePrice = asset.PurchasePrice;
            result.Asset.DepreciationDuration = asset.DepreciationDuration;
            result.Asset.DisposedDate = asset.DisposedDate;
            result.Asset.ManufactureDate = asset.ManufactureDate;
            result.Asset.CategoryCD = asset.CategoryCD;
            result.Asset.CategoryCDName = categoryCDName.Value;
            result.Asset.StatusCD = asset.StatusCD;
            result.Asset.StatusCDName = statusCDName.Value;

            return result;
        }

        public bool CreateAsset(AssetViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            if(obj != null)
            {
                try
                {
                    obj.Asset.No = _asset.GetLastNumberAsset();
                    obj.SubCategory = _generatecode.SubCategoryAsset(Convert.ToInt16(obj.Asset.CategoryCD));
                    obj.Number = _generatecode.Number(obj.Asset.No);
                    obj.Asset.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, obj.Number);


                    Asset asset = new Asset();
                    asset.ID = obj.Asset.ID;
                    asset.Code = obj.Asset.Code;
                    asset.Description = obj.Asset.Description;
                    asset.No = obj.Number;
                    asset.Name = obj.Asset.Name;
                    asset.IsMovable = obj.Asset.IsMovable;
                    asset.Owner = obj.Asset.Owner;
                    asset.PurchaseDate =    obj.Asset.PurchaseDate.Replace("/", string.Empty);
                    asset.PurchasePrice = obj.Asset.PurchasePrice;
                    asset.DepreciationDuration = obj.Asset.DepreciationDuration;
                    asset.DisposedDate = obj.Asset.DisposedDate != null ? obj.Asset.DisposedDate.Replace("/", string.Empty) : null;
                    asset.ManufactureDate = obj.Asset.ManufactureDate.Replace("/", string.Empty);
                    asset.CategoryCD = Convert.ToInt16(obj.Asset.CategoryCD);
                    asset.StatusCD = obj.Asset.StatusCD;
                    asset.CreatedDate = obj.CreatedDate;//DateTime.Now;
                    asset.CreatedBy = obj.CreatedBy;

                    _context.Asset.Add(asset);
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool UpdateAsset(AssetViewModel obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var asset = _asset.GetAssetInfoByID(obj.Asset.ID);
            if(asset!= null)
            {
                try
                {
                  
                    obj.SubCategory = _generatecode.SubCategoryAsset(Convert.ToInt16(obj.Asset.CategoryCD));
                    obj.Asset.Code = _generatecode.GenerateCode(obj.CompanyCode, obj.ApplicationCode, obj.MainCategory, obj.SubCategory, asset.No);
                    if (asset.Code != obj.Asset.Code)
                    {

                        asset.Code = obj.Asset.Code;
                    }
                    asset.Description = obj.Asset.Description;
                    asset.Name = obj.Asset.Name;
                    asset.IsMovable = obj.Asset.IsMovable;
                    asset.Owner = obj.Asset.Owner;
                    asset.PurchaseDate = obj.Asset.PurchaseDate.Replace("/", string.Empty); 
                    asset.PurchasePrice = obj.Asset.PurchasePrice;
                    asset.DepreciationDuration = obj.Asset.DepreciationDuration;
                    asset.ManufactureDate = obj.Asset.ManufactureDate.Replace("/", string.Empty);
                    asset.CategoryCD = Convert.ToInt16(obj.Asset.CategoryCD);
                    asset.StatusCD = obj.Asset.StatusCD;
                    asset.UpdatedBy = obj.UpdatedBy;
                    asset.UpdatedDate = obj.UpdatedDate;//DateTime.Now;

                    _context.Entry(asset).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool DeleteAsset(Asset obj)
        {
            bool result;
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            var asset = _asset.GetAssetInfoByID(obj.ID);
            if(asset != null)
            {
                try
                {
                    asset.DeletedBy = obj.DeletedBy;
                    asset.DeletedDate = obj.DeletedDate;//DateTime.Now;
                    _context.Entry(asset).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch(Exception ex)
                {
                    result = false;
                    transaction.Rollback();
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public List<Asset> GetAssetByCategoryCode(int code)
        {
          return  _asset.GetAssetByCategoryCode(code);
        }
       
        public List<AssetViewModel> SearchAsset(AssetViewModel obj)
        {
            List<AssetViewModel> result = new List<AssetViewModel>();
                if (obj != null)
                {
                    result = _asset.SearchAsset_SP(Convert.ToInt16(obj.Asset.CategoryCD), obj.Ismovable, obj.Asset.Owner, obj.Skip);

                }
                
            return result;
        }

        public byte[] Download(AssetViewModel obj)
        {
            byte[] bytes = new byte[0];
            if (obj.ReportName == "Total Asset Value")
            {
                bytes = ReportTotalAssetValue(obj);
            }else if (obj.ReportName == "Zero Value")
            {
                bytes = ReportAssetZeroValue(obj);

            }
            else if(obj.ReportName == "Mismatch")
            {
                bytes = MismatchReport(obj);
            }
            else if (obj.ReportName == "LostAsset")
            {
                bytes = LostAssetReport(obj);
            }
            return bytes;

        }

        public byte[] ReportTotalAssetValue(AssetViewModel obj)
        {
            byte[] bytes;
            MemoryStream stream = new MemoryStream();
            var listdata = _asset.ReportAsset_SP(Convert.ToInt16(obj.Asset.CategoryCD), obj.Ismovable, obj.Asset.Owner);
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Asset - Total Asset Value");
                //First add the headers
                InsertColumnHeaders(worksheet, obj.ReportName);
                var lastrow = 1;
                double TotalCurrentValue = 0;
                //Add values
                foreach (var data in listdata)
                {
                    lastrow = lastrow + 1;
                    InsertRowData(worksheet, data.Asset, lastrow, obj.ReportName);
                    TotalCurrentValue = TotalCurrentValue + data.Asset.CurrentValue;
                }

                lastrow = lastrow + 1;
                InsertColumnFooters(worksheet, lastrow, TotalCurrentValue);

                var cellrange = worksheet.Cells["A1:L" + lastrow];
                SetBorder(cellrange);

                bytes = package.GetAsByteArray();
            }
            return bytes;

        }

        public byte[] ReportAssetZeroValue(AssetViewModel obj)
        {
            byte[] bytes;
            MemoryStream stream = new MemoryStream();
            var listdata = _asset.ReportAssetZeroValue_SP(Convert.ToInt16(obj.Asset.CategoryCD), obj.Ismovable, obj.Asset.Owner);
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Asset - Zero Value");
                //First add the headers
                InsertColumnHeaders(worksheet, obj.ReportName);
                var lastrow = 1;
                //Add values
                foreach (var data in listdata)
                {
                    lastrow = lastrow + 1;
                    InsertRowData(worksheet, data.Asset, lastrow, obj.ReportName);
                }

                var cellrange = worksheet.Cells["A1:K" + lastrow];
                SetBorder(cellrange);

                bytes = package.GetAsByteArray();
            }
            return bytes;

        }

        public byte[] MismatchReport(AssetViewModel obj)
        {
            byte[] bytes;
            MemoryStream stream = new MemoryStream();
            var listdata = _asset.MismatchReport_SP(Convert.ToInt16(obj.Asset.CategoryCD), obj.Ismovable, obj.Asset.Owner);
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Asset - Mismatch");
                //First add the headers
                InsertColumnHeadersMismatch(worksheet);
                var lastrow = 1;
                //Add values
                foreach (var data in listdata)
                {
                    lastrow = lastrow + 1;
                    InsertRowDataMismatch(worksheet, data, lastrow);
                }

                var cellrange = worksheet.Cells["A1:H" + lastrow];
                SetBorder(cellrange);

                bytes = package.GetAsByteArray();
            }
            return bytes;

        }

        public byte[] LostAssetReport(AssetViewModel obj)
        {
            byte[] bytes;
            MemoryStream stream = new MemoryStream();
            var listdata = _asset.LostAssetReport_SP(Convert.ToInt16(obj.Asset.CategoryCD), obj.Ismovable, obj.Asset.Owner);
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Asset - Lost");
                //First add the headers
                InsertColumnHeadersLostAsset(worksheet);
                var lastrow = 1;
                //Add values
                foreach (var data in listdata)
                {
                    lastrow = lastrow + 1;
                    InsertRowDataLostAsset(worksheet, data, lastrow);
                }
                var cellrange = worksheet.Cells["A1:G" + lastrow];
                SetBorder(cellrange);

                bytes = package.GetAsByteArray();
            }
            return bytes;

        }

        public void InsertColumnHeaders(ExcelWorksheet worksheet, string reportName)
        {
            worksheet.Cells["A1"].Value = "No";
            worksheet.Cells["B1"].Value = "Code";
            worksheet.Cells["C1"].Value = "Name";
            worksheet.Cells["D1"].Value = "Description";
            worksheet.Cells["E1"].Value = "Category";
            worksheet.Cells["F1"].Value = "Owner";
            worksheet.Cells["G1"].Value = "Is Movable Asset";
            worksheet.Cells["H1"].Value = "Status";
            worksheet.Cells["I1"].Value = "Purchase Date";
            worksheet.Cells["J1"].Value = "Purchase Price";
            worksheet.Cells["K1"].Value = "Depreciation Duration";
            if (reportName == "Total Asset Value")
            {
                worksheet.Cells["L1"].Value = "Current Value";
            }

            worksheet.Column(2).Width = 30;
            worksheet.Column(3).Width = 50;
            worksheet.Column(4).Width = 10;
            worksheet.Column(5).Width = 10;
            worksheet.Column(6).Width = 15;
            worksheet.Column(7).Width = 10;
            worksheet.Column(8).Width = 20;
            worksheet.Column(9).Width = 17;
            worksheet.Column(10).Width = 20;
            worksheet.Column(11).Width = 20;
            worksheet.Column(12).Width = 20;

            worksheet.Cells["A1:L1"].Style.Font.Bold = true;
        }

        public void InsertRowData(ExcelWorksheet worksheet, AseetSearchResult Asset, int lastrow, string reportName)
        {
            //var row = worksheet
            worksheet.Cells["A"+ lastrow].Value = lastrow-1;

            worksheet.Cells["B"+ lastrow].Value = Asset.Code;
            worksheet.Cells["B" + lastrow].AutoFitColumns();

            worksheet.Cells["C"+ lastrow].Value = Asset.Name;
            worksheet.Cells["D"+ lastrow].Value = Asset.Description;
            worksheet.Cells["E"+ lastrow].Value = Asset.CategoryCDName;
            worksheet.Cells["F"+ lastrow].Value = Asset.Owner;
            worksheet.Cells["G" + lastrow].Value = Asset.IsMovable;
            worksheet.Cells["H" + lastrow].Value = Asset.StatusCDName;

            var FormatedDate = GetFormatedDate(Asset.PurchaseDate);
            worksheet.Cells["I" + lastrow].Style.Numberformat.Format = "dd/mm/yyyy";
            worksheet.Cells["I" + lastrow].Formula = "=DATE("+ FormatedDate + ")";

            worksheet.Cells["J" + lastrow].Value = Asset.PurchasePrice;
            worksheet.Cells["K" + lastrow].Value = Asset.DepreciationDuration;
            if (reportName == "Total Asset Value")
            {
                worksheet.Cells["L" + lastrow].Value = Asset.CurrentValue;
            }

        }

        public void InsertColumnFooters(ExcelWorksheet worksheet, int lastrow, double TotalCurrentValue)
        {
            worksheet.Cells["A" + lastrow].Value = "Total";
            worksheet.Cells["A" + lastrow].Style.Font.Bold = true;
            worksheet.Cells["A" + lastrow + ":K" + lastrow].Merge = true;
            worksheet.Cells["A" + lastrow + ":K" + lastrow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells["L" + lastrow].Value = TotalCurrentValue;
            worksheet.Cells["L" + lastrow].Style.Font.Bold = true;
        }

        public void SetBorder(ExcelRange cellrange)
        {
            cellrange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cellrange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cellrange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cellrange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        public string GetFormatedDate(string date)
        {
            var dt = DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture);
            return dt.ToString("yyyy,MM,dd", CultureInfo.InvariantCulture);
        }

        public AssetHistoryViewModel AssetMovementHistory(AssetViewModel obj)
        {
            var result = new AssetHistoryViewModel();
            result.Asset = _asset.GetAssetInfoByID(obj.Asset.ID);
            result.History = _movementRequest.AssetHistory_SP(obj.Asset.ID, obj.Skip);
            return result;
        }

        public void InsertColumnHeadersMismatch(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1"].Value = "No";
            worksheet.Cells["B1"].Value = "Code";
            worksheet.Cells["C1"].Value = "Asset Name";
            worksheet.Cells["D1"].Value = "Category";
            worksheet.Cells["E1"].Value = "Actual Location";
            worksheet.Cells["F1"].Value = "Current Location";
            worksheet.Cells["G1"].Value = "Status";
            worksheet.Cells["H1"].Value = "Record Date";

            //worksheet.Column(1).Width = 10;
            //worksheet.Column(2).Width = 35;
            worksheet.Column(3).Width = 35;
            worksheet.Column(4).Width = 15;
            worksheet.Column(5).Width = 20;
            worksheet.Column(6).Width = 20;
            worksheet.Column(7).Width = 15;
            worksheet.Column(8).Width = 17;

            worksheet.Cells["A1:G1"].Style.Font.Bold = true;
        }

        public void InsertRowDataMismatch(ExcelWorksheet worksheet, MismatchReportViewModel MismatchData, int lastrow)
        {
            //var row = worksheet
            worksheet.Cells["A" + lastrow].Value = lastrow - 1;
            worksheet.Cells["A" + lastrow].AutoFitColumns();

            worksheet.Cells["B" + lastrow].Value = MismatchData.AssetCode;
            worksheet.Cells["B" + lastrow].AutoFitColumns();

            worksheet.Cells["C" + lastrow].Value = MismatchData.AssetName;
            worksheet.Cells["D" + lastrow].Value = MismatchData.Category;
            worksheet.Cells["E" + lastrow].Value = MismatchData.LocationName;
            worksheet.Cells["F" + lastrow].Value = MismatchData.CurrentLocationName;
            worksheet.Cells["G" + lastrow].Value = MismatchData.Status;

            if (MismatchData.RecordDate != null)
            {
                var FormatedDate = GetFormatedDate(MismatchData.RecordDate);
                worksheet.Cells["H" + lastrow].Style.Numberformat.Format = "dd/mm/yyyy";
                worksheet.Cells["H" + lastrow].Formula = "=DATE(" + FormatedDate + ")";
            }
            else
            {
                worksheet.Cells["H" + lastrow].Value = MismatchData.RecordDate;
            }
        }

        public void InsertColumnHeadersLostAsset(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1"].Value = "No";
            worksheet.Cells["B1"].Value = "Code";
            worksheet.Cells["C1"].Value = "Asset Name";
            worksheet.Cells["D1"].Value = "Category";
            worksheet.Cells["E1"].Value = "Latest Date";
            worksheet.Cells["F1"].Value = "Latest Location";
            worksheet.Cells["G1"].Value = "Status";
            
            worksheet.Column(3).Width = 35;
            worksheet.Column(4).Width = 15;
            worksheet.Column(5).Width = 20;
            worksheet.Column(6).Width = 20;
            worksheet.Column(7).Width = 15;
            worksheet.Column(8).Width = 17;

            worksheet.Cells["A1:G1"].Style.Font.Bold = true;
        }

        public void InsertRowDataLostAsset(ExcelWorksheet worksheet, MismatchReportViewModel MismatchData, int lastrow)
        {
            //var row = worksheet
            worksheet.Cells["A" + lastrow].Value = lastrow - 1;
            worksheet.Cells["A" + lastrow].AutoFitColumns();

            worksheet.Cells["B" + lastrow].Value = MismatchData.AssetCode;
            worksheet.Cells["B" + lastrow].AutoFitColumns();

            worksheet.Cells["C" + lastrow].Value = MismatchData.AssetName;
            worksheet.Cells["D" + lastrow].Value = MismatchData.Category;

            if (MismatchData.RecordDate != null)
            {
                var FormatedDate = GetFormatedDate(MismatchData.RecordDate);
                worksheet.Cells["E" + lastrow].Style.Numberformat.Format = "dd/mm/yyyy";
                worksheet.Cells["E" + lastrow].Formula = "=DATE(" + FormatedDate + ")";
            }
            else
            {
                worksheet.Cells["E" + lastrow].Value = MismatchData.RecordDate;
            }

            worksheet.Cells["F" + lastrow].Value = MismatchData.LocationName;
            worksheet.Cells["G" + lastrow].Value = MismatchData.Status;


        }
    }
}
