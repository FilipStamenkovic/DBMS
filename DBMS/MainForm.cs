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
        public const int PageSize = 200;

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

        private void initialPaggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
        }

        private void CreateColumns()
        {            
            DataGridViewTextBoxColumn gvC1 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC2 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC3 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC4 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC5 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC6 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC7 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC8 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC9 = new  DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC10 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC11 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC12 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC13 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC14 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC15 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC16 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC17 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC18 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC19 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC20 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC21 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC22 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC23 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC24 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC25 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC26 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC27 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC28 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC29 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC30 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC31 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC32 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC33 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC34 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC35 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC36 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC37 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC38 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC39 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC40 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC41 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC42 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC43 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC44 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC45 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC46 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC47 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC48 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC49 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC50 = new DataGridViewTextBoxColumn();

            gvC1.HeaderText = "Operation";
            gvC1.Name = "Operation";
            gvC1.Width = 130;//.Width = 40;


            
            gvC2.HeaderText = "Batch";
            gvC2.Name = "Batch";
            gvC2.Width = 130;//.Width = 40;

            gvC3.HeaderText = "BatchType";
            gvC3.Name = "BatchType";
            gvC3.Width = 130;//.Width = 40;

            gvC4.HeaderText = "BatchSegment";
            gvC4.Name = "BatchSegment";
            gvC4.Width = 130;//.Width = 40;
            gvC5.FieldName = "MO_Charge_Lage";
            gvC5.HeaderText = "MO_Charge_Lage";
            gvC5.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC5.IsAutoGenerated = true;
            gvC5.Name = "MO_Charge_Lage";
            gvC5.Width = 130;//.Width = 40;
            gvC6.FieldName = "Pulver_Charge_Nr";
            gvC6.HeaderText = "Pulver_Charge_Nr";
            gvC6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC6.IsAutoGenerated = true;
            gvC6.Name = "Pulver_Charge_Nr";
            gvC6.Width = 130;//.Width = 40;
            gvC7.FieldName = "Pruefplan_Nr";
            gvC7.HeaderText = "Pruefplan_Nr";
            gvC7.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC7.IsAutoGenerated = true;
            gvC7.Name = "Pruefplan_Nr";
            gvC7.Width = 130;//.Width = 40;
            gvC8.FieldName = "Pruefplan_Rev";
            gvC8.HeaderText = "Pruefplan_Rev";
            gvC8.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC8.IsAutoGenerated = true;
            gvC8.Name = "Pruefplan_Rev";
            gvC8.Width = 130;//.Width = 40;
            gvC9.FieldName = "Material_Nr";
            gvC9.HeaderText = "Material_Nr";
            gvC9.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC9.IsAutoGenerated = true;
            gvC9.Name = "Material_Nr";
            gvC9.Width = 130;//.Width = 40;
            gvC10.FieldName = "Material_Bezeichung";
            gvC10.HeaderText = "Material_Bezeichung";
            gvC10.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC10.IsAutoGenerated = true;
            gvC10.Name = "Material_Bezeichung";
            gvC10.Width = 130;//.Width = 40;
            gvC11.FieldName = "Var_Typ";
            gvC11.HeaderText = "Var_Typ";
            gvC11.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC11.IsAutoGenerated = true;
            gvC11.Name = "Var_Typ";
            gvC11.Width = 130;//.Width = 40;
            gvC12.FieldName = "Var_D";
            gvC12.HeaderText = "Var_D";
            gvC12.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC12.IsAutoGenerated = true;
            gvC12.Name = "Var_D";
            gvC12.Width = 130;//.Width = 40;
            gvC13.FieldName = "Var_H";
            gvC13.HeaderText = "Var_H";
            gvC13.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC13.IsAutoGenerated = true;
            gvC13.Name = "Var_H";
            gvC13.Width = 130;//.Width = 40;
            gvC14.FieldName = "t_Pruefung";
            gvC14.HeaderText = "t_Pruefung";
            gvC14.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC14.IsAutoGenerated = true;
            gvC14.Name = "t_Pruefung";
            gvC14.Width = 130;//.Width = 40;
            gvC15.FieldName = "Var_Id";
            gvC15.HeaderText = "Var_Id";
            gvC15.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC15.IsAutoGenerated = true;
            gvC15.Name = "Var_Id";
            gvC15.Width = 130;//.Width = 40;
            gvC16.FieldName = "Status_Pruefung";
            gvC16.HeaderText = "Status_Pruefung";
            gvC16.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC16.IsAutoGenerated = true;
            gvC16.Name = "Status_Pruefung";
            gvC16.Width = 130;//.Width = 40;
            gvC17.FieldName = "Var_Klasse_1";
            gvC17.HeaderText = "Var_Klasse_1";
            gvC17.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC17.IsAutoGenerated = true;
            gvC17.Name = "Var_Klasse_1";
            gvC17.Width = 130;//.Width = 40;
            gvC18.FieldName = "Var_Klasse_2";
            gvC18.HeaderText = "Var_Klasse_2";
            gvC18.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC18.IsAutoGenerated = true;
            gvC18.Name = "Var_Klasse_2";
            gvC18.Width = 130;//.Width = 40;
            gvC19.FieldName = "T_Pruefung";
            gvC19.HeaderText = "T_Pruefung";
            gvC19.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC19.IsAutoGenerated = true;
            gvC19.Name = "Temperature_Pruefung";
            gvC19.Width = 130;//.Width = 40;
            gvC20.FieldName = "t_DC";
            gvC20.HeaderText = "t_DC";
            gvC20.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC20.IsAutoGenerated = true;
            gvC20.Name = "t_DC";
            gvC20.Width = 130;//.Width = 40;
            gvC21.FieldName = "Iref_DC_Ist";
            gvC21.HeaderText = "Iref_DC_Ist";
            gvC21.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC21.IsAutoGenerated = true;
            gvC21.Name = "Iref_DC_Ist";
            gvC21.Width = 130;//.Width = 40;
            gvC22.FieldName = "Iref2_DC_Ist";
            gvC22.HeaderText = "Iref2_DC_Ist";
            gvC22.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC22.IsAutoGenerated = true;
            gvC22.Name = "Iref2_DC_Ist";
            gvC22.Width = 130;//.Width = 40;
            gvC23.FieldName = "Uref_DC_Ist";
            gvC23.HeaderText = "Uref_DC_Ist";
            gvC23.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC23.IsAutoGenerated = true;
            gvC23.Name = "Uref_DC_Ist";
            gvC23.Width = 130;//.Width = 40;
            gvC24.FieldName = "Uref_DC_korr";
            gvC24.HeaderText = "Uref_DC_korr";
            gvC24.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC24.IsAutoGenerated = true;
            gvC24.Name = "Uref_DC_korr";
            gvC24.Width = 130;//.Width = 40;
            gvC25.FieldName = "Uref2_DC_Ist";
            gvC25.HeaderText = "Uref2_DC_Ist";
            gvC25.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC25.IsAutoGenerated = true;
            gvC25.Name = "Uref2_DC_Ist";
            gvC25.Width = 130;//.Width = 40;
            gvC26.FieldName = "Uref2_DC_korr";
            gvC26.HeaderText = "Uref2_DC_korr";
            gvC26.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC26.IsAutoGenerated = true;
            gvC26.Name = "Uref2_DC_korr";
            gvC26.Width = 130;//.Width = 40;
            gvC27.FieldName = "Pv_DC_Ist";
            gvC27.HeaderText = "Pv_DC_Ist";
            gvC27.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC27.IsAutoGenerated = true;
            gvC27.Name = "Pv_DC_Ist";
            gvC27.Width = 130;//.Width = 40;
            gvC28.FieldName = "Pv_DC_korr";
            gvC28.HeaderText = "Pv_DC_korr";
            gvC28.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC28.IsAutoGenerated = true;
            gvC28.Name = "Pv_DC_korr";
            gvC28.Width = 130;//.Width = 40;
            gvC29.FieldName = "Alpha_DC_Ist";
            gvC29.HeaderText = "Alpha_DC_Ist";
            gvC29.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC29.IsAutoGenerated = true;
            gvC29.Name = "Alpha_DC_Ist";
            gvC29.Width = 130;//.Width = 40;
            gvC30.FieldName = "Status_DC";
            gvC30.HeaderText = "Status_DC";
            gvC30.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC30.IsAutoGenerated = true;
            gvC30.Name = "Status_DC";
            gvC30.Width = 130;//.Width = 40;
            gvC31.FieldName = "t_AC";
            gvC31.HeaderText = "t_AC";
            gvC31.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC31.IsAutoGenerated = true;
            gvC31.Name = "t_AC";
            gvC31.Width = 130;//.Width = 40;
            gvC32.FieldName = "Iref_AC_Ist";
            gvC32.HeaderText = "Iref_AC_Ist";
            gvC32.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC32.IsAutoGenerated = true;
            gvC32.Name = "Iref_AC_Ist";
            gvC32.Width = 130;//.Width = 40;
            gvC33.FieldName = "Uref_AC_Ist";
            gvC33.HeaderText = "Uref_AC_Ist";
            gvC33.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC33.IsAutoGenerated = true;
            gvC33.Name = "Uref_AC_Ist";
            gvC33.Width = 130;//.Width = 40;
            gvC34.FieldName = "Uref_AC_korr";
            gvC34.HeaderText = "Uref_AC_korr";
            gvC34.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC34.IsAutoGenerated = true;
            gvC34.Name = "Uref_AC_korr";
            gvC34.Width = 130;//.Width = 40;
            gvC35.FieldName = "Pv_AC_Ist";
            gvC35.HeaderText = "Pv_AC_Ist";
            gvC35.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC35.IsAutoGenerated = true;
            gvC35.Name = "Pv_AC_Ist";
            gvC35.Width = 130;//.Width = 40;
            gvC36.FieldName = "Pv_AC_korr";
            gvC36.HeaderText = "Pv_AC_korr";
            gvC36.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC36.IsAutoGenerated = true;
            gvC36.Name = "Pv_AC_korr";
            gvC36.Width = 130;//.Width = 40;
            gvC37.FieldName = "Status_AC";
            gvC37.HeaderText = "Status_AC";
            gvC37.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC37.IsAutoGenerated = true;
            gvC37.Name = "Status_AC";
            gvC37.Width = 130;//.Width = 40;
            gvC38.FieldName = "t_Rest";
            gvC38.HeaderText = "t_Rest";
            gvC38.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC38.IsAutoGenerated = true;
            gvC38.Name = "t_Rest";
            gvC38.Width = 130;//.Width = 40;
            gvC39.FieldName = "Ip_Rest_Ist";
            gvC39.HeaderText = "Ip_Rest_Ist";
            gvC39.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC39.IsAutoGenerated = true;
            gvC39.Name = "Ip_Rest_Ist";
            gvC39.Width = 130;//.Width = 40;
            gvC40.FieldName = "Ul_Rest_Ist";
            gvC40.HeaderText = "Ul_Rest_Ist";
            gvC40.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC40.IsAutoGenerated = true;
            gvC40.Name = "Ul_Rest_Ist";
            gvC40.Width = 130;//.Width = 40;
            gvC41.FieldName = "Up_Rest_Ist";
            gvC41.HeaderText = "Up_Rest_Ist";
            gvC41.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC41.IsAutoGenerated = true;
            gvC41.Name = "Up_Rest_Ist";
            gvC41.Width = 130;//.Width = 40;
            gvC42.FieldName = "Up_Rest_korr";
            gvC42.HeaderText = "Up_Rest_korr";
            gvC42.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC42.IsAutoGenerated = true;
            gvC42.Name = "Up_Rest_korr";
            gvC42.Width = 130;//.Width = 40;
            gvC43.FieldName = "Q_Rest_Ist";
            gvC43.HeaderText = "Q_Rest_Ist";
            gvC43.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC43.IsAutoGenerated = true;
            gvC43.Name = "Q_Rest_Ist";
            gvC43.Width = 130;//.Width = 40;
            gvC44.FieldName = "Anz_Rest_Ist";
            gvC44.HeaderText = "Anz_Rest_Ist";
            gvC44.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC44.IsAutoGenerated = true;
            gvC44.Name = "Anz_Rest_Ist";
            gvC44.Width = 130;//.Width = 40;
            gvC45.FieldName = "Status_Rest";
            gvC45.HeaderText = "Status_Rest";
            gvC45.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC45.IsAutoGenerated = true;
            gvC45.Name = "Status_Rest";
            gvC45.Width = 130;//.Width = 40;
            gvC46.FieldName = "t_Ladung";
            gvC46.HeaderText = "t_Ladung";
            gvC46.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC46.IsAutoGenerated = true;
            gvC46.Name = "t_Ladung";
            gvC46.Width = 130;//.Width = 40;
            gvC47.FieldName = "Ip_Ladung_Ist";
            gvC47.HeaderText = "Ip_Ladung_Ist";
            gvC47.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC47.IsAutoGenerated = true;
            gvC47.Name = "Ip_Ladung_Ist";
            gvC47.Width = 130;//.Width = 40;
            gvC48.FieldName = "Q_Ladung_Ist";
            gvC48.HeaderText = "Q_Ladung_Ist";
            gvC48.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC48.IsAutoGenerated = true;
            gvC48.Name = "Q_Ladung_Ist";
            gvC48.Width = 130;//.Width = 40;
            gvC49.FieldName = "Anz_Ladung_Ist";
            gvC49.HeaderText = "Anz_Ladung_Ist";
            gvC49.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC49.IsAutoGenerated = true;
            gvC49.Name = "Anz_Ladung_Ist";
            gvC49.Width = 130;//.Width = 40;
            gvC50.FieldName = "Status_Ladung";
            gvC50.HeaderText = "Status_Ladung";
            gvC50.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;//.BottomCenter;
            gvC50.IsAutoGenerated = true;
            gvC50.Name = "Status_Ladung";
            gvC50.Width = 130;//.Width = 40;

            ColumnNames = new string[50];

            this.eGridView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gvC0,
            gvC1,
            gvC2,
            gvC3,
            gvC4,
            gvC5,
            gvC6,
            gvC7,
            gvC8,
            gvC9,
            gvC10,
            gvC11,
            gvC12,
            gvC13,
            gvC14,
            gvC15,
            gvC16,
            gvC17,
            gvC18,
            gvC19,
            gvC20,
            gvC21,
            gvC22,
            gvC23,
            gvC24,
            gvC25,
            gvC26,
            gvC27,
            gvC28,
            gvC29,
            gvC30,
            gvC31,
            gvC32,
            gvC33,
            gvC34,
            gvC35,
            gvC36,
            gvC37,
            gvC38,
            gvC39,
            gvC40,
            gvC41,
            gvC42,
            gvC43,
            gvC44,
            gvC45,
            gvC46,
            gvC47,
            gvC48,
            gvC49,
            gvC50});

        }
    }
}
