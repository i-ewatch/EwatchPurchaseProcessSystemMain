using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel
{
    public partial class PurchasePlan
    {
        public int pk { get; set; }
        public string ProjectNO { get; set; }
        public int ProjectItem { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectPurchaser { get; set; }
        public string ProjectContent { get; set; }
        public Nullable<System.DateTime> EstimatedMailDate { get; set; }
        public Nullable<System.DateTime> RealMailDate { get; set; }
        public string BuyMan { get; set; }
        public Nullable<System.DateTime> EstimatedDecisionDate { get; set; }
        public Nullable<System.DateTime> RaelDecisionDate { get; set; }
        public Nullable<bool> LongTimeDevice { get; set; }
        public string ContactCode { get; set; }
        public string ErrorCode { get; set; }
        public string Vendor { get; set; }
        public string OrderCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }
        public int EstimatedCost { get; set; }
        public int ExecutionGoal { get; set; }
        public Nullable<int> PackageCoin { get; set; }
        public int NuPackageCoin { get; set; }
        public int Subtotal { get; set; }
        public int DiffCoin { get; set; }
        public Nullable<int> AccCoin { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }
    }
}
