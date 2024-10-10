using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    //CRUD
    public class Helper
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        public static int ExecuteNonQuery(string sqlstr, CommandType cmdType, SqlParameter[] paramaters = null)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            cmd.CommandType = cmdType;
            if (cmdType == CommandType.StoredProcedure)
            {
                if (paramaters != null)
                {
                    cmd.Parameters.AddRange(paramaters);
                }
            }
            //number of rows affected by the query
            int numRows = 0;
            try
            {
                con.Open();
                numRows = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;

            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return numRows;
        }

        public static SqlDataReader ExecuteReader(string sqlstr, CommandType cmdType, SqlParameter[] paramaters = null)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            cmd.CommandType = cmdType;
            if (cmdType == CommandType.StoredProcedure)
            {
                if (paramaters != null)
                {
                    cmd.Parameters.AddRange(paramaters);
                }
            }
            SqlDataReader reader = null;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception Ex)
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                    throw Ex;
                }

            }
            return reader;
        }

        public static object ExecuteScalar(string sqlstr, CommandType cmdType, SqlParameter[] paramaters = null)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            cmd.CommandType = cmdType;
            if (cmdType == CommandType.StoredProcedure)
            {
                if (paramaters != null)
                {
                    cmd.Parameters.AddRange(paramaters);
                }
            }
            object result = null;
            try
            {
                con.Open();
                result = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }
            return result;  
        }
    }
}
