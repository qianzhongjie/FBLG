using System;
namespace FBlungu.Model
{
    /// <summary>
    /// f_grouping:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class f_grouping
    {
        public f_grouping()
        { }
        #region Model
        private int _id;
        private string _wxid;
        private string _name;
        private string _count;
        private DateTime? _time;
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
        public string wxid
        {
            set { _wxid = value; }
            get { return _wxid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? time
        {
            set { _time = value; }
            get { return _time; }
        }
        #endregion Model

    }
}


