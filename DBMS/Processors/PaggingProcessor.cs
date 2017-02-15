using DBMS.DataLayer;
using DBMS.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Processors
{
    public class PaggingProcessor : IProcessor
    {
        private object[][] data;
        private int pageNumber;
        private string query;
        public PaggingProcessor(string query)
        {            
            this.query = query;
            pageNumber = -1;
        }

        public void Dispose()
        {
            data = null;
        }

        public object GetCellValue(int x, int y)
        {
            int requestedPage = x / MainForm.PageSize;

            if (requestedPage == pageNumber && data != null)
            {
                pageNumber = requestedPage;
                using (var db = new DBModel())
                {
                    data = db.Database.SqlQuery<object[]>(string.Format(query, pageNumber & MainForm.PageSize, MainForm.PageSize, long.MaxValue)).ToArray();
                }
            }
            return data[x % MainForm.PageSize][y];
        }

        public object GetDataSource()
        {
            throw new NotImplementedException();
        }
    }
}
