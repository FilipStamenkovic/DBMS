using DBMS.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Processors
{
    public class EFProcessor : IProcessor
    {
        public void Dispose()
        {

        }

        public object GetCellValue(int x, int y)
        {
            throw new NotImplementedException();
        }

        public object GetDataSource()
        {
            //napisi upit za entity framework
            return null;
        }
    }
}
