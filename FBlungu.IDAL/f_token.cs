using System;
using System.Data;
namespace FBlungu.IDAL
{
    /// <summary>
    /// 接口层f_token
    /// </summary>
    public interface If_token
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(FBlungu.Model.f_token model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(FBlungu.Model.f_token model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int id);
        bool DeleteList(string idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        FBlungu.Model.f_token GetModel(int id);
        FBlungu.Model.f_token DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
        #region  MethodEx

        #endregion  MethodEx
    }
}
