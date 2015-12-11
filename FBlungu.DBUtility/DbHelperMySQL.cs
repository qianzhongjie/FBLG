using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using FBlungu.DBUtility;
using System.Net.Sockets;
namespace FBlungu.DBUtility
{
    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) Maticsoft
    /// </summary>
    public abstract class DbHelperMySQL
    {
        private static Stack<MySqlConnection> pool;
        private static int POOL_MAX_SIZE = int.Parse(ConfigurationManager.AppSettings["poolmax"]);

        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.	poolmax	
        public static string connectionString = PubConstant.ConnectionString;
        public DbHelperMySQL()
        {
        }


        /// <summary>
        /// 初始化线程池
        /// </summary>
        public static void InitPool()
        {
            if (pool == null)
            {
                pool = new Stack<MySqlConnection>();
                for (int i = 0; i <= POOL_MAX_SIZE; i++)
                {
                    pool.Push(new MySqlConnection(connectionString));
                }
            }
        }
        /// <summary>
        /// 持久连接
        /// </summary>
        public static MySqlConnection Connection
        {
            get
            {
                MySqlConnection _connection = null;
                object o = new object();
                lock (o)
                {
                    try
                    {
                        _connection = pool.Pop();
                        while (_connection == null)
                        {
                            _connection = pool.Pop();
                        }
                        switch (_connection.State)
                        {
                            case ConnectionState.Closed:
                                _connection.Open();
                                break;
                            case ConnectionState.Broken:
                                _connection = new MySqlConnection(connectionString);
                                _connection.Open();
                                break;
                        }
                    }
                    catch
                    {
                    }
                }
                return _connection;

            }
        }

        public static void CloseConnection(MySqlConnection _connection)
        {
            if (_connection != null)
            {
                try
                {
                    _connection.Close();
                }
                catch
                {
                    _connection = new MySqlConnection(connectionString);
                    pool.Push(_connection);
                }
            }
        }
        #region 公用方法
        /// <summary>
        /// 得到最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 是否存在（基于MySqlParameter）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            MySqlConnection conn = Connection;
            using (MySqlCommand cmd = new MySqlCommand(SQLString, conn))
            {
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    pool.Push(conn);
                    return rows;
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    cmd.Connection = conn;
                    int rows = cmd.ExecuteNonQuery();
                    pool.Push(conn);
                    return rows;
                }
            }
        }

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            MySqlConnection conn = Connection;
            using (MySqlCommand cmd = new MySqlCommand(SQLString, conn))
            {
                try
                {
                    cmd.CommandTimeout = Times;
                    int rows = cmd.ExecuteNonQuery();
                    pool.Push(conn);
                    return rows;
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = Times;
                    int rows = cmd.ExecuteNonQuery();
                    pool.Push(conn);
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            {
                MySqlConnection conn = Connection;
                MySqlCommand cmd = new MySqlCommand(SQLString, conn);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    pool.Push(conn);
                    return rows;
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    cmd.Connection = conn;
                    int rows = cmd.ExecuteNonQuery();
                    pool.Push(conn);
                    return rows;
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            {
                MySqlConnection conn = Connection;
                MySqlCommand cmd = new MySqlCommand(SQLString, conn);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    object obj = cmd.ExecuteScalar();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    cmd.Connection = conn;
                    object obj = cmd.ExecuteScalar();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }

                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {

            MySqlConnection conn = Connection;
            using (MySqlCommand cmd = new MySqlCommand(SQLString, conn))
            {
                try
                {
                    object obj = cmd.ExecuteScalar();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    MySqlCommand ecmd = new MySqlCommand(SQLString, conn);
                    object obj = cmd.ExecuteScalar();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }
        public static object GetSingle(string SQLString, int Times)
        {

            MySqlConnection conn = Connection;
            using (MySqlCommand cmd = new MySqlCommand(SQLString, conn))
            {
                try
                {
                    cmd.CommandTimeout = Times;
                    object obj = cmd.ExecuteScalar();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    MySqlCommand ecmd = new MySqlCommand(SQLString, conn);
                    object obj = cmd.ExecuteScalar();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string strSQL)
        {
            MySqlConnection conn = Connection;
            try
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                pool.Push(conn);
                return myReader;
            }
            catch
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                pool.Push(conn);
                return myReader;
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = Connection;
            try
            {
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, conn);
                command.Fill(ds, "ds");
                // pool.Push(conn);
            }
            catch
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, conn);
                command.Fill(ds, "ds");
                // pool.Push(conn);
            }
            return ds;
        }
        public static DataSet Query(string SQLString, int Times)
        {
            DataSet ds = new DataSet();
            MySqlConnection conn = Connection;
            try
            {
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, conn);
                command.SelectCommand.CommandTimeout = Times;
                command.Fill(ds, "ds");
                pool.Push(conn);
            }
            catch
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, conn);
                command.SelectCommand.CommandTimeout = Times;
                command.Fill(ds, "ds");
                pool.Push(conn);
            }
            return ds;
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                MySqlConnection conn = Connection;
                try
                {
                    PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    pool.Push(conn);
                    return rows;
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    ; conn.Open();
                    PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    pool.Push(conn);
                    return rows;
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value)
                                {
                                    isHave = false;
                                }
                                isHave = Convert.ToInt32(obj) > 0;

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                continue;
                            }
                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (CommandInfo myDE in SQLStringList)
                        {
                            string cmdText = myDE.CommandText;
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Parameters;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                MySqlConnection conn = Connection;
                try
                {
                    PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    pool.Push(conn);
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = Connection;
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                pool.Push(conn);
                return myReader;
            }
            catch
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                pool.Push(conn);
                return myReader;
            }
            finally
            {
                cmd.Dispose();
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                MySqlConnection conn = Connection;
                try
                {
                    PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    pool.Push(conn);
                }
                catch
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    pool.Push(conn);
                }
            }
            return ds;
        }


        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                if (trans != null)
                    cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;//cmdType;
                if (cmdParms != null)
                {
                    foreach (MySqlParameter parameter in cmdParms)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parameter);
                    }
                }
            }
            catch (Exception socketex)
            { }
        }

        #endregion



    }

}
