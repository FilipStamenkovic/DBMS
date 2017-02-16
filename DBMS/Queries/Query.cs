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

      ,TestResult.*

FROM
	(
    select *
    FROM
		(
		    SELECT ROW_NUMBER() OVER (order by temp.ProductSerial) AS rn, temp.*
		    FROM
			(
			Select distinct top {2} TestResults.*
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

order by TestResult.Id";
    }
}
