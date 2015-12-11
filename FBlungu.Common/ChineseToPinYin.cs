using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.International.Converters.PinYinConverter;


namespace FBlungu.Common
{
    public partial class ChineseToPinYin
    {
        /// <summary>
        /// 拼音转换，主要提供数据库插入拼音转换
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string ToPinYins(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                StringBuilder hzresult = new StringBuilder();//使用StringBuilder优化字符串连接              
                foreach (char cobj in txt)
                {
                    if (cobj >= 0x4e00 && cobj <= 0x9fbb)
                    {
                        try
                        {
                            ChineseChar chineseChar = new ChineseChar(cobj);
                            if (hzresult.Length <= 0)
                            {
                                var cpy = chineseChar.Pinyins[0].Substring(0, chineseChar.Pinyins[0].Length - 1).ToLower().ToCharArray();
                                for (int i = 0; i < cpy.Count(); i++)
                                {
                                    for (int j = 0; j <= i; j++)
                                    {
                                        hzresult.Append(cpy[j]);
                                    }
                                    hzresult.Append(" ");
                                }
                            }
                            else
                            {
                                hzresult.Append(chineseChar.Pinyins[0].Substring(0, chineseChar.Pinyins[0].Length - 1).ToLower() + " ");
                            }
                        }
                        catch (Exception ex)
                        {
                            hzresult.Append(cobj + " ");
                        }
                    }
                    else
                    {
                        hzresult.Append(cobj);
                    }
                }
                if (hzresult.ToString().IndexOf(' ') < 0)
                {
                    var pingying = hzresult.ToString().ToCharArray();
                    hzresult = new StringBuilder();
                    for (int i = 0; i < pingying.Count(); i++)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            hzresult.Append(pingying[j]);
                        }
                        hzresult.Append(" ");
                    }
                }
                return hzresult.ToString().Substring(0, hzresult.Length - 1);
            }
            return "";
        }
        /// <summary>
        /// 拼音转换，主要提供数据库查询拼音转换
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string ToPinYin(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                StringBuilder hzresult = new StringBuilder();//使用StringBuilder优化字符串连接              
                foreach (char cobj in txt)
                {
                    if (cobj >= 0x4e00 && cobj <= 0x9fbb)
                    {
                        ChineseChar chineseChar = new ChineseChar(cobj);
                        hzresult.Append(chineseChar.Pinyins[0].Substring(0, chineseChar.Pinyins[0].Length - 1).ToLower() + " ");
                    }
                    else
                    {
                        hzresult.Append(cobj);
                    }
                }
                if (hzresult.ToString().LastIndexOf(' ') == hzresult.Length)
                {
                    return hzresult.ToString().Substring(0, hzresult.Length - 1);
                }
                return hzresult.ToString().ToLower();
            }
            return "";
        }
    }
}
