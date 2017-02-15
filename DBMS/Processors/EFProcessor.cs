using DBMS.DataLayer;
using DBMS.ObjectModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Processors
{
    public class EFProcessor : IProcessor
    {
        private DBModel dbModel;
        private readonly int pageSize = 300;
        private int pageNumber = 0;
        private IQueryable<TestResult> query;
        private List<TestResult> page;

        public EFProcessor()
        {
            dbModel = new DBModel();
            /*order by product id*/
            query = dbModel.TestResults.AsQueryable().OrderBy(tr => tr.ProductSerial).Skip(pageNumber * pageSize).Take(pageSize);

            page = query.ToList();
        }

        public void Dispose()
        {
            dbModel.Dispose();
        }

        public object GetCellValue(int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public object GetDataSource()
        {
            //return dbModel.TestResults.Local.ToBindingList();
            throw new NotImplementedException("Use only in virtual mode with GetCellValue()!");
        }
    }
}
