using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessInfoSpider.WebSpider;

namespace BusinessInfoSpider
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ZYZFCGW zyzfcgw = new ZYZFCGW();
            HBSZFCGW hbszfcgw = new HBSZFCGW();
            List<BaseSpider> analyseList = new List<BaseSpider>();
            analyseList.Add(zyzfcgw);
            analyseList.Add(hbszfcgw);
            for (int i = 0; i < analyseList.Count; i++)
            {
                analyseList[i].StartAnalyse();
            }
        }
    }
}
