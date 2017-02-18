use DB
GO
create view

[dbo].[PaggingView]

as

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
mip3.Value AS VarHeight,
  t.*
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

GO

use DB_Indexed
go

create view

[dbo].[pagging_view] with schemabinding

as

select 
d.Id,
d.Operation,

d.BatchSegment,
d.BatchLot,

d.PowderCharge,

d.TestPlan,
d.TestPlanRevision,

d.Material,
d.MaterialDescription,

d.VaristorType,
d.VarDiameter,
d.VarHeight,
d.TestResultId
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

from dbo.DisplayResults d
inner join dbo.TestResults t on t.Id = d.TestResultId

GO

 CREATE UNIQUE CLUSTERED INDEX Index_testResultId ON [pagging_view] (Id, TestResultId)
 
 GO

--Create table DisplayResults
select ROW_NUMBER() OVER(ORDER BY pv.Id ASC) as Id, pv.Operation, pv.Batch, pv.BatchType, pv.BatchSegment, pv.BatchLot, pv.PowderCharge, pv.TestPlan
	, pv.TestPlanRevision, pv.Material, pv.MaterialDescription, pv.VaristorType, pv.VarDiameter, pv.VarHeight, pv.Id as TestResultId
into 
DisplayResults 
from 
PaggingView pv;
----------------------------
