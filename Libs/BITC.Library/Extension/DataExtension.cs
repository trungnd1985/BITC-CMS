using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Library.Extension
{
    public static class DataExtension
    {
        public static IList<T> ToCollection<T>(this SqlDataReader reader)
        {
            var lst = new List<T>();

            while (reader.Read())
            {

            }

            reader.Close();

            return lst;
        }
    }
}
