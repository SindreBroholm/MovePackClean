CREATE TABLE Customers
(
	[CustomerId] int NOT NULL IDENTITY(1,1),
	[Name] nvarchar(max) NOT NULL,
	[PhoneNumber] nvarchar(max) NOT NULL,
	[Email] nvarchar(max) NOT NULL,
	PRIMARY KEY ([CustomerId])
);
GO
CREATE TABLE ServiceTypes
(
	[TypeId] int NOT NULL IDENTITY(1,1),
	[Name] varchar(max) NOT NULL,
	[Active] bit NOT NULL DEFAULT 1,
	PRIMARY KEY ([TypeId])
);
GO
CREATE TABLE OrderDetails
(
	[OrderDetailId] int NOT NULL IDENTITY(1,1),
	[ServiceType] int NOT NULL,
	[PrimaryAddress] nvarchar(max) NOT NULL,
	[SecondaryAddress] nvarchar(max) NULL,
	[StartTime] datetime NOT NULL,
	[EndTime] datetime NULL,
	[Details] nvarchar(max) NULL,
	PRIMARY KEY ([OrderDetailId]),
	FOREIGN KEY ([ServiceType]) REFERENCES ServiceTypes([TypeId]) 
);
GO
CREATE TABLE Orders
(
	[OrderId] int NOT NULL IDENTITY(1,1),
	[CustomerId] int NOT NULL,
	[OrderDetailId] int NOT NULL,
	PRIMARY KEY ([OrderId]),
	FOREIGN KEY ([CustomerId]) REFERENCES Customers([CustomerId]),
	FOREIGN KEY ([OrderDetailId]) REFERENCES OrderDetails([OrderDetailId]) ON DELETE CASCADE
);
GO
INSERT INTO ServiceTypes ([Name]) VALUES ('Moving'), ('Packing'), ('Cleaning');