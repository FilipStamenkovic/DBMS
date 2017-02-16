using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.DataLayer
{
    public partial class DisplayResult
    {
        public DisplayResult()
        {

        }

        public string Operation { get; set; }
        public string Batch { get; set; }
        public string BatchType { get; set; }
        public string BatchSegment { get; set; }
        public string BatchLot { get; set; }
        public string PowderCharge { get; set; }
        public string TestPlan { get; set; }
        public int TestPlanRevision { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
        public string VaristorType { get; set; }
        public string VarDiameter { get; set; }
        public string VarHeight { get; set; }

        public virtual TestResult TestResult { get; set; }
    }
}
