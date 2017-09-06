using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class PrefExtensions
    {
        AstonContext context = new AstonContext();

        public List<Pref> GetCategory()
        {
            return context.Pref.Where(p => p.Type == "Category").ToList();
        }
        public List<Pref> GetLocationType()
        {
            return context.Pref.Where(p => p.Type == "LocationType").ToList();
        }
        public List<Pref> GetStatus()
        {
            return context.Pref.Where(p => p.Type == "Status").ToList();
        }
        public Pref GetPrefByCategoryCode(int code)
        {
            return context.Pref.Where(p => p.Type == "Category" && p.Code == code).FirstOrDefault();
        }
        public Pref GetPrefByStatusCode(int code)
        {
            return context.Pref.Where(p => p.Type == "Status" && p.Code == code).FirstOrDefault();
        }
        public Pref GetPrefByLocationTypeCode(int code)
        {
            return context.Pref.Where(p => p.Type == "LocationType" && p.Code == code).FirstOrDefault();
        }
    }
}
