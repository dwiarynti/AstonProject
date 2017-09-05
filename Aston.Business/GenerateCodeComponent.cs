using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business
{
    public class GenerateCodeComponent
    {
        public string SubCategoryAsset(int CategoryCD)
        {
            switch (CategoryCD.ToString().Length)
            {
                case 0:
                    return "0000";
                case 1:
                    return "000" + CategoryCD.ToString();
                case 2:
                    return "00" + CategoryCD.ToString();
                case 3:
                    return "0" + CategoryCD.ToString();
                case 4:
                    return CategoryCD.ToString();
                default:
                    return CategoryCD.ToString();
            }
        }

        public string Number(string No)
        {

            switch (No.Length)
            {
                case 0:
                    return "0000";
                case 1:
                    return "000" + No;
                case 2:
                    return "00" + No;
                case 3:
                    return "0" + No;
                case 4:
                    return No;
                default:
                    return No;
            }

        }
        public string SubCategoryLocation(int LocationTypeCD, string Floor)
        {{
                string category;

                switch (LocationTypeCD.ToString().Length)
                {
                    case 1:
                        category = "0" + LocationTypeCD.ToString();
                        break;
                    default:
                        category = LocationTypeCD.ToString();
                        break;
                }

                switch (Floor.ToString().Length)
                {
                    case 1:
                        category = "0" + Floor.ToString();
                        break;
                    default:
                        category = Floor.ToString();
                        break;
                }

                return category;
            } //location category
        }

        public string GenerateCode(string CompanyCode, string ApplicationCode, string MainCategory,string SubCategory, string Number)
        {
            string result;
            result = CompanyCode + ApplicationCode + MainCategory + SubCategory+ Number;
            return result;
        }
    }
}
