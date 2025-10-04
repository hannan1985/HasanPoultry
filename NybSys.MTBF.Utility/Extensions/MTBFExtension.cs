using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace NybSys.MTBF.Utility.Extensions
{
    public static class MTBFExtension
    {
        public static System.Data.DataTable ToDataTable(this System.Collections.IList input)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            var obj = input[0];
            System.Reflection.PropertyInfo[] lstProperty = obj.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pro in lstProperty)
            {
                dt.Columns.Add(pro.Name, pro.PropertyType);
            }

            foreach (var objExist in input)
            {
                DataRow row = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pro in lstProperty)
                {
                    row[pro.Name] = pro.GetValue(objExist, null);
                }
                dt.Rows.Add(row);
            }



            return dt;
        }

        public static string GetDescription(this Enum currentEnum)
        {

            string description = string.Empty;
            DescriptionAttribute da = default(DescriptionAttribute);

            FieldInfo fi = currentEnum.GetType().GetField(currentEnum.ToString());
            da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            if (da != null)
            {
                description = da.Description;
            }
            else
            {
                description = currentEnum.ToString();
            }

            return description;

        }

       
    }
}
