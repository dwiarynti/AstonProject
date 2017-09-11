using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class LookupListExtensions
    {
        AstonContext context = new AstonContext();

        public List<LookupList> GetCategory()
        {
            return context.LookupList.Where(p => p.Type == "Category").ToList();
        }
        public List<LookupList> GetLocationType()
        {
            return context.LookupList.Where(p => p.Type == "LocationType").ToList();
        }
        public List<LookupList> GetStatus()
        {
            return context.LookupList.Where(p => p.Type == "Status").ToList();
        }
        public List<LookupList> GetApprovalStatus()
        {
            return context.LookupList.Where(p => p.Type == "ApprovalStatus").ToList();
        }
        public LookupList GetLookupByCategoryCode(int code)
        {
            return context.LookupList.Where(p => p.Type == "Category" && p.Code == code).FirstOrDefault();
        }
        public LookupList GetLookupByStatusCode(int code)
        {
            return context.LookupList.Where(p => p.Type == "Status" && p.Code == code).FirstOrDefault();
        }
        public LookupList GetLookupByLocationTypeCode(int code)
        {
            return context.LookupList.Where(p => p.Type == "LocationType" && p.Code == code).FirstOrDefault();
        }
        public LookupList GetLookupByApprovalStatusCode(int code)
        {
            return context.LookupList.Where(p => p.Type == "ApprovalStatus" && p.Code == code).FirstOrDefault();
        }
    }
}
