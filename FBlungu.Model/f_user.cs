using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBlungu.Model
{
    /// <summary>
    /// f_user:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class f_user
    {
        public f_user()
        { }
        #region Model
        private int _id;
        private string _f_userid;
        private string _f_pwd;
        private DateTime? _f_time;
        private string _f_obj1;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_userid
        {
            set { _f_userid = value; }
            get { return _f_userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_pwd
        {
            set { _f_pwd = value; }
            get { return _f_pwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? F_time
        {
            set { _f_time = value; }
            get { return _f_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_obj1
        {
            set { _f_obj1 = value; }
            get { return _f_obj1; }
        }
        #endregion Model
    }
}