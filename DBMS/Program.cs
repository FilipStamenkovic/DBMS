using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}





//filterBox = new TextBox();
//filterBox.Location = e.Location;
//            filterBox.Width = 200;

//            filterBox.Text = filters[dataGridView1.Columns[e.ColumnIndex].HeaderText];

//            this.Controls.Add(filterBox);
//filterBox.BringToFront();
//filterBox.Select();
//filterBox.Focus();
//filterBox.LostFocus += (focusSender, args) =>
//              {
//    filters[dataGridView1.Columns[e.ColumnIndex].HeaderText] = filterBox.Text;

//    this.Controls.Remove(filterBox);
//    filterBox.Dispose();
//    filterBox = null;

//    RefreshGrid();
//};
//}