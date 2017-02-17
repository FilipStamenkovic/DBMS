using DBMS.ObjectModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Processors
{
    public class ViewProcessor : IProcessor
    {
        private List<object[]> data;
        private int pageNumber;
        private string query;
        private string queryCount;
        private int columnCount;
        private Stopwatch stopwatch;
        private string whereConditions;
        private string sortMode;

        public ViewProcessor(string query, int columnCount, string queryCount)
        {
            this.query = query;
            this.columnCount = columnCount;
            this.queryCount = queryCount;
            stopwatch = new Stopwatch();

        }
        public event QueryExecuted QueryExecuted;

        public void Dispose()
        {
            data = null;
        }

        public object GetCellValue(int rowIndex, int columnIndex)
        {
            int requestedPage = rowIndex / MainForm.PageSize;

            if (requestedPage != pageNumber || data == null)
            {
                pageNumber = requestedPage;
                int startIndex = pageNumber * MainForm.PageSize;
                data = GetData(startIndex, MainForm.PageSize);

            }
            if (rowIndex % MainForm.PageSize >= data.Count)
                return null;
            return data[rowIndex % MainForm.PageSize][columnIndex];
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
            throw new NotImplementedException();
        }

        public int GetRowCount()
        {
            int size = 0;
            pageNumber = -1;
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

        public void SetFilterAndSort(string sort, string filterColumn, string filterValue, bool ascending)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                string sortColumn = "p." + sort;
                sortMode = sortColumn + (ascending ? " asc, " : " desc, ");
            }
            else
            {
                sortMode = "";
            }
            if (!string.IsNullOrEmpty(filterColumn))
            {
                filterColumn = "p." + filterColumn;
                whereConditions = "where " + filterColumn + " LIKE '%" + filterValue + "%' ";
            }
            else
            {
                whereConditions = "";
            }
        }
    }
}
