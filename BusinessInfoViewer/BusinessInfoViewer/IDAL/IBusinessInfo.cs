using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessInfoViewer.Model;

namespace BusinessInfoViewer.IDAL
{
    public interface IBusinessInfo
    {
        /// <summary>
        /// 根据关键字查询
        /// </summary>
        /// <param name="keywordList"></param>
        /// <returns></returns>
        List<BusinessInfo> SearchKeyWord(List<string> keywordList);
        /// <summary>
        /// 根据关键字查询 可查询相关度
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns></returns>
        List<Model.BusinessInfoEx> SearchKeyWordEx(List<string> keywordList);
        /// <summary>
        /// 根据关键字查询 可查询相关度
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns></returns>
        List<Model.BusinessInfoEx> SearchKeyWordEx(List<Keyword> keywordList);
        /// <summary>
        /// 返回指定日期的带相关度的信息
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Model.BusinessInfoEx> SearchByDate(DateTime date);
    }
}
