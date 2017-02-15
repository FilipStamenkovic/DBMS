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
        private DataGridView dataGridView;

        public MainForm()
        {
            InitializeComponent();
            dataGridView = null;
        }

        private void CreateDataGridView(bool isVirtual)
        {
            if (dataGridView != null)
            {
                if(dataGridView.VirtualMode)
                    dataGridView.CellValueNeeded -= DataGridView_CellValueNeeded;

                this.Controls.Remove(dataGridView);
                dataGridView.Dispose();
            }
            dataGridView = new DataGridView();

            ((ISupportInitialize)(dataGridView)).BeginInit();
            this.SuspendLayout();

            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToOrderColumns = true;
            dataGridView.AutoGenerateColumns = true;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ShowCellErrors = false;
            dataGridView.ShowRowErrors = false;
            dataGridView.TabIndex = 3;
            dataGridView.VirtualMode = isVirtual;
            if (isVirtual)
                dataGridView.CellValueNeeded += DataGridView_CellValueNeeded;

            this.Controls.Add(dataGridView);
            ((ISupportInitialize)(dataGridView)).EndInit();
            this.ResumeLayout(false);
        }

        private void DataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(false);
            //using (var db = new DBModel())
            //{
            //    var query = db.Products.Where(p => p.ProductType.Name == "Varistor");

            //    foreach (var product in query)
            //    {
            //        string serial = product.SerialNumber;
            //    }
            //}
        }

        private void eFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(false);
        }

        private void paggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
        }

        private void paggingViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
        }
    }
}
