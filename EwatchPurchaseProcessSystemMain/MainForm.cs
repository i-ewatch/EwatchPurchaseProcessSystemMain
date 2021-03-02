using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.Method;
using EwatchPurchaseProcessSystemMain.Views;
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
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// MySQL設定物件
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// MySQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        /// <summary>
        /// 畫面物件整理
        /// </summary>
        private List<Field4UserControl> field4UserControls = new List<Field4UserControl>();
        public NavigationFrame navigationFrame;
        public MainForm()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File(path: $"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log.txt",
                                      rollingInterval: RollingInterval.Day,
                                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                        .CreateLogger();        //宣告Serilog初始化
            SQLSettings = InitialMethod.InitialSQLSetting();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 畫面加入
            navigationFrame = new NavigationFrame() { Dock = DockStyle.Fill, Parent = panelControl1 };//切換畫面動畫
            NoneView noneView = new NoneView() { Dock = DockStyle.Fill };
            field4UserControls.Add(noneView);
            navigationFrame.AddPage(noneView);
            ProjectCreateView projectCreateView = new ProjectCreateView() { Dock = DockStyle.Fill };
            field4UserControls.Add(projectCreateView);
            navigationFrame.AddPage(projectCreateView);
            #endregion
        }

        private void barListItem1_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e)
        {
            if (barListItem1.DataIndex == 0)
            {
                navigationFrame.SelectedPageIndex = 1;
            }
            else if (barListItem1.DataIndex == 1)
            {
                navigationFrame.SelectedPageIndex = 0;
                ProjectBrowsingForm projectBrowsingForm = new ProjectBrowsingForm() { Dock = DockStyle.None };
                projectBrowsingForm.ShowDialog(this);
            }
            else if (barListItem1.DataIndex == 2)
            {
                navigationFrame.SelectedPageIndex = 0;
                QuotationSearchForm quotationSearchForm = new QuotationSearchForm() { Dock = DockStyle.None };
                quotationSearchForm.ShowDialog(this);
            }
            else if (barListItem1.DataIndex == 3)
            {
                navigationFrame.SelectedPageIndex = 0;
                QuotationImportForm quotationImportForm = new QuotationImportForm() { Dock = DockStyle.None };
                quotationImportForm.ShowDialog(this);
            }
        }

        private void barListItem2_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e)
        {
            if (barListItem2.DataIndex == 0)
            {
                navigationFrame.SelectedPageIndex = 0;
                PurchasePlanBrowsingForm purchasePlanBrowsingForm = new PurchasePlanBrowsingForm() { Dock = DockStyle.None };
                purchasePlanBrowsingForm.ShowDialog(this);
            }
            else if (barListItem2.DataIndex == 1)
            {
                navigationFrame.SelectedPageIndex = 0;
                SignOffOutputForm signOffOutputForm = new SignOffOutputForm() { Dock = DockStyle.None };
                signOffOutputForm.ShowDialog(this);
            }
        }
    }
}
