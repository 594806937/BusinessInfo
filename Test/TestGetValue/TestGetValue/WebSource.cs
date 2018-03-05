using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGetValue
{
    public class WebSource
    {
        protected string name;
        public string Name
        {
            get { return this.name; }
        }

        protected string url;
        public string URL
        {
            get { return this.url; }
        }
    }

    public class HBZFCGW : WebSource
    {
        public HBZFCGW()
        {
            this.name = "河北省政府采购网";
            this.url = "http://www.ccgp-hebei.gov.cn/zfcg/";
        }
    }
}
