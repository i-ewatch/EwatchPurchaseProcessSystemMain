using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel;
using EwatchPurchaseProcessSystemMain.Method;
using EwatchPurchaseProcessSystemMain.ReportView;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EwatchPurchaseProcessSystemMain
{
    public partial class SignOffOutputForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        /// <summary>
        /// 專案查詢內容
        /// </summary>
        private List<DispatchDataTable> searchdispatchdatatable;
        /// <summary>
        /// 簽核單內容
        /// </summary>
        private List<SignOffList> countsignofflist;
        /// <summary>
        /// pk值
        /// </summary>
        private int pk { get; set; }
        private byte agreement { get; set; }
        public SignOffOutputForm()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();
        }
        private void ReviewsimpleButton_Click(object sender, EventArgs e)
        {
            #region 製作簽核單
            if (documentViewer1.DocumentSource != null)
            {
                documentViewer1.Controls.Clear();
            }
            ReportFQ reportFQ = new ReportFQ();
            reportFQ.PaperKind = System.Drawing.Printing.PaperKind.A4;
            string projectno = ProjectNOcomboBoxEdit.Text;
            string projectcode = ProjectCodecomboBoxEdit.Text;
            string buyno = BuyNOtextEdit.Text;
            string projectpurchaseer = SQLMethod.Count_dispatchdatatable()[0].projectleader;
            string branch = BranchtextEdit.Text;
            string project = SQLMethod.Count_dispatchdatatable()[0].ownername;
            string appdate = ApplicationdateEdit.Text;
            string buylimitdate = BuyLimitdateEdit.Text;
            string needdate = NeeddateEdit.Text;
            string pickup = PickuptextEdit.Text;
            string deliery = DeliverytextEdit.Text;
            bool devicecheck = false;
            bool materialcheck = false;
            bool constructioncheck = false;
            bool hangcheck = false;
            bool elsecheck = false;
            bool inagreement = false;
            bool noagreement = false;
            bool just = false;
            bool suggest = false;
            bool remark = checkEdit1.Checked;
            if (remark)
            {
                agreement = 1;
            }
            else
            {
                agreement = 0;
            }
            switch (comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    {
                        devicecheck = true;
                    }
                    break;
                case 1:
                    {
                        materialcheck = true;
                    }
                    break;
                case 2:
                    {
                        constructioncheck = true;
                    }
                    break;
                case 3:
                    {
                        hangcheck = true;
                    }
                    break;
                case 4:
                    {
                        elsecheck = true;
                    }
                    break;
            }
            switch (comboBoxEdit2.SelectedIndex)
            {
                case 0:
                    {
                        inagreement = true;
                    }
                    break;
                case 1:
                    {
                        noagreement = true;
                    }
                    break;
                case 2:
                    {
                        just = true;
                    }
                    break;
                case 3:
                    {
                        suggest = true;
                    }
                    break;
            }
            reportFQ.Textchange(projectno, projectcode, buyno, projectpurchaseer, branch, project, appdate, buylimitdate, needdate, pickup, deliery, devicecheck, materialcheck, constructioncheck, hangcheck, elsecheck, inagreement, noagreement, just, suggest, remark);
            reportFQ.DataxrCrossTabChange();
            reportFQ.CreateDocument();
            documentViewer1.DocumentSource = reportFQ;
            #endregion
            #region 儲存進資料庫
            pk = SQLMethod.Catch_signofflist().Count;
            if (pk != 0)
            {
                pk = pk + 1;
            }
            else
            {
                pk = 1;
            }
            string content = $"{pk},'{projectno}','{projectcode.Substring(0,1)}', '{projectcode}','{buyno}','{branch}','{appdate}','{buylimitdate}','{needdate}','{pickup}','{deliery}','{comboBoxEdit1.Text}','{comboBoxEdit2.Text}',{agreement}";
            SQLMethod.Insert_signofflist(content);
            #endregion
        }

        private void ProjectNOcomboBoxEdit_Properties_MouseEnter(object sender, EventArgs e)
        {
            ProjectNOcomboBoxEdit.Properties.Items.Clear();
            searchdispatchdatatable = SQLMethod.Search_dispatchdatatable();
            for (int i = 0; i < searchdispatchdatatable.Count; i++)
            {
                ProjectNOcomboBoxEdit.Properties.Items.Add(searchdispatchdatatable[i].projectno);
            }
        }

        private void ProjectCodecomboBoxEdit_Properties_MouseEnter(object sender, EventArgs e)
        {
            ProjectCodecomboBoxEdit.Properties.Items.Clear();
            var searchprojectcode = SQLMethod.Count_purchaseplan().Where(g=>g.ProjectNO == $"{ProjectNOcomboBoxEdit.Text}").Select(g=>g.ProjectCode).ToList();
            for (int i = 0; i < SQLMethod.Count_purchaseplan().Count; i++)
            {
                ProjectCodecomboBoxEdit.Properties.Items.Add(searchprojectcode[i]);
            }
        }

        private void ProjectNOcomboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProjectNOcomboBoxEdit.Text != null)
            {
                ProjectCodecomboBoxEdit.Enabled = true;
                ReviewsimpleButton.Enabled = true;
            }
            else
            {
                ProjectCodecomboBoxEdit.Text = null;
                ProjectCodecomboBoxEdit.Enabled = false;
                ReviewsimpleButton.Enabled = false;
            }
        }

        private void ProjectCodecomboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            countsignofflist = SQLMethod.Count_signofflist(ProjectCodecomboBoxEdit.Text, ProjectNOcomboBoxEdit.Text);
            if (countsignofflist.Count != 0)
            {
                BuyNOtextEdit.Text = countsignofflist[0].PurchaseNumber;
                BranchtextEdit.Text = countsignofflist[0].ApplicationSector;
                ApplicationdateEdit.EditValue = countsignofflist[0].ApplicationDate;
                BuyLimitdateEdit.EditValue = countsignofflist[0].PurchaseDate;
                NeeddateEdit.EditValue = countsignofflist[0].NeedDate;
                PickuptextEdit.Text = countsignofflist[0].Receiver;
                DeliverytextEdit.Text = countsignofflist[0].TradingLocation;
                if (countsignofflist[0].Content == "設備")
                {
                    comboBoxEdit1.SelectedIndex = 0;
                }
                else if (countsignofflist[0].Content == "材料")
                {
                    comboBoxEdit1.SelectedIndex = 1;
                }
                else if (countsignofflist[0].Content == "施工")
                {
                    comboBoxEdit1.SelectedIndex = 2;
                }
                else if (countsignofflist[0].Content == "吊運")
                {
                    comboBoxEdit1.SelectedIndex = 3;
                }
                else if (countsignofflist[0].Content == "其他")
                {
                    comboBoxEdit1.SelectedIndex = 4;
                }
                if (countsignofflist[0].Brand == "合約內廠牌")
                {
                    comboBoxEdit2.SelectedIndex = 0;
                }
                else if (countsignofflist[0].Brand == "不限廠牌")
                {
                    comboBoxEdit2.SelectedIndex = 1;
                }
                else if (countsignofflist[0].Brand == "指定廠牌")
                {
                    comboBoxEdit2.SelectedIndex = 2;
                }
                else if (countsignofflist[0].Brand == "建議廠牌")
                {
                    comboBoxEdit2.SelectedIndex = 3;
                }
                if (countsignofflist[0].Check ==1)
                {
                    checkEdit1.Checked = true;
                }
                else
                {
                    checkEdit1.Checked = false;
                }
            }
            else
            {
                BuyNOtextEdit.Text = null;
                BranchtextEdit.Text = null;
                ApplicationdateEdit.EditValue = null;
                BuyLimitdateEdit.EditValue = null;
                NeeddateEdit.EditValue = null;
                PickuptextEdit.Text = null;
                DeliverytextEdit.Text = null;
                comboBoxEdit1.SelectedIndex = -1;
                comboBoxEdit2.SelectedIndex = -1;
                checkEdit1.Checked = false;
            }
        }
    }
}
