using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessInfoSpider.Model
{
    public class Bidding
    {
        public Bidding()
        { }
        #region Model
        private string _businessid = Guid.NewGuid().ToString("N").ToUpper();
        private string _businesstitle;
        private string _projectid;
        private string _releasecom;
        private string _money;
        private string _supplier;
        private string _releaselocation;
        private DateTime _releasetime;
        private string _detileurl;
        private string _source;
        private string _xzqhmc;
        private DateTime _updatetime = DateTime.Now;//Convert.ToDateTime(CURRENT_TIMESTAMP);
        /// <summary>
        /// 
        /// </summary>
        public string BusinessID
        {
            set { _businessid = value; }
            get { return _businessid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BusinessTitle
        {
            set { _businesstitle = value; }
            get { return _businesstitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReleaseCom
        {
            set { _releasecom = value; }
            get { return _releasecom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReleaseLocation
        {
            set { _releaselocation = value; }
            get { return _releaselocation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReleaseTime
        {
            set { _releasetime = value; }
            get { return _releasetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetileURL
        {
            set { _detileurl = value; }
            get { return _detileurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XZQHMC
        {
            set { _xzqhmc = value; }
            get { return _xzqhmc; }
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Content { get; set; }
        #endregion Model

    }
}


