using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Aston.Business.Data
{
    public class UserExtensions
    {

        public List<UserPaginationViewModel> GetUser_Pagination(int Skip)
        {
            var result = new List<UserPaginationViewModel>();
            var obj = new UserPaginationViewModel();

            using (AstonContext dbContext = new AstonContext())
            {



                dbContext.Database.OpenConnection();
                DbCommand cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "dbo.sp_UserPagination";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Int) { Value = Skip });

                using (var reader = cmd.ExecuteReader())
                {
                    var Userlist = dbContext.DataReaderMapToList<UserPaginationViewModel>(reader);
                    foreach (var User in Userlist)
                    {
                        result.Add(User);
                    }
                    cmd.Connection.Close();
                }
            }
            return result;
        }
    }
}
