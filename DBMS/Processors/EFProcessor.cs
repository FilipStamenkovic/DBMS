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
        private IQueryable<object> query;
        private List<object> page;

        public EFProcessor()
        {
            dbModel = new DBModel();
            /*order by product id*/
            //query = dbModel.TestResults.AsQueryable().OrderBy(tr => tr.ProductSerial)//.Skip(pageNumber * pageSize).Take(pageSize);

            //query = dbModel.Products
            //    .Join(dbModel.TestResults, p => p.SerialNumber, tr => tr.ProductSerial, (p, tr) => new { Product = p, TestResult = tr })
            //    .Join(dbModel.ProductionOrders, a => )
                
            //    .Where(a => a.Product.ProductionOrder.ProductionOrderProperties.Count == 0 || a.Product.ProductionOrder.ProductionOrderProperties.)
            //query = dbModel.ProductionOrders.Select Where(po => po.ProductionOrderProperties.Count == 0 || po.ProductionOrderProperties.)
            //.OrderBy(a => a.Product.Id)
            //.Skip(pageNumber * pageSize)
            //.Take(pageSize)
            //.AsQueryable();

            //query = from testResult in dbModel.TestResults
            //        from product in dbModel.Products
            //        from productProperty in dbModel.ProductProperties
            //        from productionOrder in dbModel.ProductionOrders
            //        from testPlan in dbModel.ProductionOrderProperties
            //        from productionOrderProperty1 in dbModel.ProductionOrderProperties
            //        from productionOrderProperty2 in dbModel.ProductionOrderProperties
            //        from productionOrderProperty3 in dbModel.ProductionOrderProperties
            //        from productType in dbModel.ProductTypes
            //        from materialItem in dbModel.MaterialItems
            //        from materialItemProperty in dbModel.MaterialItemProperties
            //        from configurationVariant in dbModel.ConfigurationVariants
            //        from materialClass in dbModel.MaterialClasses
            //        where
            //        (productionOrder.ProductionOrderProperties.Count == 0 || (testPlan.ProductionOrderId == productionOrder.Id && testPlan.Name == "TestPlanId"))
            //        &&
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

        public int GetRowCount()
        {
            throw new NotImplementedException();
        }
    }
}
