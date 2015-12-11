using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace FBlungu.Common
{
    public class ObjectToJson
    {
        /// <summary>
        /// 将实体对象转换成json对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJsonString<T>(T t)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer json = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, t);
                    return System.Text.Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 将json对象转换成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonobj"></param>
        /// <returns></returns>
        public static T JsonStringToObject<T>(string jsonobj)
        {
            T t = default(T);
            try
            {
                JavaScriptSerializer convertModel = new JavaScriptSerializer();
                t = convertModel.Deserialize<T>(jsonobj);
            }
            catch { }
            return t;
        }
    }
}
