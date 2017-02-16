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
        object GetCellValue(int rowIndex, int columnIndex);
        int GetRowCount();

        event QueryExecuted QueryExecuted;        
    }

    public delegate void QueryExecuted(double time, int rowCount);

    public enum ProcessorType
    {
        EFProcessor = 0,
        PaggingProcessor = 1,
        ViewProcessor = 2,
        OneTableProcessor = 4
    }
}
