using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using FBlungu.IDAL;
using FBlungu.DBUtility;//Please add references
namespace FBlungu.MySQLDAL
{
    /// <summary>
    /// 数据访问类:f_messenger
    /// </summary>
    public partial class f_messenger : If_messenger
    {
        public f_messenger()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "f_messenger");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from f_messenger");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FBlungu.Model.f_messenger model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into f_messenger(");
            strSql.Append("id,typeid,title,author,content,digest,state,c_time,s_time)");
            strSql.Append(" values (");
            strSql.Append("@id,@typeid,@title,@author,@content,@digest,@state,@c_time,@s_time)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11),
					new MySqlParameter("@typeid", MySqlDbType.VarChar,255),
					new MySqlParameter("@title", MySqlDbType.VarChar,255),
					new MySqlParameter("@author", MySqlDbType.VarChar,255),
					new MySqlParameter("@content", MySqlDbType.Text),
					new MySqlParameter("@digest", MySqlDbType.VarChar,255),
					new MySqlParameter("@state", MySqlDbType.VarChar,255),
					new MySqlParameter("@c_time", MySqlDbType.DateTime),
					new MySqlParameter("@s_time", MySqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.typeid;
            parameters[2].Value = model.title;
            parameters[3].Value = model.author;
            parameters[4].Value = model.content;
            parameters[5].Value = model.digest;
            parameters[6].Value = model.state;
            parameters[7].Value = model.c_time;
            parameters[8].Value = model.s_time;

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
        public bool Update(FBlungu.Model.f_messenger model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update f_messenger set ");
            strSql.Append("typeid=@typeid,");
            strSql.Append("title=@title,");
            strSql.Append("author=@author,");
            strSql.Append("content=@content,");
            strSql.Append("digest=@digest,");
            strSql.Append("state=@state,");
            strSql.Append("c_time=@c_time,");
            strSql.Append("s_time=@s_time");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@typeid", MySqlDbType.VarChar,255),
					new MySqlParameter("@title", MySqlDbType.VarChar,255),
					new MySqlParameter("@author", MySqlDbType.VarChar,255),
					new MySqlParameter("@content", MySqlDbType.Text),
					new MySqlParameter("@digest", MySqlDbType.VarChar,255),
					new MySqlParameter("@state", MySqlDbType.VarChar,255),
					new MySqlParameter("@c_time", MySqlDbType.DateTime),
					new MySqlParameter("@s_time", MySqlDbType.DateTime),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
            parameters[0].Value = model.typeid;
            parameters[1].Value = model.title;
            parameters[2].Value = model.author;
            parameters[3].Value = model.content;
            parameters[4].Value = model.digest;
            parameters[5].Value = model.state;
            parameters[6].Value = model.c_time;
            parameters[7].Value = model.s_time;
            parameters[8].Value = model.id;

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
            strSql.Append("delete from f_messenger ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
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
            strSql.Append("delete from f_messenger ");
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
        public FBlungu.Model.f_messenger GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,typeid,title,author,content,digest,state,c_time,s_time from f_messenger ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            FBlungu.Model.f_messenger model = new FBlungu.Model.f_messenger();
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
        public FBlungu.Model.f_messenger DataRowToModel(DataRow row)
        {
            FBlungu.Model.f_messenger model = new FBlungu.Model.f_messenger();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["typeid"] != null)
                {
                    model.typeid = row["typeid"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["author"] != null)
                {
                    model.author = row["author"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["digest"] != null)
                {
                    model.digest = row["digest"].ToString();
                }
                if (row["state"] != null)
                {
                    model.state = row["state"].ToString();
                }
                if (row["c_time"] != null && row["c_time"].ToString() != "")
                {
                    model.c_time = DateTime.Parse(row["c_time"].ToString());
                }
                if (row["s_time"] != null && row["s_time"].ToString() != "")
                {
                    model.s_time = DateTime.Parse(row["s_time"].ToString());
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
            strSql.Append("select id,typeid,title,author,content,digest,state,c_time,s_time ");
            strSql.Append(" FROM f_messenger ");
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
            strSql.Append("select count(1) FROM f_messenger ");
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
            strSql.Append(")AS Row, T.*  from f_messenger T ");
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
            parameters[0].Value = "f_messenger";
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

