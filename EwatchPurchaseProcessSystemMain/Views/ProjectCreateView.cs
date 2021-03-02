using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.Method;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EwatchPurchaseProcessSystemMain.Views
{
    public partial class ProjectCreateView : Field4UserControl
    {
        public MainForm mainForm { get; set; }
        /// <summary>
        /// MySQL設定物件
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// MySQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        /// <summary>
        /// 議價紀錄是否
        /// </summary>
        private byte bargain { get; set; }
        /// <summary>
        /// 工程保險是否
        /// </summary>
        private byte insurance { get; set; }
        /// <summary>
        /// 合約報價單與否
        /// </summary>
        private byte contractmoney { get; set; }
        /// <summary>
        /// 合約成本單與否
        /// </summary>
        private byte contractcost { get; set; }
        public ProjectCreateView()
        {
            InitializeComponent();
            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();
        }

        private void SetUpsimpleButton_Click(object sender, EventArgs e)
        {
            if (BargainYEScheckEdit.Checked == true)
            {
                bargain = 1;
            }
            if (BargainNONEcheckEdit.Checked == true)
            {
                bargain = 0;
            }
            if (InsuranceNOcheckEdit.Checked == true)
            {
                insurance = 0;
            }
            if (InsuranceYEScheckEdit.Checked == true)
            {
                insurance = 1;
            }
            if (ContractMoneycheckEdit.Checked == true)
            {
                contractmoney = 1;
            }
            else
            {
                contractmoney = 0;
            }
            if (ContractCostcheckEdit.Checked == true)
            {
                contractcost = 1;
            }
            else
            {
                contractcost = 0;
            }
            string content = $"'{DatedateEdit.DateTime.ToString("yyyy/MM/dd HH:mm:ss")}', '{ProjectNOtextEdit.Text}', '{OwnerNametextEdit.Text}', '{OwnerAddresstextEdit.Text}', '{OwnerNOtextEdit.Text}', " +
                $"'{WorkStartdateEdit.DateTime.ToString("yyyy/MM/dd HH:mm:ss")}', '{WorkEnddateEdit.DateTime.ToString("yyyy/MM/dd HH:mm:ss")}', '{ContacttextEdit.Text}', '{ContactPhonetextEdit.Text}', '{ProjectLeadertextEdit.Text}', " +
                $"'{ProjectWorkLeadertextEdit.Text}', '{BargainPersontextEdit.Text}', {bargain}, '{BargainRensontextEdit.Text}', '{BargaindateEdit.DateTime.ToString("yyyy/MM/dd HH:mm:ss")}', " +
                $"'{BargainLocaltextEdit.Text}', '{BargainPricecomboBoxEdit.Text}', {Convert.ToInt32(BargainMoneytextEdit.Text)}, '{QuotationCodetextEdit.Text}', {Convert.ToInt32(QuotationPricetextEdit.Text)}, " +
                $"{insurance}, '{InsuranceStartdateEdit.DateTime.ToString("yyyy/MM/dd HH:mm:ss")}', '{InsuranceEnddateEdit.DateTime.ToString("yyyy/MM/dd HH:mm:ss")}', {Convert.ToInt32(ContractMoneytextEdit.Text)}, {contractmoney}, " +
                $"'{PlanCosttextEdit.Text}',{contractcost}, '{PlanGoaltextEdit.Text}', '{PONumbertextEdit.Text}'";
            SQLMethod.Insert_dispatchdatatable(content);
        }

        private void CancelsimpleButton_Click(object sender, EventArgs e)
        {
            DatedateEdit.Text = null;
            ProjectNOtextEdit.Text = null;
            OwnerNametextEdit.Text = null;
            OwnerAddresstextEdit.Text = null;
            OwnerNOtextEdit.Text = null;
            WorkStartdateEdit.Text = null;
            WorkEnddateEdit.Text = null;
            ContacttextEdit.Text = null;
            ContactPhonetextEdit.Text = null;
            ProjectLeadertextEdit.Text = null;
            ProjectWorkLeadertextEdit.Text = null;
            BargainPersontextEdit.Text = null;
            BargainYEScheckEdit.Checked = false;
            BargainNONEcheckEdit.Checked = false;
            BargainRensontextEdit.Text = null;
            BargaindateEdit.Text = null;
            BargainLocaltextEdit.Text = null;
            BargainPricecomboBoxEdit.SelectedIndex = -1;
            BargainMoneytextEdit.Text = null;
            QuotationCodetextEdit.Text = null;
            QuotationPricetextEdit.Text = null;
            InsuranceNOcheckEdit.Checked = false;
            InsuranceYEScheckEdit.Checked = false;
            InsuranceStartdateEdit.Text = null;
            InsuranceEnddateEdit.Text = null;
            ContractMoneytextEdit.Text = null;
            ContractMoneycheckEdit.Checked = false;
            PlanCosttextEdit.Text = null;
            ContractCostcheckEdit.Checked = false;
            PlanGoaltextEdit.Text = null;
            PONumbertextEdit.Text = null;
        }
    }
}
