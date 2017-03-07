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
/// <summary>
/// 
/// </summary>
namespace DBMS.Processors
{
    public class EFProcessor : IProcessor
    {
        private DBModel dbModel;
        private IQueryable<object> query;
        private int pageSize = 400;
        private DataGridView grid;
        public event QueryExecuted QueryExecuted;
        private string[] columnNames;
        private Stopwatch stopwatch;
        private Dictionary<int, object[]> cache;

        public EFProcessor(DataGridView grid)
        {
            dbModel = new DBModel("name=" + MainForm.ConnectionName);
            this.grid = grid;
            columnNames = new string[this.grid.Columns.Count];
            stopwatch = new Stopwatch();
            cache = new Dictionary<int, object[]>(3);

            for (int i = 0; i < this.grid.Columns.Count; i++)
            {
                columnNames[i] = this.grid.Columns[i].Name;
            }

            MainForm.ConnectionChaged += MainForm_ConnectionChaged;
            MainForm.UseDisplayResultsChanged += MainForm_UseDisplayResultsChanged;
            BuildQuery();
        }

        private void MainForm_UseDisplayResultsChanged(bool usePreJoinedTable)
        {
            BuildQuery();
        }

        private void BuildQuery()
        {
            if (MainForm.UseDisplayResultsTable)
            {
                query = dbModel.DisplayResults.Include(r => r.TestResult).OrderBy(d => d.Id).AsQueryable();
            }
            else
            {
                var validTestResults = dbModel.TestResults.Where(tr => tr.Valid);
                var varistorProducts = dbModel.Products.Where(p => p.ProductType.Name == "Varistor");

                var q1 = varistorProducts.Join(validTestResults, p => p.SerialNumber, tr => tr.ProductSerial
                    , (p, tr) => new { Product = p, TestResult = tr, TestPlanId = p.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "TestPlanId") });

                var q2 = q1.Select(a => new {
                    Product = a.Product
                    , TestResult = a.TestResult
                    , TestPlanId = a.TestPlanId
                    , ConfigurationVariant = (a.TestPlanId == null) ? null : dbModel.ConfigurationVariants.FirstOrDefault(c => c.Id.ToString() == a.TestPlanId.Value)
                    , Batch = a.Product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "MOBatch").Value
                    , BatchType = a.Product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "ProductionVersion").Value
                    , BatchSegment = a.Product.Parent.ProductProperties.FirstOrDefault(pp => pp.Name == "SegmentName").Value
                    , BatchLot = a.Product.Parent.ProductProperties.FirstOrDefault(pp => pp.Name == "LayerName").Value
                    , PowderCharge = a.Product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop => pop.Name == "PowderCharge").Value
                });

                var q3 = q2.Select(a => new {
                    Product = a.Product
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

                query = q3.OrderBy(a => a.Product.Id).Select(a => new
                {
                    Operation = a.Product.ProductionOrder.Parent.ExternalId
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
                    , TestResult = a.TestResult
                });
            }

            //LINQ Query syntax approach
            //query = from testResult in dbModel.TestResults
            //        join product in dbModel.Products on testResult.ProductSerial equals product.SerialNumber
            //        from testPlan in dbModel.ProductionOrderProperties.Where(pop => product.ProductionOrderId == pop.ProductionOrderId && pop.Name == "TestPlanId").DefaultIfEmpty()
            //        from confVar in dbModel.ConfigurationVariants.Where(cv => testPlan != null && cv.Id.ToString() == testPlan.Value).DefaultIfEmpty()
            //        where
            //            //product.ProductType.Name == "Varistor" &&
            //            testResult.Valid
            //        orderby
            //            testResult.Id ascending
            //        select new
            //        {
            //             Operation = product.ProductionOrder.Parent.ExternalId
            //             , Batch = product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop1 => pop1.Name == "MOBatch")
            //             , BatchType = product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop1 => pop1.Name == "ProductionVersion")
            //             , BatchSegment = product.Parent.ProductProperties.FirstOrDefault(pp6 => pp6.Name == "SegmentName")
            //             , BatchLot = product.Parent.ProductProperties.FirstOrDefault(pp6 => pp6.Name == "LayerName")
            //             , PowderCharge = product.ProductionOrder.ProductionOrderProperties.FirstOrDefault(pop3 => pop3.Name == "PowderCharge")
            //             , TestPlan = confVar != null ? confVar.Name : null
            //             , TestPlanRevision = confVar != null ? confVar.Revision : null
            //             , Material = confVar != null ? confVar.MaterialItem.Code : null
            //             , MaterialDescription = confVar != null ? confVar.MaterialItem.ItemDescription : null
            //             , VaristorType = confVar != null ? confVar.MaterialItem.MaterialItemProperties.FirstOrDefault(mip1 => mip1.MaterialClass.Name == "Var_Typ") : null
            //             , VarDiameter = confVar != null ? confVar.MaterialItem.MaterialItemProperties.FirstOrDefault(mip2 => mip2.MaterialClass.Name == "Diameter") : null
            //             , VarHeight = confVar != null ? confVar.MaterialItem.MaterialItemProperties.FirstOrDefault(mip3 => mip3.MaterialClass.Name == "Height") : null
            //             , TestResult = testResult
            //        };

        }

        private void MainForm_ConnectionChaged(string connectionName)
        {
            if (dbModel != null)
                dbModel.Dispose();

            dbModel = new DBModel("name=" + MainForm.ConnectionName);
            BuildQuery();
        }

        public void Dispose()
        {
            dbModel.Dispose();
        }

        public object GetCellValue(int rowIndex, int columnIndex)
        {
            int requestedPageNumber = rowIndex / pageSize;

            if (!cache.Keys.Contains(requestedPageNumber))
            {
                FetchPage(requestedPageNumber);
            }

            object record = cache[requestedPageNumber][rowIndex % pageSize];

            if (columnIndex > 12)
            {
                TestResult testRes = (TestResult)record.GetType().GetProperty("TestResult").GetValue(record, null);
                return testRes.GetType().GetProperty(columnNames[columnIndex]).GetValue(testRes, null);
            }
            else
                return record.GetType().GetProperty(columnNames[columnIndex]).GetValue(record, null);
        }

        private void FetchPage(int requestedPageNumber)
        {
            stopwatch.Restart();

            if (cache.Count >= 3)
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
            //throw new NotImplementedException();
        }

        public int GetRowCount()
        {
            return query.Count();
        }
    }
}
