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

        public EFProcessor()
        {
            dbModel = new DBModel();
        }

        public void Dispose()
        {
            dbModel.Dispose();
        }

        public object GetCellValue(int x, int y)
        {
            throw new NotImplementedException();
        }

        public object GetDataSource()
        {
            dbModel.TestResults.LoadAsync();
            return dbModel.TestResults.Local.ToBindingList();
        }

        public int GetRowCount()
        {
            throw new NotImplementedException();
        }
    }
}
