using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.ObjectModel
{
    public interface IProcessor : IDisposable
    {
        object GetDataSource();
        object GetCellValue(int x, int y);
    }
}
