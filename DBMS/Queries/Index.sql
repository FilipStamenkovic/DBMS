CREATE INDEX IX_Product_SerialNumber ON Products (SerialNumber); 

CREATE INDEX IX_TestResult_ProductSerial on TestResults (ProductSerial);

CREATE INDEX IX_ProductProperty_Product ON ProductProperties (ProductId); 

CREATE INDEX IX_ProductionOrderProperty_ProductionOrder ON ProductionOrderProperties (ProductionOrderId);