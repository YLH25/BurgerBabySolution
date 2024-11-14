using Microsoft.Data.SqlClient;
using System.Data;

namespace BurgerBaby.Models.Infa
{
    public class SqlDbHelper
    {
        private string connString;
        public SqlDbHelper(string keyOfConnString)
        {
            connString = keyOfConnString;
        }
        public void ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
        public DataTable? Select(string sql, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connString))
            {
                var command = new SqlCommand(sql, conn);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "result");
                return dataSet.Tables["result"];
            }
        }

    }
    public class SqlParameterBuilder
    {
        private List<SqlParameter> parameters = new List<SqlParameter>();
        public SqlParameterBuilder AddNVarchar(string name, int length, string value)
        {
            var param = new SqlParameter
                        (name, SqlDbType.NVarChar, length)
            { Value = value };
            parameters.Add(param);

            return this;
        }
        public SqlParameterBuilder AddInt(string name, int value)
        {
            var param = new SqlParameter
                        (name, SqlDbType.Int)
            { Value = value };
            parameters.Add(param);

            return this;
        }

        public SqlParameterBuilder AddDecimal(string name, decimal value)
        {
            var param = new SqlParameter
                        (name, SqlDbType.Decimal)
            { Value = value };
            parameters.Add(param);

            return this;
        }
        public SqlParameter[] Build()
        {
            return parameters.ToArray();
        }

    }
}
