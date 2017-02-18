using DBMS.DataLayer;
using DBMS.Helpers;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    public partial class MainForm : Form
    {
        private ILog _log = LogManager.GetLogger("DBMS");
        private DataGridView dataGridView;
        public const int PageSize = 300;
        private string sortedColumn;
        private bool ascending;
        private IProcessor processor;
        private const string labelText = "Time elapsed: {0}. Rows fetched: {1}";
        private string filteredColumn = "";
        private string filteredValue = "";
        private TextBox filterBox;
        private bool IndexOn = false;
        private ScrollBar s;
        public static bool UseDisplayResultsTable = false;
        public static event DbConnectionChanged ConnectionChaged;
        public static event UsePreJoinedTableChanged UseDisplayResultsChanged;

        public static string ConnectionName { get; internal set; }

        public MainForm()
        {
            InitializeComponent();
            dataGridView = null;
            ConnectionName = "DBConnection";
        }

        private void SetProcessorAndRefresh(IProcessor processorType)
        {
            if (processor != null)
            {
                processor.Dispose();
                processor.QueryExecuted -= Processor_QueryExecuted;
            }
            this.processor = processorType;
            //if (processorType == ProcessorType.EFProcessor)
            //    processor = new EFProcessor(dataGridView);
            //else if (processorType == ProcessorType.PaggingProcessor)
            //    processor = new PaggingProcessor(query, columnCount, queryCount);
            //else if (processorType == ProcessorType.ViewProcessor)
            //    processor = new ViewProcessor(query, columnCount, queryCount);

            processor.QueryExecuted += Processor_QueryExecuted;

            if (dataGridView.VirtualMode)
                dataGridView.RowCount = processor.GetRowCount();
        }

        private void CreateDataGridView(bool isVirtual)
        {
            if (dataGridView != null)
            {
                if (dataGridView.VirtualMode)
                {
                    dataGridView.CellValueNeeded -= DataGridView_CellValueNeeded;
                    dataGridView.ColumnHeaderMouseClick -= DataGridView_ColumnHeaderMouseClick;
                    dataGridView.ColumnHeaderMouseDoubleClick -= DataGridView_ColumnHeaderMouseDoubleClick;
                    s.Scroll -= new ScrollEventHandler(DataGridView_Scroll);
                }
                this.Controls.Remove(dataGridView);
                dataGridView.Dispose();
            }            

            ((ISupportInitialize)(dataGridView)).BeginInit();
            this.SuspendLayout();

            dataGridView = DataGridViewHelper.CreateDataGridView(isVirtual);            

            if (isVirtual)
            {
                dataGridView.CellValueNeeded += DataGridView_CellValueNeeded;
                dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
                dataGridView.ColumnHeaderMouseDoubleClick += DataGridView_ColumnHeaderMouseDoubleClick;

                Type t = dataGridView.GetType();
                PropertyInfo pi = t.GetProperty("VerticalScrollBar", BindingFlags.Instance | BindingFlags.NonPublic);
                s = pi.GetValue(dataGridView, null) as ScrollBar;

                s.Scroll += new ScrollEventHandler(DataGridView_Scroll);
            }

            this.panel.Controls.Add(dataGridView);

            ((ISupportInitialize)(dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.Invalidate();
        }

        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type != ScrollEventType.EndScroll)
                //dataGridView.SuspendLayout();
                dataGridView.CellValueNeeded -= DataGridView_CellValueNeeded;
            else
            {
                dataGridView.CellValueNeeded += DataGridView_CellValueNeeded;
                dataGridView.Refresh();
            }
        }

        private void DataGridView_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            filterBox = new TextBox();
            Rectangle rect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            filterBox.Location = new Point(rect.Location.X, rect.Location.Y + rect.Height / 2);
            filterBox.Width = 200;

            filterBox.Text = filteredColumn == dataGridView.Columns[e.ColumnIndex].Name ? filteredValue : "";
            filteredColumn = dataGridView.Columns[e.ColumnIndex].Name;

            this.Controls.Add(filterBox);
            filterBox.BringToFront();
            filterBox.Select();
            filterBox.Focus();
            filterBox.LostFocus += (focusSender, args) =>
                          {
                              string enteredText = filterBox.Text;
                              this.Controls.Remove(filterBox);
                              filterBox.Dispose();
                              filterBox = null;

                              if (filteredValue != enteredText)
                              {
                                  filteredValue = enteredText;
                                  RefreshGrid();
                              }
                          };
        }

        private void RefreshGrid()
        {
            dataGridView.RowCount = 0;
            dataGridView.Refresh();

            processor.SetFilterAndSort(sortedColumn, filteredColumn, filteredValue, ascending);

            dataGridView.RowCount = processor.GetRowCount();
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string columnName = dataGridView.Columns[e.ColumnIndex].Name;

                if (!string.IsNullOrEmpty(sortedColumn) && sortedColumn.Contains(columnName))
                {
                    ascending = !ascending;
                }
                else
                    ascending = true;

                sortedColumn = columnName;

                RefreshGrid();
            }
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
        }

        private void Processor_QueryExecuted(double time, int rowCount)
        {
            toolStripStatusLabel1.Text = string.Format(labelText, time.ToString(".000"), rowCount);
        }

        private void fixedPaggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
            SetProcessorAndRefresh(new PaggingProcessor(Query.PaggingQuery, dataGridView.ColumnCount, Query.PaggingQueryCount));
        }

        private void paggingViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
            SetProcessorAndRefresh(new ViewProcessor(Query.ViewQuery, dataGridView.ColumnCount, Query.ViewCount, false));
        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
            SetProcessorAndRefresh(new ViewProcessor(Query.OneTableSqlQuery, dataGridView.ColumnCount, Query.OneTableSqlCount, true));
        }

        private void initialPaggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDataGridView(true);
            SetProcessorAndRefresh(new PaggingProcessor(Query.InitialQuery, dataGridView.ColumnCount, null));
        }

        private void turnIndexONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IndexOn)
            {
                turnIndexONToolStripMenuItem.Text = "Turn Index OFF";
                ConnectionName = "DB_IndexedConnection";
            }
            else
            {
                turnIndexONToolStripMenuItem.Text = "Turn Index ON";
                ConnectionName = "DBConnection";
            }

            IndexOn = !IndexOn;

            if (ConnectionChaged != null)
                ConnectionChaged.Invoke(ConnectionName);
        }

        private void joinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseDisplayResultsTable)
            {
                UseDisplayResultsTable = false;

                if (UseDisplayResultsChanged != null)
                    UseDisplayResultsChanged.Invoke(true);

                if (processor != null)
                    dataGridView.RowCount = processor.GetRowCount();
            }

            if (dataGridView == null || !(processor is EFProcessor))
            {
                CreateDataGridView(true);
                SetProcessorAndRefresh(new EFProcessor(dataGridView));
            }
        }

        private void preJoinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UseDisplayResultsTable)
            {
                UseDisplayResultsTable = true;

                if (UseDisplayResultsChanged != null)
                    UseDisplayResultsChanged.Invoke(true);

                if (processor != null)
                    dataGridView.RowCount = processor.GetRowCount();
            }

            if (dataGridView == null || !(processor is EFProcessor))
            {
                CreateDataGridView(true);
                SetProcessorAndRefresh(new EFProcessor(dataGridView));
            }
        }
    }

    public delegate void DbConnectionChanged(string connectionName);
    public delegate void UsePreJoinedTableChanged(bool usePreJoinedTable);
}
