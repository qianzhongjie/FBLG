using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.Entities.GroupSendMsg
{
    /// <summary>
    /// 返回通用模型层
    /// </summary>
    public partial class ReplyContentModel
    {

        public class Text
        {
            public string content { get; set; }
            public string url { get; set; }
            public string Stime { get; set; }
        }

        public class Newcontent
        {
            public string title { get; set; }
            public string description { get; set; }
            public string picurl { get; set; }
            public string url { get; set; }
            public string Stime { get; set; }
            public string author { get; set; }
            public string content { get; set; }
            public string share { get; set; }
        }

        public class Image
        {
            public string MediaId { get; set; }
            public string imageUrl { get; set; }
            public string Stime { get; set; }
        }       

        public class Vidoe
        {
            public string title { get; set; }
            public string VUrl { get; set; }
            public string MediaId { get; set; }
            public string description { get; set; }
            public string Stime { get; set; }
        }

        public class Voice
        {
            public string MediaId { get; set; }
            public string MUrl { get; set; }
            public string funcFlag { get; set; }
            public string Stime { get; set; }
            public string description { get; set; }
        }
        public class File
        {
            public string MediaId { get; set; }
            public string Stime { get; set; }
            public string FUrl { get; set; }
        }

        public string types { get; set; }
        public Text text = new Text();
        public List<Newcontent> newcontent { get; set; }
        public Image image = new Image();
        public Vidoe vidoe = new Vidoe();
        public Voice voice = new Voice();
        public File file { get; set; }
    }
}

