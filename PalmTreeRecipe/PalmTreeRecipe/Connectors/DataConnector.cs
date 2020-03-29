using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Connectors
{
    public class DataConnector
    {

        public string DB_URL = PalmTreeRecipe.Properties.Resources.DatabaseConnection;

        public T getValueOrDefault<T>(int column, SqlDataReader reader)
        {
            if(!reader.IsDBNull(column))
            {
                object value = reader.GetValue(column);
                return (T)value;
            }
            return default;
        }

        public object PassValueOrDBNull(object value)
        {
            if(value == null)
            {
                return DBNull.Value;
            } else
            {
                return value;
            }
        }


    }
}
