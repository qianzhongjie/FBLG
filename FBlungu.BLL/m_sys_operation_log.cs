﻿using System;
using System.Data;
using System.Collections.Generic;
using FBlungu.Model;
using FBlungu.DALFactory;
using FBlungu.IDAL;
namespace FBlungu.BLL
{
    /// <summary>
    /// m_sys_operation_log
    /// </summary>
    public partial class m_sys_operation_log
    {
        private readonly Im_sys_operation_log dal = DataAccess.Createm_sys_operation_log();
        public m_sys_operation_log()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FBlungu.Model.m_sys_operation_log model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FBlungu.Model.m_sys_operation_log model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        //public bool DeleteList(string idlist)
        //{
        //    return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(idlist, 0));
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FBlungu.Model.m_sys_operation_log GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public FBlungu.Model.m_sys_operation_log GetModelByCache(int id)
        //{

        //    string CacheKey = "m_sys_operation_logModel-" + id;
        //    object objModel = FBlungu.Common.CacheHelper.GetValue(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(id);
        //            if (objModel != null)
        //            {
        //                int ModelCache = FBlungu.Common.CacheHelper.SetValue("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (FBlungu.Model.m_sys_operation_log)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FBlungu.Model.m_sys_operation_log> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FBlungu.Model.m_sys_operation_log> DataTableToList(DataTable dt)
        {
            List<FBlungu.Model.m_sys_operation_log> modelList = new List<FBlungu.Model.m_sys_operation_log>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FBlungu.Model.m_sys_operation_log model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        //}
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}


