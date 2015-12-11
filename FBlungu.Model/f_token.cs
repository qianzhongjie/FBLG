using System;
namespace FBlungu.Model
{
    /// <summary>
    /// f_token:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class f_token
    {
        public f_token()
        { }
        #region Model
        private int _id;
        private string _token;
        private DateTime? _token_time;
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
        public string token
        {
            set { _token = value; }
            get { return _token; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? token_time
        {
            set { _token_time = value; }
            get { return _token_time; }
        }
        #endregion Model

    }
}

