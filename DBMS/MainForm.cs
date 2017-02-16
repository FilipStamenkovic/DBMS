using DBMS.DataLayer;
using DBMS.ObjectModel;
using DBMS.Processors;
using DBMS.Queries;
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
        private ILog _log = LogManager.GetLogger("DBMS");
        private DataGridView dataGridView;
        public const int PageSize = 200;
        private IProcessor processor;
        private const string labelText = "Time elapsed: {0}. Rows fetched: {1}";


        public MainForm()
        {
            InitializeComponent();
            dataGridView = null;
        }

        private void CreateProcessorAndRefresh(ProcessorType processorType, string query, int columnCount)
        {
            if (processor != null)
            {
                processor.Dispose();
                processor.QueryExecuted -= Processor_QueryExecuted;
            }

            if (processorType == ProcessorType.EFProcessor)
                processor = new EFProcessor(dataGridView);
            else if (processorType == ProcessorType.PaggingProcessor)
                processor = new PaggingProcessor(query, columnCount);

            processor.QueryExecuted += Processor_QueryExecuted;

            if (dataGridView.VirtualMode)
                dataGridView.RowCount = processor.GetRowCount();
        }

        private void CreateDataGridView(bool isVirtual)
        {
            if (dataGridView != null)
            {
                if (dataGridView.VirtualMode)
                    dataGridView.CellValueNeeded -= DataGridView_CellValueNeeded;

                this.Controls.Remove(dataGridView);
                dataGridView.Dispose();
            }

            dataGridView = new DataGridView();

            ((ISupportInitialize)(dataGridView)).BeginInit();
            this.SuspendLayout();

            dataGridView.ColumnHeadersVisible = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToOrderColumns = true;
            dataGridView.AutoGenerateColumns = !isVirtual;
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
            {
                CreateColumns();
                dataGridView.CellValueNeeded += DataGridView_CellValueNeeded;
            }

            this.panel.Controls.Add(dataGridView);

            ((ISupportInitialize)(dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.Invalidate();
        }

        private void DataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = processor.GetCellValue(e.RowIndex, e.ColumnIndex);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(false);
        }

        private void eFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
            CreateProcessorAndRefresh(ProcessorType.EFProcessor, null, dataGridView.ColumnCount);
        }

        private void Processor_QueryExecuted(double time, int rowCount)
        {
            toolStripStatusLabel1.Text = string.Format(labelText, time.ToString(".000"), rowCount);
        }

        private void fixedPaggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);

            CreateProcessorAndRefresh(ProcessorType.PaggingProcessor, Query.PaggingQuery, dataGridView.ColumnCount);
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
            CreateProcessorAndRefresh(ProcessorType.PaggingProcessor, Query.InitialQuery, dataGridView.ColumnCount);
        }

        private void CreateColumns()
        {
            DataGridViewTextBoxColumn gvC1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC4 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC6 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC7 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC8 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn gvC9 = new DataGridViewTextBoxColumn();
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
            gvC1.Width = 130;
            


            gvC2.HeaderText = "Batch";
            gvC2.Name = "Batch";
            gvC2.Width = 130;

            gvC3.HeaderText = "BatchType";
            gvC3.Name = "BatchType";
            gvC3.Width = 130;

            gvC4.HeaderText = "BatchSegment";
            gvC4.Name = "BatchSegment";
            gvC4.Width = 130;

            gvC5.HeaderText = "BatchLot";
            gvC5.Name = "BatchLot";
            gvC5.Width = 130;

            gvC6.HeaderText = "PowderCharge";
            gvC6.Name = "PowderCharge";
            gvC6.Width = 130;

            gvC7.HeaderText = "TestPlan";
            gvC7.Name = "TestPlan";
            gvC7.Width = 130;

            gvC8.HeaderText = "TestPlanRevision";
            gvC8.Name = "TestPlanRevision";
            gvC8.Width = 130;

            gvC9.HeaderText = "Material";
            gvC9.Name = "Material";
            gvC9.Width = 130;

            gvC10.HeaderText = "MaterialDescription";
            gvC10.Name = "MaterialDescription";
            gvC10.Width = 130;

            gvC11.HeaderText = "VaristorType";
            gvC11.Name = "VaristorType";
            gvC11.Width = 130;

            gvC12.HeaderText = "VarDiameter";
            gvC12.Name = "VarDiameter";
            gvC12.Width = 130;

            gvC13.HeaderText = "VarHeight";
            gvC13.Name = "VarHeight";
            gvC13.Width = 130;

            gvC14.HeaderText = "TestTs";
            gvC14.Name = "TestTs";
            gvC14.Width = 130;

            gvC15.HeaderText = "ProductSerial";
            gvC15.Name = "ProductSerial";
            gvC15.Width = 130;

            gvC16.HeaderText = "TestStatus";
            gvC16.Name = "TestStatus";
            gvC16.Width = 130;

            gvC17.HeaderText = "Class1";
            gvC17.Name = "Class1";
            gvC17.Width = 130;

            gvC18.HeaderText = "Class2";
            gvC18.Name = "Class2";
            gvC18.Width = 130;

            gvC19.HeaderText = "TestTemperature";


            gvC19.Name = "TestTemperature";
            gvC19.Width = 130;

            gvC20.HeaderText = "DCTs";


            gvC20.Name = "DCTs";
            gvC20.Width = 130;

            gvC21.HeaderText = "DCParam1";


            gvC21.Name = "DCParam1";
            gvC21.Width = 130;

            gvC22.HeaderText = "DCParam2";


            gvC22.Name = "DCParam2";
            gvC22.Width = 130;

            gvC23.HeaderText = "DCParam3";


            gvC23.Name = "DCParam3";
            gvC23.Width = 130;

            gvC24.HeaderText = "DCParam4";


            gvC24.Name = "DCParam4";
            gvC24.Width = 130;

            gvC25.HeaderText = "DCParam5";


            gvC25.Name = "DCParam5";
            gvC25.Width = 130;

            gvC26.HeaderText = "DCParam6";


            gvC26.Name = "DCParam6";
            gvC26.Width = 130;

            gvC27.HeaderText = "DCParam7";


            gvC27.Name = "DCParam7";
            gvC27.Width = 130;

            gvC28.HeaderText = "DCParam8";


            gvC28.Name = "DCParam8";
            gvC28.Width = 130;

            gvC29.HeaderText = "DCAlpha";


            gvC29.Name = "DCAlpha";
            gvC29.Width = 130;

            gvC30.HeaderText = "DCStatus";


            gvC30.Name = "DCStatus";
            gvC30.Width = 130;

            gvC31.HeaderText = "ACTs";


            gvC31.Name = "ACTs";
            gvC31.Width = 130;

            gvC32.HeaderText = "ACParam1";


            gvC32.Name = "ACParam1";
            gvC32.Width = 130;

            gvC33.HeaderText = "ACParam2";


            gvC33.Name = "ACParam2";
            gvC33.Width = 130;

            gvC34.HeaderText = "ACParam3";


            gvC34.Name = "ACParam3";
            gvC34.Width = 130;

            gvC35.HeaderText = "ACParam4";


            gvC35.Name = "ACParam4";
            gvC35.Width = 130;

            gvC36.HeaderText = "ACParam5";


            gvC36.Name = "ACParam5";
            gvC36.Width = 130;

            gvC37.HeaderText = "ACStatus";


            gvC37.Name = "ACStatus";
            gvC37.Width = 130;

            gvC38.HeaderText = "RestTs";


            gvC38.Name = "RestTs";
            gvC38.Width = 130;

            gvC39.HeaderText = "RestParam1";


            gvC39.Name = "RestParam1";
            gvC39.Width = 130;

            gvC40.HeaderText = "RestParam2";


            gvC40.Name = "RestParam2";
            gvC40.Width = 130;

            gvC41.HeaderText = "RestParam3";


            gvC41.Name = "RestParam3";
            gvC41.Width = 130;

            gvC42.HeaderText = "RestParam4";


            gvC42.Name = "RestParam4";
            gvC42.Width = 130;

            gvC43.HeaderText = "RestParam5";


            gvC43.Name = "RestParam5";
            gvC43.Width = 130;

            gvC44.HeaderText = "RestParam6";


            gvC44.Name = "RestParam6";
            gvC44.Width = 130;

            gvC45.HeaderText = "RestStatus";


            gvC45.Name = "RestStatus";
            gvC45.Width = 130;

            gvC46.HeaderText = "ChargeTs";


            gvC46.Name = "ChargeTs";
            gvC46.Width = 130;

            gvC47.HeaderText = "ChargeParam1";


            gvC47.Name = "ChargeParam1";
            gvC47.Width = 130;

            gvC48.HeaderText = "ChargeParam2";


            gvC48.Name = "ChargeParam2";
            gvC48.Width = 130;

            gvC49.HeaderText = "ChargeParam3";


            gvC49.Name = "ChargeParam3";
            gvC49.Width = 130;

            gvC50.HeaderText = "ChargeStatus";


            gvC50.Name = "ChargeStatus";
            gvC50.Width = 130;

            DataGridViewColumn[] columnRange = new DataGridViewColumn[] {
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
            gvC50};

            columnRange.ToList().ForEach(c => c.SortMode = DataGridViewColumnSortMode.Programmatic);
            this.dataGridView.Columns.AddRange(columnRange);

        }
    }
}
