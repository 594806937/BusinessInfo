using System.Configuration;
using System.Reflection;
using BusinessInfoViewer.IDAL;

namespace BusinessInfoViewer.DALFactory
{
    /// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
    /// </summary>
    public sealed class DataAccess
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["MySQLDAL"];
        public DataAccess()
        { }

        #region CreateObject

        //不使用缓存
        private static object CreateObjectNoCache(string assemblyparam, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(assemblyparam).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }

        }
        //使用缓存
        private static object CreateObject(string assemblyparam, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    Assembly ass = Assembly.Load(assemblyparam);
                    objType = ass.CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch//(System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        #endregion

        #region 泛型生成
        ///// <summary>
        ///// 创建数据层接口。
        ///// </summary>
        //public static t Create(string ClassName)
        //{

        //    string ClassNamespace = assemblyparam +"."+ ClassName;
        //    object objType = CreateObject(assemblyparam, ClassNamespace);
        //    return (t)objType;
        //}
        #endregion

        #region CreateSysManage
        public static ISysManage CreateSysManage()
        {
            //方式1			
            //return (Maticsoft.IDAL.ISysManage)Assembly.Load(assemblyparam).CreateInstance(assemblyparam+".SysManage");

            //方式2 			
            string classNamespace = AssemblyPath + ".SysManage";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (ISysManage)objType;
        }
        #endregion



        /// <summary>
        /// 创建business数据层接口。
        /// </summary>
        public static IBusiness Createbusiness()
        {

            string classNamespace = AssemblyPath + ".business";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (IBusiness)objType;
        }


        /// <summary>
        /// 创建detile数据层接口。
        /// </summary>
        public static Idetile Createdetile()
        {

            string classNamespace = AssemblyPath + ".detile";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (Idetile)objType;
        }


        /// <summary>
        /// 创建view_querykeyword数据层接口。
        /// </summary>
        public static Iview_querykeyword Createview_querykeyword()
        {

            string classNamespace = AssemblyPath + ".view_querykeyword";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (Iview_querykeyword)objType;
        }

        /// <summary>
        /// 创建BusinessInfo数据层接口。
        /// </summary>
        public static IBusinessInfo CreateBusinessInfo()
        {

            string classNamespace = AssemblyPath + ".BusinessInfo";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (IBusinessInfo)objType;
        }

    }
}