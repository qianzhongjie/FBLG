using System;
using System.Reflection;
using System.Configuration;
namespace FBlungu.DALFactory
{
    /// <summary>
    /// 抽象工厂模式创建DAL。
    /// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
    /// DataCache类在导出代码的文件夹里
    /// <appSettings> 
    /// <add key="DAL" value="FBlungu.MySQLDAL" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DataAccess//<t>
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string AssemblyPath, string ClassNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
                return objType;
            }
            catch
            {

            }

            return null;
        }
        /// <summary>
        /// 创建数据层接口
        /// </summary>
        //public static t Create(string ClassName)
        //{
        //string ClassNamespace = AssemblyPath +"."+ ClassName;
        //object objType = CreateObject(AssemblyPath, ClassNamespace);
        //return (t)objType;
        //}
        /// <summary>
        /// 创建f_user数据层接口。
        /// </summary>
        public static IDAL.If_user Createf_user()
        {

            string ClassNamespace = AssemblyPath + ".f_user";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (IDAL.If_user)objType;
        }
        /// <summary>
        /// 创建m_sys_operation_log数据层接口。
        /// </summary>
        public static FBlungu.IDAL.Im_sys_operation_log Createm_sys_operation_log()
        {

            string ClassNamespace = AssemblyPath + ".m_sys_operation_log";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBlungu.IDAL.Im_sys_operation_log)objType;
        }

        /// <summary>
        /// 创建f_grouping数据层接口。
        /// </summary>
        public static FBlungu.IDAL.If_grouping Createf_grouping()
        {

            string ClassNamespace = AssemblyPath + ".f_grouping";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBlungu.IDAL.If_grouping)objType;
        }
        /// <summary>
        /// 创建f_messenger数据层接口。
        /// </summary>
        public static FBlungu.IDAL.If_messenger Createf_messenger()
        {

            string ClassNamespace = AssemblyPath + ".f_messenger";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBlungu.IDAL.If_messenger)objType;
        }
        /// <summary>
        /// 创建f_token数据层接口。
        /// </summary>
        public static FBlungu.IDAL.If_token Createf_token()
        {

            string ClassNamespace = AssemblyPath + ".f_token";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBlungu.IDAL.If_token)objType;
        }
        /// <summary>
        /// 创建UserInfo数据层接口。
        /// </summary>
        public static FBlungu.IDAL.IUserInfo CreateUserInfo()
        {

            string ClassNamespace = AssemblyPath + ".UserInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (FBlungu.IDAL.IUserInfo)objType;
        }
    }
}

