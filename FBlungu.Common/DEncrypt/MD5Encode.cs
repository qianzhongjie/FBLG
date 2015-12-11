using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace FBlungu.Common.DEncrypt
{
    /// <summary>
    /// MD5加密字符串
    /// </summary>
    public class MD5Encode
    {
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>       
        public static string GetMd5Str(String ConvertString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            DES des = new DESCryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(ConvertString), 0, ConvertString.Length);
            String returnThis = "";
            for (int i = 0; i < res.Length; i++)
            {
                returnThis += System.Uri.HexEscape((char)res[i]);
            }
            returnThis = returnThis.Replace("%", "");
            returnThis = returnThis.ToLower();
            return returnThis;
        }
    }
}
