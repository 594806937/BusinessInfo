using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            Thread tr = new Thread(DataCollectStart);
            tr.Start();
        }

        private void DataCollectStart()
        {
            int processcount = 4;
            if (Txt_Process.Text != "")
                processcount = Convert.ToInt32(this.Txt_Process.Text);

            ZYZFCGW zyzfcgw = new ZYZFCGW();
            ZYZFCGW_ZBGG zyzfcgw_zbgg = new ZYZFCGW_ZBGG();
            HBSZFCGW hbszfcgw = new HBSZFCGW();
            ZGZFCGW zgzfcgw = new ZGZFCGW();

            List<BaseSpider> analyseList = new List<BaseSpider>();
            analyseList.Add(zyzfcgw);
            //analyseList.Add(zyzfcgw_zbgg);
            //analyseList.Add(hbszfcgw);
            //analyseList.Add(zgzfcgw);

            for (int i = 0; i < analyseList.Count; i++)
            {
                //this.logBox.BeginInvoke(new Action(() => { this.logBox.Text += string.Format("\n{0}:开始采集:{1}", DateTime.Now, analyseList[i].Name); }));
                analyseList[i].StartAnalyse(processcount);
                //this.logBox.BeginInvoke(new Action(() => { this.logBox.Text += string.Format("\n{0}:采集完成:{1}", DateTime.Now, analyseList[i].Name); }));
            }
        }
    }
}
