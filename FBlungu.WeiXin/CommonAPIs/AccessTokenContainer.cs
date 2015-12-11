using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.CommonAPIs
{
    class AccessTokenBag
    {
        public string ManagerUser { get; set; }
        public string Corpid { get; set; }
        public string Secret { get; set; }
        public DateTime ExpireTime { get; set; }
        public AccessTokenResult AccessTokenResult { get; set; }
        public JSAccessTokenResult JSAccessTokenResult { get; set; }
        public DateTime JSExpireTime { get; set; }
    }


    /// <summary>
    /// 通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    public class AccessTokenContainer
    {
        static QYCustomerSubject.BLL.c_access_token access_token = new QYCustomerSubject.BLL.c_access_token();
        static QYCustomerSubject.BLL.c_interconfig bll_interconfig = new QYCustomerSubject.BLL.c_interconfig();
        static Dictionary<string, AccessTokenBag> AccessTokenCollection =
           new Dictionary<string, AccessTokenBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public static void Register(string manageruse, string corpid, string secret)
        {
            AccessTokenCollection[manageruse] = new AccessTokenBag()
            {
                ManagerUser = manageruse,
                Corpid = corpid,
                Secret = secret,
                ExpireTime = DateTime.MinValue,
                AccessTokenResult = new AccessTokenResult(),
                JSAccessTokenResult = new JSAccessTokenResult(),
                JSExpireTime = DateTime.MinValue
            };
        }

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        [Obsolete("该方法已经过时，不建议在次使用，请调用GetToken方法，传递企业信息id", false)]
        public static string TryGetToken(string manageruse, string corpid, string secret, bool getNewToken = false)
        {
            if (!CheckRegistered(manageruse) || getNewToken)
            {
                Register(manageruse, corpid, secret);
            }
            return GetToken(manageruse);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string manageruse, bool getNewToken = false)
        {
            return GetTokenResult(manageruse, getNewToken).access_token;
        }
        /// <summary>
        /// 获取jsToken
        /// </summary>
        /// <param name="manageruse"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string GetJsToken(string manageruse, bool getNewToken = false)
        {
            if (!AccessTokenCollection.ContainsKey(manageruse))
            {
                var interconfig = bll_interconfig.GetModelList("E_entid=@entid", 0, new MySql.Data.MySqlClient.MySqlParameter("@entid", manageruse));
                if (interconfig.Count > 0)
                {
                    Register(manageruse, interconfig[0].C_icorpid, interconfig[0].C_icorpsecret);
                    var model_access = access_token.GetModelList("E_entid=@entid and a_type=2", 0, new MySql.Data.MySqlClient.MySqlParameter("@entid", manageruse));
                    var accessTokenBags = AccessTokenCollection[manageruse];
                    if (model_access.Count > 0)
                    {
                        if ((DateTime.Now - model_access[0].a_endtime.Value).TotalHours < 2)
                        {
                            accessTokenBags.JSAccessTokenResult.ticket= model_access[0].a_access_token;
                            accessTokenBags.JSExpireTime = model_access[0].a_endtime.Value;
                        }
                        else
                        {
                            accessTokenBags.JSAccessTokenResult = CommonApi.GetJsToken(GetToken(manageruse));
                            accessTokenBags.JSExpireTime = DateTime.Now.AddSeconds(7200);
                            model_access[0].a_access_token = accessTokenBags.JSAccessTokenResult.ticket;
                            model_access[0].a_endtime = DateTime.Now;
                            access_token.Update(model_access[0]);
                        }
                        return accessTokenBags.JSAccessTokenResult.ticket;
                    }
                }
                else
                {
                    return null;
                }
            }

            var accessTokenBag = AccessTokenCollection[manageruse];
            if (getNewToken || accessTokenBag.JSExpireTime <= DateTime.Now)
            {
                //已过期，重新获取
                accessTokenBag.JSAccessTokenResult = CommonApi.GetJsToken(GetToken(manageruse));
                accessTokenBag.JSExpireTime = DateTime.Now.AddSeconds(7200);
                access_token.Add(new QYCustomerSubject.Model.c_access_token()
                {
                    E_entid = int.Parse(manageruse),
                    a_access_token = accessTokenBag.JSAccessTokenResult.ticket,
                    a_endtime = DateTime.Now,
                    a_type = 2
                });
            }
            return accessTokenBag.JSAccessTokenResult.ticket;
        }
        /// <summary>
        /// 企业id
        /// </summary>
        /// <param name="manageruse"></param>
        /// <returns></returns>
        public static string GetCorpid(string manageruse)
        {
            return AccessTokenCollection[manageruse].Corpid;
        }
        public static string GetEntid(string corpid)
        {
            return AccessTokenCollection[corpid].ManagerUser;
        } 
        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetTokenResult(string manageruse, bool getNewToken = false)
        {
            if (!AccessTokenCollection.ContainsKey(manageruse))
            {
                var interconfig = bll_interconfig.GetModelList("E_entid=@entid", 0, new MySql.Data.MySqlClient.MySqlParameter("@entid", manageruse));
                if (interconfig.Count > 0)
                {
                    Register(manageruse, interconfig[0].C_icorpid, interconfig[0].C_icorpsecret);
                    var model_access = access_token.GetModelList("E_entid=@entid and a_type=1", 0, new MySql.Data.MySqlClient.MySqlParameter("@entid", manageruse));
                    var accessTokenBags = AccessTokenCollection[manageruse];
                    if (model_access.Count > 0)
                    {
                        if ((DateTime.Now - model_access[0].a_endtime.Value).TotalHours < 2)
                        {
                            accessTokenBags.AccessTokenResult.access_token = model_access[0].a_access_token;
                            accessTokenBags.ExpireTime = model_access[0].a_endtime.Value.AddSeconds(7200);
                        }
                        else
                        {
                            accessTokenBags.AccessTokenResult = CommonApi.GetToken(accessTokenBags.Corpid, accessTokenBags.Secret);
                            accessTokenBags.ExpireTime = DateTime.Now.AddSeconds(7200);
                            model_access[0].a_access_token = accessTokenBags.AccessTokenResult.access_token;
                            model_access[0].a_endtime = DateTime.Now;
                            access_token.Update(model_access[0]);
                        }
                        return accessTokenBags.AccessTokenResult;
                    }
                }
                else
                {
                    return null;
                }
            }

            var accessTokenBag = AccessTokenCollection[manageruse];
            if (getNewToken || accessTokenBag.ExpireTime <= DateTime.Now)
            {
                //已过期，重新获取
                accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.Corpid, accessTokenBag.Secret);
                accessTokenBag.ExpireTime = DateTime.Now.AddSeconds(7200);
                access_token.Add(new QYCustomerSubject.Model.c_access_token()
                {
                    E_entid = int.Parse(manageruse),
                    a_access_token = accessTokenBag.AccessTokenResult.access_token,
                    a_endtime = DateTime.Now,
                    a_type = 1
                });
            }
            return accessTokenBag.AccessTokenResult;
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string manageruse)
        {
            return AccessTokenCollection.ContainsKey(manageruse);
        }
    }
}
