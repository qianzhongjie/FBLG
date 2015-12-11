using System;
namespace FBlungu.Model
{
    /// <summary>
    /// f_messenger:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class f_messenger
    {
        public f_messenger()
        { }
        #region Model
        private int _id;
        private string _typeid;
        private string _title;
        private string _author;
        private string _content;
        private string _digest;
        private string _state;
        private DateTime? _c_time;
        private DateTime? _s_time;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string typeid
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string digest
        {
            set { _digest = value; }
            get { return _digest; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? c_time
        {
            set { _c_time = value; }
            get { return _c_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? s_time
        {
            set { _s_time = value; }
            get { return _s_time; }
        }
        #endregion Model

    }
}

