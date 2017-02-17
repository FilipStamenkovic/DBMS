using DBMS.DataLayer;
using DBMS.ObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;
using DBMS.Queries;

namespace DBMS.Processors
{
    public class PaggingProcessor : IProcessor
    {
        private List<object[]> data;
        private int pageNumber;
        private string query;
        private string queryCount;
        private int columnCount;
        private Stopwatch stopwatch;
        private Dictionary<string, string> columnMapper;
        private string whereConditions;
        private string sortMode;

        public event QueryExecuted QueryExecuted;

        public PaggingProcessor(string query, int columnCount, string queryCount)
        {
            this.query = query;
            this.queryCount = queryCount;
            this.columnCount = columnCount;
            stopwatch = new Stopwatch();

            columnMapper = new Dictionary<string, string>();
            columnMapper.Add("Operation", "ppo.ExternalId");
            columnMapper.Add("Batch", "pop1.Value");
            columnMapper.Add("BatchType", "pop2.Value");
            columnMapper.Add("BatchSegment", "pp6.Value");
            columnMapper.Add("BatchLot", "pp7.Value");
            columnMapper.Add("PowderCharge", "pop3.Value");
            columnMapper.Add("TestPlan", "cv.Name");
            columnMapper.Add("TestPlanRevision", "cv.Revision");
            columnMapper.Add("Material", "cvMI.code");
            columnMapper.Add("MaterialDescription", "cvMI.ItemDescription");
            columnMapper.Add("VaristorType", "mip1.Value");
            columnMapper.Add("VarDiameter", "mip2.Value");
            columnMapper.Add("VarHeight", "mip3.Value");
        }

        public void Dispose()
        {
            data = null;
        }

        public object GetCellValue(int x, int y)
        {
            int requestedPage = x / MainForm.PageSize;

            if (requestedPage != pageNumber || data == null)
            {
                pageNumber = requestedPage;
                int startIndex = pageNumber * MainForm.PageSize;
                data = GetData(startIndex, MainForm.PageSize);

            }
            if (x % MainForm.PageSize >= data.Count)
                return null;
            return data[x % MainForm.PageSize][y];
        }

        private List<object[]> GetData(int startIndex, int pageSize)
        {
            List<object[]> results = new List<object[]>();
            stopwatch.Restart();

            using (SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 150;
                cmd.CommandText = string.Format(query, startIndex, pageSize, whereConditions, sortMode);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                data = new List<object[]>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] row = new object[columnCount];
                        for (int i = 0; i < columnCount; i++)
                            row[i] = reader[i];

                        results.Add(row);
                    }
                }
                sqlConnection1.Close();
            }

            stopwatch.Stop();

            QueryExecuted.Invoke(stopwatch.Elapsed.TotalSeconds, results.Count);
            return results;
        }

        public object GetDataSource()
        {
            //List<DisplayResult> results;           
            //stopwatch.Restart();

            //using (DBModel model = new DBModel())
            //{
            //    results = model.Database.SqlQuery<DisplayResult>(string.Format(query, 0, 100, long.MaxValue)).ToList();
            //}
            //stopwatch.Stop();

            //QueryExecuted.Invoke(stopwatch.Elapsed.TotalSeconds, results.Count);
            //return results;

            throw new NotImplementedException();
        }

        public int GetRowCount()
        {
            pageNumber = -1;
            data = null;
            if (string.IsNullOrEmpty(queryCount))
                using (var db = new DBModel())
                {
                    return db.TestResults.Where(x => x.Valid).Count();
                }
            else
            {
                int size = 0;

                using (SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 150;
                    cmd.CommandText = string.Format(queryCount, whereConditions);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    data = new List<object[]>();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            size = reader.GetInt32(0);
                        }
                    }
                    sqlConnection1.Close();
                }

                return size;
            }
        }

        public void SetFilterAndSort(string sort, string filterColumn, string filterValue, bool ascending)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                string sortColumn = columnMapper.ContainsKey(sort) ? columnMapper[sort] : "t." + sort;
                sortMode = sortColumn + (ascending ? " asc, " : " desc, ");
            }
            else
            {
                sortMode = "";
            }
            if (!string.IsNullOrEmpty(filterColumn))
            {
                filterColumn = columnMapper.ContainsKey(filterColumn) ? columnMapper[filterColumn] : "t." + filterColumn;
                whereConditions = " and " + filterColumn + " LIKE '%" + filterValue + "%' ";
            }
            else
            {
                whereConditions = "";
            }
        }
    }
}
