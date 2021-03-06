

INSERT INTO [Order] (OrderNo, Date, RecordedDate, SenderGroup, SenderCompany, SenderDepartment, ReceiverGroup, ReceiverCompany, ReceiverDepartment, CustomerGroup, CustomerCompany, CustomerDepartment, PaymentType, PickupCity, PickupRegion, PickupArea, DropoffCity, DropoffRegion, DropoffArea, DropoffAddress, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight, PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, VehicleAdvance) 
VALUES ('OrderNo', 'Date', GETDATE(), 'SenderGroup', 'SenderCompany', 'SenderDepartment', 'ReceiverGroup', 'ReceiverCompany', 'ReceiverDepartment', 'CustomerGroup', 'CustomerCompany', 'CustomerDepartment', 'PaymentType', 'PickupCity', 'PickupRegion', 'PickupArea', 'DropoffCity', 'DropoffRegion', 'DropoffArea', 'DropoffAddress', 'ClearingAgent', AdditionalWeight, ActualWeight, BiltyFreight, Freight, PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, VehicleAdvance);



INSERT INTO OrderConsignment (OrderID, ContainerType, ContainerNo, ContainerWeight, EmptyContainerPickLocation, EmptyContainerDropLocation, VesselName, Remarks) 
VALUES (OrderID, 'ContainerType', 'ContainerNo', ContainerWeight, 'EmptyContainerPickLocation', 'EmptyContainerDropLocation', 'VesselName', 'Remarks');

INSERT INTO OrderVehicle (OrderID, VehicleType, VehicleRegNo, VehicleContactNo, BrokerID, DriverName, FatherName, DriverNIC, DriverLicence, DriverCellNo) 
VALUES (OrderID, 'VehicleType', 'VehicleRegNo', VehicleContactNo, BrokerID, 'DriverName', 'FatherName', DriverNIC, DriverLicence, DriverCellNo);

INSERT INTO OrderProduct (OrderID, PackageType, Item, Qty, TotalWeight) VALUES (OrderID, 'PackageType', 'Item', Qty, TotalWeight);

INSERT INTO OrderConsignmentReceiver (OrderID, ReceivedBy, ReceivedDateTime) VALUES (OrderID, 'ReceivedBy', 'ReceivedDateTime');

INSERT INTO OrderDocumentReceiving (OrderID, DocumentType, DocumentNo, DocumentName, DocumentPath) VALUES (OrderID, 'DocumentType', 'DocumentNo', 'DocumentName', 'DocumentPath');


INSERT INTO OrderDamage (OrderID, ItemName, DamageType, DamageCost, DamageCause, DamageDocumentName, DamageDocumentPath) VALUES (OrderID, 'ItemName', 'DamageType', DamageCost, 'DamageCause', 'DamageDocumentName', 'DamageDocumentPath');