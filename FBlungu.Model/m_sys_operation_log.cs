using System;
namespace FBlungu.Model
{
    /// <summary>
    /// m_sys_operation_log:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class m_sys_operation_log
    {
        public m_sys_operation_log()
        { }
        #region Model
        private int _id;
        private string _error_code;
        private string _error_desc;
        private string _error_source;
        private DateTime? _error_time;
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
        public string error_code
        {
            set { _error_code = value; }
            get { return _error_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string error_desc
        {
            set { _error_desc = value; }
            get { return _error_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string error_source
        {
            set { _error_source = value; }
            get { return _error_source; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? error_time
        {
            set { _error_time = value; }
            get { return _error_time; }
        }
        #endregion Model

    }
}


