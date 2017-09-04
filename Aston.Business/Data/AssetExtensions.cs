﻿using Aston.Entities;
using Aston.Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business.Data
{
    public class AssetExtensions
    {
        AstonContext context = new AstonContext();
        public Asset GetAssetInfoByID(int id)
        {
            var obj = context.Asset.Where(p => p.ID == id).FirstOrDefault();
            return obj;
        }
        public Asset GetAssetInfoByCode (string code)
        {
            var obj = context.Asset.Where(p => p.Code == code).FirstOrDefault();
            return obj;
        }
        public List<Asset> GetAsset()
        {
            var obj = context.Asset.Where(p => p.DeletedBy == null && p.DeletedDate == null).ToList();
            return obj;
        }
    }
}
