using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel
{
    public partial class DispatchDataTable
    {
        public DateTime projectdatettime { get; set; }
        public string projectno { get; set; }
        public string ownername { get; set; }
        public string owneraddress { get; set; }
        public string ownerno { get; set; }
        public DateTime workstartdate { get; set; }
        public DateTime workenddate { get; set; }
        public string contact { get; set; }
        public string contactphone { get; set; }
        public string projectleader { get; set; }
        public string projectworkleader { get; set; }
        public string bargainperson { get; set; }
        public byte bargainyesornot { get; set; }
        public string bargainrenson { get; set; }
        public DateTime bargaindate { get; set; }
        public string bargainlocal { get; set; }
        public string bargainprice { get; set; }
        public int bargainmoney { get; set; }
        public string quotationcode { get; set; }
        public int quotationprice { get; set; }
        public byte insuranceyesornot { get; set; }
        public DateTime insurancestartdate { get; set; }
        public DateTime insuranceenddate { get; set; }
        public int contractmoney { get; set; }
        public byte contractmoneyyesornot { get; set; }
        public string plancost { get; set; }
        public byte contractcost { get; set; }
        public string plangoal { get; set; }
        public string ponumber { get; set; }
    }
}
