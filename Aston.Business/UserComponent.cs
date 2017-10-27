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
    public class UserComponent
    {
        AstonContext _context = new AstonContext();
        UserExtensions _user = new UserExtensions();
        public List<UserPaginationViewModel> GetUserPagintion (int Skip)
        {
            return _user.GetUser_Pagination(Skip);
        }

        //public RegisterUserViewModel CreateUser(RegisterUserViewModel obj)
        //{
        //    _
        //}

    }
}
