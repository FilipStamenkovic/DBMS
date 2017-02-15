using DBMS.DataLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    public partial class MainForm : Form
    {
        ILog _log = LogManager.GetLogger("DBMS");
             
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var db = new DBModel())
            {
                var query = db.Products.Where(p => p.ProductType.Name == "Varistor");
                
                foreach (var product in query)
                {
                    string serial = product.SerialNumber;
                }
            }
        }
    }
}
