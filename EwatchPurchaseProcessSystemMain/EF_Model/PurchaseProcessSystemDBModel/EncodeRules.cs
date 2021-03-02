using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel
{
    public partial class EncodeRule
    {
        public int pk { get; set; }
        public string Code { get; set; }
        public string CodeMeam { get; set; }
    }
}
