using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.IO;
//using System.Web.Security;

namespace QYSubjectWeixin
{
    public class CheckSignature
    {
        /// <summary>
        /// 在网站没有提供Token（或传入为null）的情况下的默认Token，建议在网站中进行配置。
        /// </summary>        
        //构造函数
        // @param Token: 公众平台上，开发者设置的Token
        // @param EncodingAESKey: 公众平台上，开发者设置的EncodingAESKey
        // @param CorpID: 企业号的CorpID     
        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echoSt">随机串，对应URL参数的echostr</param>
        /// <param name="sReplyEchoStr">加密的随机字符串</param>
        /// <param name="token">公众平台上，开发者设置的Token</param>
        /// <param name="encodingAESKey">公众平台上，开发者设置的EncodingAESKey</param>
        /// <param name="corpID">企业号的CorpID</param>
        /// <returns></returns>
        public static bool Check(string signature, string timestamp, string nonce, string echoSt, ref string sReplyEchoStr, string token = null, string encodingAESKey = null, string corpID = null)
        {
            return signature == GetSignature(timestamp, nonce, echoSt, ref sReplyEchoStr, token, encodingAESKey, corpID);
        }

        /// <summary>
        /// 签名认证
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <param name="encodingAESKey"></param>
        /// <param name="corpID"></param>
        /// <returns></returns>
        public static string GetSignature(string timestamp, string nonce, string echoSt, ref string sReplyEchoStr, string token = null, string encodingAESKey = null, string corpID = null)
        {

            ArrayList AL = new ArrayList();
            AL.Add(token);
            AL.Add(timestamp);
            AL.Add(nonce);
            AL.Add(echoSt);
            AL.Sort(new DictionarySort());
            string raw = "";
            for (int i = 0; i < AL.Count; ++i)
            {
                raw += AL[i];
            }

            var arr = new[] { token, timestamp, nonce, echoSt }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            //var enText = FormsAuthentication.HashPasswordForStoringInConfigFile(arrString, "SHA1");//使用System.Web.Security程序集
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(raw));
            //StringBuilder enText = new StringBuilder();
            //foreach (var b in sha1Arr)
            //{
            //    enText.AppendFormat("{0:x2}", b);
            //}
            //if (encodingAESKey.Length != 43)
            //{
            //    return null;
            //}
            //ret = GenarateSinature(token, timestamp, nonce, echoSt);      
            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(raw);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return null;
            }
            sReplyEchoStr = "";
            string cpid = "";
            try
            {
                sReplyEchoStr = Cryptography.AES_decrypt(echoSt, encodingAESKey, ref cpid); //m_sCorpID);
            }
            catch (Exception)
            {
                sReplyEchoStr = "";
            }
            if (cpid != corpID)
            {
                sReplyEchoStr = "";
            }
            return hash;
        }
        /// <summary>
        /// 加密返回
        /// </summary>
        /// <param name="sReplyMsg"></param>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="token"></param>
        /// <param name="encodingAESKey"></param>
        /// <param name="corpID"></param>
        /// <returns></returns>
        public static string GetJiaMi(string sReplyMsg, string sTimeStamp, string token = null, string encodingAESKey = null, string corpID = null)
        {
            string raw = "";
            try
            {
                raw = Cryptography.AES_encrypt(sReplyMsg, encodingAESKey, corpID);
            }
            catch (Exception)
            {
                return null;
            }
            string sNonce = DateTime.Now.Ticks.ToString();
            string MsgSigature = "";
            MsgSigature = GetSignature(sTimeStamp, sNonce, raw, ref MsgSigature, token, encodingAESKey, corpID);
            string sEncryptMsg = "";

            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";
            sEncryptMsg = sEncryptMsg + "<xml>" + EncryptLabelHead + raw + EncryptLabelTail;
            sEncryptMsg = sEncryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
            sEncryptMsg = sEncryptMsg + TimeStampLabelHead + sTimeStamp + TimeStampLabelTail;
            sEncryptMsg = sEncryptMsg + NonceLabelHead + sNonce + NonceLabelTail;
            sEncryptMsg += "</xml>";

            return sEncryptMsg;
        }

        public class DictionarySort : System.Collections.IComparer
        {
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }
    }
}
