using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using FBlungu.IDAL;
using FBlungu.DBUtility;
using FBlungu.DBUtility;//Please add references
namespace FBlungu.DAL
{
    /// <summary>
    /// 数据访问类:m_sys_operation_log
    /// </summary>
    public partial class m_sys_operation_log : Im_sys_operation_log
    {
        public m_sys_operation_log()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from m_sys_operation_log");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FBlungu.Model.m_sys_operation_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into m_sys_operation_log(");
            strSql.Append("error_code,error_desc,error_source,error_time)");
            strSql.Append(" values (");
            strSql.Append("@error_code,@error_desc,@error_source,@error_time)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@error_code", MySqlDbType.VarChar,255),
					new MySqlParameter("@error_desc", MySqlDbType.VarChar,255),
					new MySqlParameter("@error_source", MySqlDbType.VarChar,255),
					new MySqlParameter("@error_time", MySqlDbType.DateTime)};
            parameters[0].Value = model.error_code;
            parameters[1].Value = model.error_desc;
            parameters[2].Value = model.error_source;
            parameters[3].Value = model.error_time;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FBlungu.Model.m_sys_operation_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update m_sys_operation_log set ");
            strSql.Append("error_code=@error_code,");
            strSql.Append("error_desc=@error_desc,");
            strSql.Append("error_source=@error_source,");
            strSql.Append("error_time=@error_time");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@error_code", MySqlDbType.VarChar,255),
					new MySqlParameter("@error_desc", MySqlDbType.VarChar,255),
					new MySqlParameter("@error_source", MySqlDbType.VarChar,255),
					new MySqlParameter("@error_time", MySqlDbType.DateTime),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = model.error_code;
            parameters[1].Value = model.error_desc;
            parameters[2].Value = model.error_source;
            parameters[3].Value = model.error_time;
            parameters[4].Value = model.id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from m_sys_operation_log ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from m_sys_operation_log ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FBlungu.Model.m_sys_operation_log GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,error_code,error_desc,error_source,error_time from m_sys_operation_log ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
            parameters[0].Value = id;

            FBlungu.Model.m_sys_operation_log model = new FBlungu.Model.m_sys_operation_log();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FBlungu.Model.m_sys_operation_log DataRowToModel(DataRow row)
        {
            FBlungu.Model.m_sys_operation_log model = new FBlungu.Model.m_sys_operation_log();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["error_code"] != null)
                {
                    model.error_code = row["error_code"].ToString();
                }
                if (row["error_desc"] != null)
                {
                    model.error_desc = row["error_desc"].ToString();
                }
                if (row["error_source"] != null)
                {
                    model.error_source = row["error_source"].ToString();
                }
                if (row["error_time"] != null && row["error_time"].ToString() != "")
                {
                    model.error_time = DateTime.Parse(row["error_time"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,error_code,error_desc,error_source,error_time ");
            strSql.Append(" FROM m_sys_operation_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM m_sys_operation_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from m_sys_operation_log T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "m_sys_operation_log";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

