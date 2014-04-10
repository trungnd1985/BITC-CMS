using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Library.Data
{
    public partial class DataProvider : IQueryProvider
    {
        #region Declaration

        private const string DEFAULT_CONNECTION_NAME = "DefaultConnection";
        SqlConnection _conn = null;

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

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            return new DataContext<TElement>(this, expression);
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)DataContext.Execute(expression, IsEnumerable);
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            return DataProvider(expression, false);
        }

    }
}
