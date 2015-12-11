using System;
using System.Diagnostics;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace FBlungu.Common
{
    /// <summary>
    /// 操作csv导出数据报表的类
    /// Copyright (C) Maticsoft
    /// </summary>
    public class DataToExcel
    {
        public string OutPutCvs(System.Data.DataTable dt, string rowtitle, string FilePath, string showTitle = "")
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".csv";
            ClearFile(FilePath);
            using (StreamWriter streamWriter = new StreamWriter(FilePath + @"\" + filename, false, Encoding.Default))
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder showtitle = new StringBuilder();
                if (!string.IsNullOrEmpty(showTitle))
                {
                    sb.Append(" " + showTitle + " ");
                }
                foreach (var title in rowtitle.Split(','))
                {
                    if (!string.IsNullOrEmpty(showTitle))
                    {
                        sb.Append(","); //没一个字段后面都加逗号，表示是一列，因为这是第一行    因此也是列标题
                    }
                    showtitle.Append(" " + title + " ").Append(","); //没一个字段后面都加逗号，表示是一列，因为这是第一行    因此也是列标题
                }
                streamWriter.WriteLine(sb.ToString());
                streamWriter.WriteLine(showtitle.ToString());

                foreach (DataRow row in dt.Rows)
                {
                    sb = new StringBuilder();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == System.Type.GetType("System.DateTime"))
                        {
                            object str = (object)(Convert.ToDateTime(row[col.ColumnName].ToString())).ToString();
                            sb.Append(ReplaceSpecialChars(str + "")).Append(",");
                        }
                        else
                        {
                            sb.Append(ReplaceSpecialChars(row[col.ColumnName] + "")).Append(",");
                        }

                    }
                    streamWriter.WriteLine(sb.ToString());
                }
            }
            return filename;
        }
        public static string ReplaceSpecialChars(string input)
        {
            // space -> _x0020_   特殊字符的替换
            // % -> _x0025_
            // # -> _x0023_
            // & -> _x0026_
            // / -> _x002F_
            if (input == null) return "";
            input = input.Replace(",", "-");
            return input;
        }

        #region  清理过时的Excel文件

        private void ClearFile(string FilePath)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            String[] Files = System.IO.Directory.GetFiles(FilePath);
            if (Files.Length > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        System.IO.File.Delete(Files[i]);
                    }
                    catch
                    {
                    }

                }
            }
        }
        #endregion

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="hfc">客户端上传文件流</param>
        /// <param name="AfterPath">类似：aaaa/bbb/子文件夹</param>
        /// <returns></returns>
        public static List<string> Upload(HttpFileCollectionBase hfc, string afterPath = "")
        {
            string path = "~/" + afterPath;

            List<string> theList = new List<string>();
            string filePath = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (string item in hfc)
            {
                if (hfc[item].ContentLength == 0) continue;
                try
                {
                    string fileExtension = Path.GetExtension(hfc[item].FileName);
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    hfc[item].SaveAs(filePath + fileName);

                    string strSqlPath = "~/UploadFile/Pic/" + fileName;
                    theList.Add(strSqlPath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
            }
            return theList;
        }

    }
}