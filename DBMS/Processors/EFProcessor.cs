using DBMS.DataLayer;
using DBMS.ObjectModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS.Processors
{
    public class EFProcessor : IProcessor
    {
        private DBModel dbModel;
        private IQueryable<DisplayResult> query;
        private int pageSize = 400;
        private DataGridView grid;
        public event QueryExecuted QueryExecuted;
        private string[] columnNames;
        private Stopwatch stopwatch;
        private Dictionary<int, DisplayResult[]> cache;

        public EFProcessor(DataGridView grid)
        {
            dbModel = new DBModel();
            this.grid = grid;
            columnNames = new string[this.grid.Columns.Count];
            stopwatch = new Stopwatch();
            cache = new Dictionary<int, DisplayResult[]>(3);

            for (int i = 0; i < this.grid.Columns.Count; i++)
            {
                columnNames[i] = this.grid.Columns[i].Name;
            }

            /*order by product id*/
            //query = dbModel.TestResults.AsQueryable().OrderBy(tr => tr.ProductSerial)//.Skip(pageNumber * pageSize).Take(pageSize);

            //query = dbModel.Products
            //    .Where(p => p.Parent != null && p.Parent.ProductProperties.Any(pp => pp.Name == "SegmentName") && p.Parent.ProductProperties.Any(pp => pp.Name == "LayerName"))
            //    .Where(p => p.ProductionOrder.ProductionOrderProperties.Any(pop => pop.Name == "MOBatch") 
            //        && p.ProductionOrder.ProductionOrderProperties.Any(pop => pop.Name == "ProductionVersion")
            //        && p.ProductionOrder.ProductionOrderProperties.Any(pop => pop.Name == "PowderCharge"))
            //    .Join(dbModel.TestResults, p => p.SerialNumber, tr => tr.ProductSerial
            //        , (p, tr) => new { Product = p, TestResult = tr, TestPlanId = p.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "TestPlanId") })
            //    .Where(a => a.TestResult.Valid)
            //    .OrderBy(a => a.Product.Id)
            //    .Select(a => new DisplayResult {
            //        Operation = a.Product.ProductionOrder.Parent.ExternalId,
            //        Batch = a.Product.ProductionOrder.ProductionOrderProperties.First(pop => pop.Name == "MOBatch").Value,
            //        BatchType = a.Product.ProductionOrder.ProductionOrderProperties.First(pop => pop.Name == "ProductionVersion").Value,
            //        BatchSegment = a.Product.Parent.ProductProperties.First(pp => pp.Name == "SegmentName").Value,
            //        BatchLot = a.Product.Parent.ProductProperties.First(pp => pp.Name == "LayerName").Value,
            //        PowderCharge = a.Product.ProductionOrder.ProductionOrderProperties.First(pop => pop.Name == "PowderCharge").Value,
            //        TestPlan = 
            //    })
            //    .Skip(pageNumber * pageSize)
            //    .Take(pageSize)
            //    .AsQueryable();


            var validTestResults = dbModel.TestResults.Where(tr => tr.Valid);
            var varistorProducts = dbModel.Products.Where(p => p.ProductType.Name == "Varistor");

            var q1 = varistorProducts.Join(validTestResults, p => p.SerialNumber, tr => tr.ProductSerial
                , (p, tr) => new { Product = p, TestResult = tr, TestPlanId = p.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "TestPlanId") });
            
            var q2 = q1.Select(a => new { Product = a.Product
                , TestResult = a.TestResult
                , TestPlanId = a.TestPlanId
                , ConfigurationVariant = (a.TestPlanId == null) ? null : dbModel.ConfigurationVariants.FirstOrDefault(c => c.Id.ToString() == a.TestPlanId.Value)
                , Batch = a.Product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "MOBatch").Value
                , BatchType = a.Product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "ProductionVersion").Value
                , BatchSegment = a.Product.Parent.ProductProperties.FirstOrDefault(pp => pp.Name == "SegmentName").Value
                , BatchLot = a.Product.Parent.ProductProperties.FirstOrDefault(pp => pp.Name == "LayerName").Value
                , PowderCharge = a.Product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "PowderCharge").Value
            });

            var q3 = q2.Select(a => new { Product = a.Product
                , TestResult = a.TestResult
                , TestPlanId = a.TestPlanId
                , ConfigurationVariant = a.ConfigurationVariant
                , Batch = a.Batch
                , BatchType = a.BatchType
                , BatchSegment = a.BatchSegment
                , BatchLot = a.BatchLot
                , PowderCharge = a.PowderCharge
                , mip1 = (a.ConfigurationVariant == null || a.ConfigurationVariant.MaterialItem == null) ? null : a.ConfigurationVariant.MaterialItem.MaterialItemProperties.FirstOrDefault(p => p.MaterialClass.Name == "Var_Typ")
                , mip2 = (a.ConfigurationVariant == null || a.ConfigurationVariant.MaterialItem == null) ? null : a.ConfigurationVariant.MaterialItem.MaterialItemProperties.FirstOrDefault(p => p.MaterialClass.Name == "Diameter")
                , mip3 = (a.ConfigurationVariant == null || a.ConfigurationVariant.MaterialItem == null) ? null : a.ConfigurationVariant.MaterialItem.MaterialItemProperties.FirstOrDefault(p => p.MaterialClass.Name == "Height")
            });

            query = q3.OrderBy(a => a.Product.Id).Select(a => new DisplayResult{ Operation = a.Product.ProductionOrder.Parent.ExternalId
                , Batch = a.Batch
                , BatchType = a.BatchType
                , BatchSegment = a.BatchSegment
                , BatchLot = a.BatchLot
                , PowderCharge = a.PowderCharge
                , TestPlan = a.ConfigurationVariant == null ? null : a.ConfigurationVariant.Name
                , TestPlanRevision = a.ConfigurationVariant == null ? null : a.ConfigurationVariant.Revision
                , Material = a.ConfigurationVariant == null ? null : a.ConfigurationVariant.MaterialItem.Code
                , MaterialDescription = a.ConfigurationVariant == null ? null : a.ConfigurationVariant.MaterialItem.ItemDescription
                , VaristorType = a.mip1 == null ? null : a.mip1.Value
                , VarDiameter = a.mip2 == null ? null : a.mip2.Value
                , VarHeight = a.mip3 == null ? null : a.mip2.Value
                , TestResult = a.TestResult});

            //query = dbModel.ProductionOrders
            //    .Where(po => po.ProductionOrderProperties.Count == 0 || po.ProductionOrderProperties.Any(pop => pop.Name == "TestPlanId"))
            //    .Select(po => new { prodOrder = po, TestPlan = po.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "TestPlanId") })
            //    .Select(a => new { prodOrder = a.prodOrder, TestPlan = a.TestPlan, cv = dbModel.ConfigurationVariants.FirstOrDefault(c => c.Id.ToString() == a.TestPlan.Value) })
            //    .Select(a => new { prodOrder = a.prodOrder, TestPlan = a.TestPlan, cv = a.cv
            //        , mip1 = (a.cv == null || a.cv.MaterialItem == null) ? null : a.cv.MaterialItem.MaterialItemProperties.FirstOrDefault(p => p.MaterialClass.Name == "Var_Typ")
            //        , mip2 = (a.cv == null || a.cv.MaterialItem == null) ? null : a.cv.MaterialItem.MaterialItemProperties.FirstOrDefault(p => p.MaterialClass.Name == "Diameter")
            //        , mip3 = (a.cv == null || a.cv.MaterialItem == null) ? null : a.cv.MaterialItem.MaterialItemProperties.FirstOrDefault(p => p.MaterialClass.Name == "Height") })
            //    .OrderBy(a => a.prodOrder.Id)
            //    .Skip(pageNumber * pageSize)
            //    .Take(pageSize)
            //    .AsQueryable();

            //cv ima referencu na cvMI!!!=

            //    .Where(a => a.Product.ProductionOrder.ProductionOrderProperties.Count == 0 || a.Product.ProductionOrder.ProductionOrderProperties.)
            //query = dbModel.ProductionOrders.Select Where(po => po.ProductionOrderProperties.Count == 0 || po.ProductionOrderProperties.)
            //.OrderBy(a => a.Product.Id)
            //.Skip(pageNumber * pageSize)
            //.Take(pageSize)
            //.AsQueryable();

            //query = from testResult in dbModel.TestResults
            //        from product in dbModel.Products
            //        from productProperty1 in dbModel.ProductProperties
            //        from productProperty2 in dbModel.ProductProperties                    
            //        from productionOrder in dbModel.ProductionOrders
            //        from testPlan in dbModel.ProductionOrderProperties
            //        from productionOrderProperty1 in dbModel.ProductionOrderProperties
            //        from productionOrderProperty2 in dbModel.ProductionOrderProperties
            //        from productionOrderProperty3 in dbModel.ProductionOrderProperties
            //        from configurationVariant in dbModel.ConfigurationVariants
            //        from materialItem in dbModel.MaterialItems
            //        from materialItemProperty1 in dbModel.MaterialItemProperties
            //        from materialItemProperty2 in dbModel.MaterialItemProperties
            //        from materialItemProperty3 in dbModel.MaterialItemProperties
            //        where
            //        product.SerialNumber == testResult.ProductSerial
            //        &&
            //        testResult.Valid
            //        &&
            //        testPlan.ProductionOrderId == product.ProductionOrderId && 

            //        (testPlan == null || testPlan.Value == configurationVariant.Id.ToString()
            //        &&
            //        (configurationVariant == null || configurationVariant.MaterialItemId == materialItem.Id)
            //        &&
            //        product.SerialNumber == testResult.ProductSerial &&
            //        testResult.Valid &&
            //        product.ProductionOrderId == productionOrder.Id &&
            //        productionOrderProperty1.ProductionOrderId == productionOrder.Id &&
            //        productionOrderProperty2.ProductionOrderId == productionOrder.Id &&
            //        productionOrderProperty3.ProductionOrderId == productionOrder.Id &&

            //        ;

            //FetchPage(pageNumber);
        }

        public void Dispose()
        {
            dbModel.Dispose();
        }

        public object GetCellValue(int rowIndex, int columnIndex)
        {
            int requestedPageNumber = rowIndex / pageSize;

            if(!cache.Keys.Contains(requestedPageNumber))
            {
                FetchPage(requestedPageNumber);
            }

            DisplayResult record = cache[requestedPageNumber][rowIndex % pageSize];

            if (columnIndex > 12)
                return record.TestResult.GetType().GetProperty(columnNames[columnIndex]).GetValue(record.TestResult, null);
            else
                return record.GetType().GetProperty(columnNames[columnIndex]).GetValue(record, null);
        }

        private void FetchPage(int requestedPageNumber)
        {
            stopwatch.Restart();

            if(cache.Count >= 3)
            {
                //delete some page
                int difference = 0;
                int pageToRemove = -1;

                foreach (var key in cache.Keys)
                {
                    if (Math.Abs(key - requestedPageNumber) > difference)
                    {
                        pageToRemove = key;
                        difference = Math.Abs(key - requestedPageNumber);
                    }
                }

                cache.Remove(pageToRemove);
            }
            
            var page = query.Skip(requestedPageNumber * pageSize).Take(pageSize).ToArray();

            cache.Add(requestedPageNumber, page);

            stopwatch.Stop();
            QueryExecuted.Invoke(stopwatch.Elapsed.TotalSeconds, page.Length);
        }

        public object GetDataSource()
        {
            //return dbModel.TestResults.Local.ToBindingList();
            throw new NotImplementedException("Use only in virtual mode with GetCellValue()!");
        }

        public void SetFilterAndSort(string sort, string filterColumn, string filterValue, bool ascending)
        {
            throw new NotImplementedException();
        }

        public int GetRowCount()
        {
            return query.Count();
        }
    }
}
