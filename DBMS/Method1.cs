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
    public partial class Method1 : Form
    {
        private Dictionary<string, string> filters;
        public Method1()
        {
            InitializeComponent();

            List<string> data = new List<string>();

            data.Add("first");
            data.Add("second");

            filters = new Dictionary<string, string>();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                filters.Add(column.HeaderText, "");
            }

          //  BindingList<string> list = new BindingList<string>(data);
            
           // dataGridView1.DataSource = data;

           // DataGridViewAutoFilter d;
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TextBox filterBox = new TextBox();
            filterBox.Location = e.Location;
            filterBox.Width = 100;

            filterBox.Text = filters[dataGridView1.Columns[e.ColumnIndex].HeaderText];

            this.Controls.Add(filterBox);

            //filterBox.MouseLeave += (focusSender, args) =>
            //  {
            //      filters[dataGridView1.Columns[e.ColumnIndex].HeaderText] = filterBox.Text;

            //      filterBox.Dispose();
            //      this.Controls.Add(filterBox);
            //  };
        }
    }
}
