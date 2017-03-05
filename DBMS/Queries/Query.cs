using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Queries
{
    public class Query
    {
        public const string InitialQuery = @"SELECT distinct

ppo.ExternalId as Operation,

pop1.Value AS Batch,
pop2.Value AS BatchType,

pp6.Value AS BatchSegment,
pp7.Value AS BatchLot,

pop3.Value AS PowderCharge,

cv.Name AS TestPlan,
cv.Revision AS TestPlanRevision,

cvMI.code AS Material,
cvMI.ItemDescription AS MaterialDescription,

mip1.Value AS VaristorType,
mip2.Value AS VarDiameter,
mip3.Value AS VarHeight

      ,TestResult.[TestTs]
      ,TestResult.[ProductSerial]
      ,TestResult.[TestStatus]
      ,TestResult.[Class1]
      ,TestResult.[Class2]
      ,TestResult.[TestTemperature]
      ,TestResult.[DCTs]
      ,TestResult.[DCParam1]
      ,TestResult.[DCParam2]
      ,TestResult.[DCParam3]
      ,TestResult.[DCParam4]
      ,TestResult.[DCParam5]
      ,TestResult.[DCParam6]
      ,TestResult.[DCParam7]
      ,TestResult.[DCParam8]
      ,TestResult.[DCAlpha]
      ,TestResult.[DCStatus]
      ,TestResult.[ACTs]
      ,TestResult.[ACParam1]
      ,TestResult.[ACParam2]
      ,TestResult.[ACParam3]
      ,TestResult.[ACParam4]
      ,TestResult.[ACParam5]
      ,TestResult.[ACStatus]
      ,TestResult.[RestTs]
      ,TestResult.[RestParam1]
      ,TestResult.[RestParam2]
      ,TestResult.[RestParam3]
      ,TestResult.[RestParam4]
      ,TestResult.[RestParam5]
      ,TestResult.[RestParam6]
      ,TestResult.[RestStatus]
      ,TestResult.[ChargeTs]
      ,TestResult.[ChargeParam1]
      ,TestResult.[ChargeParam2]
      ,TestResult.[ChargeParam3]
      ,TestResult.[ChargeStatus]

FROM
	(
    select *
    FROM
		(
		    SELECT ROW_NUMBER() OVER (order by temp.ProductSerial) AS rn, temp.*
		    FROM
			(
			Select distinct top 100 percent TestResults.*
		    from
			    Products
                join ProductTypes on Products.ProductTypeId = ProductTypes.Id and ProductTypes.Name = 'Varistor'
				join TestResults on Products.SerialNumber = TestResults.ProductSerial and TestResults.Valid = 1,
                ProductionOrders,
				ProductionOrderProperties
		    WHERE
				ProductionOrderProperties.Name = 'MOBatch'
				and
			    Products.ProductionOrderId = ProductionOrders.Id
				and
				ProductionOrders.Id = ProductionOrderProperties.ProductionOrderId
			order by TestResults.ProductSerial asc
		    ) as temp
        ) as temp2
	where temp2.rn between {0} and {1}
    ) TestResult

	left join Products p on TestResult.ProductSerial = p.SerialNumber

	left join ProductionOrders po on po.Id = p.ProductionOrderId
    left join ProductionOrders ppo on ppo.Id = po.ParentId

--	GET TESTPLAN ID
	left join ProductionOrderProperties popTestPlan on po.Id = popTestPlan.ProductionOrderId  and popTestPlan.Name = 'TestPlanId'

--	GET DATA FROM PRODUCTION ORDER PROPERTY TABLE
	left join ProductionOrderProperties pop1 on po.ParentId = pop1.ProductionOrderId  and pop1.Name = 'MOBatch'
	left join ProductionOrderProperties pop2 on po.ParentId = pop2.ProductionOrderId  and pop2.Name = 'ProductionVersion'
    left join ProductionOrderProperties pop3 on po.ParentId = pop3.ProductionOrderId  and pop3.Name = 'PowderCharge'

--	GET DATA FROM PRODUCT PROPERTY TABLE
	left join ProductProperties pp6 on p.ParentId = pp6.ProductId and pp6.Name = 'SegmentName'
	left join ProductProperties pp7 on p.ParentId = pp7.ProductId and pp7.Name = 'LayerName'

--	GET CONFIGURATION VARIANT BASED ON FETCHED TESTPLAN ID
	left join ConfigurationVariants cv on CAST (popTestPlan.Value as bigint) = cv.Id
	left join MaterialItems cvMI on cv.MaterialItemId = cvMI.id

--	GET DATA FROM MATERIAL ITEM PROPERTY TABLE
	left join MaterialItemProperties mip1 on cvMI.id = mip1.MaterialItemId and mip1.MaterialClassId in (select Id from MaterialClasses where Name = 'Var_Typ')
	left join MaterialItemProperties mip2 on cvMI.id = mip2.MaterialItemId and mip2.MaterialClassId in (select Id from MaterialClasses where Name = 'Diameter')
	left join MaterialItemProperties mip3 on cvMI.id = mip3.MaterialItemId and mip3.MaterialClassId in (select Id from MaterialClasses where Name = 'Height')
";

        public const string PaggingQuery = @"with TestIds as (Select
t.Id as TestResultId,
ppo.Id as OperationId,
pop1.Id AS BatchId,
pop2.Id AS BatchTypeId,

pp6.Id AS BatchSegmentId,
pp7.Id AS BatchLotId,

pop3.Id AS PowderChargeId,

cv.Id AS TestPlanId,

cvMI.Id AS MaterialId,

mip1.Id AS VaristorTypeId,
mip2.Id AS VarDiameterId,
mip3.Id AS VarHeightId
		    from
			
				TestResults t 
				join Products prod on t.ProductSerial = prod.SerialNumber  
				
			--	join ProductTypes pt on pt.Id = prod.ProductTypeId and pt.Name = 'Varistor'				
				
				join ProductionOrders prodOrder on prodOrder.Id = prod.ProductionOrderId
				join ProductionOrders ppo on ppo.Id = prodOrder.ParentId
						
				left join ProductionOrderProperties TestPlan on prodOrder.Id = TestPlan.ProductionOrderId  and TestPlan.Name = 'TestPlanId'
				--	GET CONFIGURATION VARIANT BASED ON FETCHED TESTPLAN ID
				left join ConfigurationVariants cv on CAST (TestPlan.Value as bigint) = cv.Id
				left join MaterialItems cvMI on cv.MaterialItemId = cvMI.id

				--	GET DATA FROM MATERIAL ITEM PROPERTY TABLE
				left join MaterialItemPropertIes mip1 on cvMI.id = mip1.MaterialItemId and mip1.MaterialClassId in (select Id from MaterialClasses where Name = 'Var_Typ')
				left join MaterialItemPropertIes mip2 on cvMI.id = mip2.MaterialItemId and mip2.MaterialClassId in (select Id from MaterialClasses where Name = 'Diameter')
				left join MaterialItemProperties mip3 on cvMI.id = mip3.MaterialItemId and mip3.MaterialClassId in (select Id from MaterialClasses where Name = 'Height')
	
				join ProductionOrderProperties pop1 on pop1.ProductionOrderId = prodOrder.Id and pop1.Name = 'MOBatch'
				join ProductionOrderProperties pop2 on pop2.ProductionOrderId = prodOrder.Id and pop2.Name = 'ProductionVersion'
				join ProductionOrderProperties pop3 on pop3.ProductionOrderId = prodOrder.Id and pop3.Name = 'PowderCharge'
				left join ProductProperties pp6 on (pp6.ProductId =  prod.ParentId and pp6.Name = 'SegmentName')
				left join ProductProperties pp7 on (pp7.ProductId =  prod.ParentId and pp7.Name = 'LayerName')
          WHERE	
			t.valid = 1
            {2}
order by {3} t.Id 
OFFSET {0} ROWS
FETCH NEXT {1} ROWS ONLY)

Select
ppo.ExternalId as Operation,
pop1.Value AS Batch,
pop2.Value AS BatchType,

pp6.Value AS BatchSegment,
pp7.Value AS BatchLot,

pop3.Value AS PowderCharge,

cv.Name AS TestPlan,
cv.Revision AS TestPlanRevision,

cvMI.code AS Material,
cvMI.ItemDescription AS MaterialDescription,

mip1.Value AS VaristorType,
mip2.Value AS VarDiameter,
mip3.Value AS VarHeight
      ,t.[TestTs]
      ,t.[ProductSerial]
      ,t.[TestStatus]
      ,t.[Class1]
      ,t.[Class2]
      ,t.[TestTemperature]
      ,t.[DCTs]
      ,t.[DCParam1]
      ,t.[DCParam2]
      ,t.[DCParam3]
      ,t.[DCParam4]
      ,t.[DCParam5]
      ,t.[DCParam6]
      ,t.[DCParam7]
      ,t.[DCParam8]
      ,t.[DCAlpha]
      ,t.[DCStatus]
      ,t.[ACTs]
      ,t.[ACParam1]
      ,t.[ACParam2]
      ,t.[ACParam3]
      ,t.[ACParam4]
      ,t.[ACParam5]
      ,t.[ACStatus]
      ,t.[RestTs]
      ,t.[RestParam1]
      ,t.[RestParam2]
      ,t.[RestParam3]
      ,t.[RestParam4]
      ,t.[RestParam5]
      ,t.[RestParam6]
      ,t.[RestStatus]
      ,t.[ChargeTs]
      ,t.[ChargeParam1]
      ,t.[ChargeParam2]
      ,t.[ChargeParam3]
      ,t.[ChargeStatus]
		    from
			
				TestIds ids
				join TestResults t on t.Id = ids.TestResultId
				join ProductionOrders ppo on ppo.Id = ids.OperationId
				join ProductionOrderProperties pop1 on pop1.Id = ids.BatchId
				join ProductionOrderProperties pop2 on pop2.Id = ids.BatchTypeId
				join ProductionOrderProperties pop3 on pop3.Id = ids.PowderChargeId
				left join ProductProperties pp6 on (pp6.Id =  ids.BatchSegmentId)
				left join ProductProperties pp7 on (pp7.Id =  ids.BatchLotId )
			
				left join ConfigurationVariants cv on cv.Id = ids.TestPlanId
				left join MaterialItems cvMI on cvMI.Id = ids.MaterialId

				left join MaterialItemPropertIes mip1 on mip1.Id = ids.VaristorTypeId
				left join MaterialItemPropertIes mip2 on mip2.Id = ids.VarDiameterId
				left join MaterialItemProperties mip3 on mip3.Id = ids.VarHeightId
            
order {3} by t.Id

OPTION (FORCE ORDER)
;";

        public const string PaggingQueryCount = @"Select
count(*)
		    from
			
				TestResults t 
				join Products prod on t.ProductSerial = prod.SerialNumber  
				
				join ProductTypes pt on pt.Id = prod.ProductTypeId and pt.Name = 'Varistor'				
				
				join ProductionOrders prodOrder on prodOrder.Id = prod.ProductionOrderId
				join ProductionOrders ppo on ppo.Id = prodOrder.ParentId
				join ProductionOrderProperties pop1 on pop1.ProductionOrderId = prodOrder.Id and pop1.Name = 'MOBatch'
				join ProductionOrderProperties pop2 on pop2.ProductionOrderId = prodOrder.Id and pop2.Name = 'ProductionVersion'
				join ProductionOrderProperties pop3 on pop3.ProductionOrderId = prodOrder.Id and pop3.Name = 'PowderCharge'
				left join ProductProperties pp6 on (pp6.ProductId =  prod.ParentId and pp6.Name = 'SegmentName')
				left join ProductProperties pp7 on (pp7.ProductId =  prod.ParentId and pp7.Name = 'LayerName')
			
				left join ProductionOrderProperties TestPlan on prodOrder.Id = TestPlan.ProductionOrderId  and TestPlan.Name = 'TestPlanId'
				--	GET CONFIGURATION VARIANT BASED ON FETCHED TESTPLAN ID
				left join ConfigurationVariants cv on CAST (TestPlan.Value as bigint) = cv.Id
				left join MaterialItems cvMI on cv.MaterialItemId = cvMI.id

				--	GET DATA FROM MATERIAL ITEM PROPERTY TABLE
				left join MaterialItemPropertIes mip1 on cvMI.id = mip1.MaterialItemId and mip1.MaterialClassId in (select Id from MaterialClasses where Name = 'Var_Typ')
				left join MaterialItemPropertIes mip2 on cvMI.id = mip2.MaterialItemId and mip2.MaterialClassId in (select Id from MaterialClasses where Name = 'Diameter')
				left join MaterialItemProperties mip3 on cvMI.id = mip3.MaterialItemId and mip3.MaterialClassId in (select Id from MaterialClasses where Name = 'Height')
          WHERE
				
			t.valid = 1
            {0}
";
        public const string ViewCount = @"select count(*) from PaggingView p {0}";
        public const string OneTableSqlCount = @"select count(*) from DisplayResults p inner join TestResults r on p.TestResultId = r.Id {0}";
        public const string ViewQuery = @"with pg as (SELECT
    p.id
  FROM PaggingView p
    {2}
	order by {3} p.id
  offset {0} rows fetch next {1} rows only)

  select 
p.[Operation]
      ,p.[Batch]
      ,p.[BatchType]
      ,p.[BatchSegment]
      ,p.[BatchLot]
      ,p.[PowderCharge]
      ,p.[TestPlan]
      ,p.[TestPlanRevision]
      ,p.[Material]
      ,p.[MaterialDescription]
      ,p.[VaristorType]
      ,p.[VarDiameter]
      ,p.[VarHeight]
      ,p.[TestTs]
      ,p.[ProductSerial]
      ,p.[TestStatus]
      ,p.[Class1]
      ,p.[Class2]
      ,p.[TestTemperature]
      ,p.[DCTs]
      ,p.[DCParam1]
      ,p.[DCParam2]
      ,p.[DCParam3]
      ,p.[DCParam4]
      ,p.[DCParam5]
      ,p.[DCParam6]
      ,p.[DCParam7]
      ,p.[DCParam8]
      ,p.[DCAlpha]
      ,p.[DCStatus]
      ,p.[ACTs]
      ,p.[ACParam1]
      ,p.[ACParam2]
      ,p.[ACParam3]
      ,p.[ACParam4]
      ,p.[ACParam5]
      ,p.[ACStatus]
      ,p.[RestTs]
      ,p.[RestParam1]
      ,p.[RestParam2]
      ,p.[RestParam3]
      ,p.[RestParam4]
      ,p.[RestParam5]
      ,p.[RestParam6]
      ,p.[RestStatus]
      ,p.[ChargeTs]
      ,p.[ChargeParam1]
      ,p.[ChargeParam2]
      ,p.[ChargeParam3]
      ,p.[ChargeStatus]
from PaggingView p 
  inner join pg on pg.id = p.id
  order by {3} p.Id";

        public const string OneTableSqlQuery = @"with pg as (SELECT
    p.Id as pid, r.Id as rid
  from DisplayResults p inner join TestResults r on p.TestResultId = r.Id
    {2}
	order by {3} p.Id
  offset {0} rows fetch next {1} rows only)

  select 
       p.[Operation]
      ,p.[Batch]
      ,p.[BatchType]
      ,p.[BatchSegment]
      ,p.[BatchLot]
      ,p.[PowderCharge]
      ,p.[TestPlan]
      ,p.[TestPlanRevision]
      ,p.[Material]
      ,p.[MaterialDescription]
      ,p.[VaristorType]
      ,p.[VarDiameter]
      ,p.[VarHeight]
      ,r.[TestTs]
      ,r.[ProductSerial]
      ,r.[TestStatus]
      ,r.[Class1]
      ,r.[Class2]
      ,r.[TestTemperature]
      ,r.[DCTs]
      ,r.[DCParam1]
      ,r.[DCParam2]
      ,r.[DCParam3]
      ,r.[DCParam4]
      ,r.[DCParam5]
      ,r.[DCParam6]
      ,r.[DCParam7]
      ,r.[DCParam8]
      ,r.[DCAlpha]
      ,r.[DCStatus]
      ,r.[ACTs]
      ,r.[ACParam1]
      ,r.[ACParam2]
      ,r.[ACParam3]
      ,r.[ACParam4]
      ,r.[ACParam5]
      ,r.[ACStatus]
      ,r.[RestTs]
      ,r.[RestParam1]
      ,r.[RestParam2]
      ,r.[RestParam3]
      ,r.[RestParam4]
      ,r.[RestParam5]
      ,r.[RestParam6]
      ,r.[RestStatus]
      ,r.[ChargeTs]
      ,r.[ChargeParam1]
      ,r.[ChargeParam2]
      ,r.[ChargeParam3]
      ,r.[ChargeStatus]
from DisplayResults p,  TestResults r, pg
where
pg.pid = p.Id
and
pg.rid = r.Id
  order by {3} pg.pid";
    }    
}
