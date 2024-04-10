using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace BITC.Library.Data
{
    public partial class DataProvider : IDisposable
    {
        #region Declaration

        private static DataProvider _provider;

        private const string DEFAULT_CONNECTION_NAME = "DefaultConnection";
        SqlConnection _conn = null;

        #endregion

        #region Constructor

        private DataProvider()
        {

        }

        #endregion

        #region Property

        public SqlConnection Connection
        {
            get
            {
                if (_conn == null)
                {
                    string _connectionName = ConfigurationManager.AppSettings[DEFAULT_CONNECTION_NAME];
                    string _connectionString = string.Empty;

                    if (!string.IsNullOrEmpty(_connectionName))
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;
                    }
                    else
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                    }

                    _conn = new SqlConnection(_connectionString);
                }

                return _conn;
            }
        }

        #endregion

        #region Singleton

        public static DataProvider GetInstance()
        {
            if (_provider == null)
            {
                _provider = new DataProvider();
            }

            return _provider;
        }

        #endregion

        #region Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string storedProcedureName, params object[] parameters)
        {
            if (Connection != null && Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }

            SqlCommand _cmd = _conn.CreateCommand();
            _cmd.CommandText = storedProcedureName;
            _cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(_cmd);

            for (int i = 0; i < _cmd.Parameters.Count; i++)
            {
                for (int j = 0; j < parameters.Count(); j++)
                {
                    if (_cmd.Parameters[i].Direction == System.Data.ParameterDirection.ReturnValue)
                    {
                        break;
                    }

                    _cmd.Parameters[i].Value = parameters[j];
                }
            }

            return _cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string storedProcedureName, params object[] parameters)
        {
            SqlDataReader _reader = null;
            SqlCommand _cmd = null;

            try
            {
                _cmd = CreateCommand(storedProcedureName, parameters);
                _reader = _cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }

            return _reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string storedProcedureName, params object[] parameters)
        {
            int _result = 0;
            SqlCommand _cmd = null;

            try
            {
                _cmd = CreateCommand(storedProcedureName, parameters);

                _result = _cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (Connection != null || Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return _result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string storedProcedureName, params object[] parameters)
        {
            object _result = null;
            SqlCommand _cmd = null;

            try
            {
                _cmd = CreateCommand(storedProcedureName, parameters);
                _result = _cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (Connection != null || Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return _result;
        }

        #endregion

        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Dispose();
            }
        }
    }
}
