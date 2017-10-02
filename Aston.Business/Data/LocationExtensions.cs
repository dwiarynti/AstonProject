using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aston.Business.Data
{
    public class LocationExtensions
    {
        AstonContext context = new AstonContext();

        public Location GetLocationByCode(string code)
        {
            var obj = context.Location.Where(p => p.Code == code).FirstOrDefault();
            return obj;
        }
        public Location GetLocationByID(int id)
        {
            var obj = context.Location.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }
        public List<Location> GetLocation()
        {
            var obj = context.Location.Where(p => p.DeletedBy == null && p.DeletedDate == null).ToList();
            return obj;
        }
        public string GetLastNumberLocation()
        {
            List<int> listNo = context.Location.ToList().Select(o => Convert.ToInt32(o.No)).ToList();

            int lastNumber = listNo.Count > 0 ? listNo.Max() : 0;
            return Convert.ToString(lastNumber+1);

        }

        public List<LocationViewModel> SearchLocation_SP(int? LocationTypeCD, string Floor, int Skip)
        {
            AstonContext dbContext = new AstonContext();

            var result = new List<LocationViewModel>();
            dbContext.Database.OpenConnection();
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "dbo.sp_SearchLocation";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@LocationTypeCD", SqlDbType.Int) { Value = LocationTypeCD });
                cmd.Parameters.Add(new SqlParameter("@Floor", SqlDbType.NVarChar) { Value = Floor });
                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {
                    var locationlist = dbContext.DataReaderMapToList<LocationSearchResult>(reader);
                    foreach (var location in locationlist)
                    {
                        result.Add(new LocationViewModel() { Location = location });
                    }
                    cmd.Connection.Close();
                }
            }
            return result;
        }
        

    }
}
