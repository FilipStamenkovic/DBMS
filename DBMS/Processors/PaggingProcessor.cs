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

namespace DBMS.Processors
{
    public class PaggingProcessor : IProcessor
    {
        private List<object[]> data;
        private int pageNumber;
        private string query;
        private int columnCount;
        private Stopwatch stopwatch;

        public event QueryExecuted QueryExecuted;

        public PaggingProcessor(string query, int columnCount)
        {
            this.query = query;
            pageNumber = -1;
            this.columnCount = columnCount;
            stopwatch = new Stopwatch();
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
                stopwatch.Restart();
                using (SqlConnection sqlConnection1 = new SqlConnection("data source=localhost;initial catalog=DB;integrated security=True;MultipleActiveResultSets=True;"))
                {
                    SqlCommand cmd = new SqlCommand();

                    int startIndex = pageNumber * MainForm.PageSize;

                    cmd.CommandText = string.Format(query, startIndex, startIndex + MainForm.PageSize, long.MaxValue);
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
                                row[i] = reader[i + 2];

                            data.Add(row);
                        }
                    }
                    sqlConnection1.Close();
                }

                stopwatch.Stop();

                double time = stopwatch.Elapsed.TotalSeconds;

                QueryExecuted.Invoke(time, data.Count);
            }
            if (x % MainForm.PageSize >= data.Count)
                return null;
            return data[x % MainForm.PageSize][y];
        }

        public object GetDataSource()
        {
            throw new NotImplementedException();
        }

        public int GetRowCount()
        {
            using (var db = new DBModel())
            {
                return db.TestResults.Where(x => x.Valid).Count();
            }
        }
    }
}
