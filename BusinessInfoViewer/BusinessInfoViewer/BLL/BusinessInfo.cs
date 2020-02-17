using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessInfoViewer.DALFactory;
using BusinessInfoViewer.IDAL;
using BusinessInfoViewer.Model;

namespace BusinessInfoViewer.BLL
{
    public class BusinessInfo
    {
        private readonly IBusinessInfo dal = DataAccess.CreateBusinessInfo();

        public BusinessInfo()
        {
        }
        /// <summary>
        /// 返回关键字查询结果
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns>最终结果</returns>
        public List<Model.BusinessInfo> SearchKeyWord(List<string> keywordList)
        {
            if (keywordList.Count == 0)
            {
                keywordList = new List<string>() { "地理", "信息系统", "GIS", "软件", "地灾", "测绘" };
            }
            return dal.SearchKeyWord(keywordList);
        }
        /// <summary>
        /// 根据关键字查询 可查询相关度
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns></returns>
        public List<Model.BusinessInfoEx> SearchKeyWordEx(List<string> keywordList)
        {
            if (keywordList.Count == 0)
            {
                keywordList = new List<string>() { "地理", "信息系统", "GIS", "软件", "地灾", "测绘" };
            }
            return dal.SearchKeyWordEx(keywordList);
        }

        /// <summary>
        /// 根据关键字查询 可查询相关度
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns></returns>
        public List<Model.BusinessInfoEx> SearchKeyWordEx(List<Keyword> keywordList)
        {
            return dal.SearchKeyWordEx(keywordList);
        }
    }
}
