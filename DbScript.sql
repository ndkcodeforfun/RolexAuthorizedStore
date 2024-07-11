create database RolexAuthorizedStoreDb
GO

use RolexAuthorizedStoreDb
GO

create table Customers (
	CustomerId INT IDENTITY(1,1) PRIMARY KEY,
	Email NVARCHAR(255) NOT NULL,
    HashedPassword NVARCHAR(255) NOT NULL,
    Status INT NOT NULL,
	Name NVARCHAR(255),
	Address NVARCHAR(255),
	Phone NVARCHAR(255),
	DoB Datetime,
	Avatar NVARCHAR(MAX)
)
GO

create table Categories (
	CategoryId INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL,
	Description NVARCHAR(MAX),
	Status INT,
)

create table Products (
	ProductId INT IDENTITY(1,1) PRIMARY KEY,
	CategoryId INT NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	Description NVARCHAR(MAX),
	Price decimal(13, 2) NOT NULL,
	Quantity INT NOT NULL CHECK (Quantity >= 0),
	Status INT NOT NULL,
	CONSTRAINT FK_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
)
GO

-- Create trigger to update status based on quantity
CREATE TRIGGER trg_UpdateProductStatus
ON Products
AFTER UPDATE
AS
BEGIN
    UPDATE Products
    SET Status = 0
    WHERE Quantity = 0 AND ProductId IN (SELECT ProductId FROM inserted);
END;
GO

create table ProductImages (
	ImageId INT IDENTITY(1,1) PRIMARY KEY,
	ProductId INT NOT NULL,
	ImagePath NVARCHAR(MAX),
	CONSTRAINT FK_ProductId_ProductImages FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
)
GO

create table CartItems (
	ItemId INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	ProductId INT NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT FK_CustomerId_CartItems FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
	CONSTRAINT FK_ProductId_CartItems FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
)
GO

create table Orders (
	OrderId INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	TotalPrice decimal(13,2),
	TransactionCode NVARCHAR(256),
	OrderDate DATETIME NOT NULL, 
	ExpiredDate DATETIME NOT NULL,
	Status INT NOT NULL,
	CONSTRAINT FK_CustomerId_Orders FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
)
GO

create table OrderDetails (
	OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	PricePerUnit decimal(13,2) NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT FK_ProductId_OrderDetails FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
	CONSTRAINT FK_OrderId_OrderDetails FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
)
GO

create table ChatRequest (
	MessageId INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	Type NVARCHAR(256) NOT NULL,
	Content NVARCHAR(MAX),
	SendTime DATETIME,
	Status INT NOT NULL
)
GO

create table Payments (
	PaymentId INT IDENTITY(1,1) PRIMARY KEY,
	PaymentMethod NVARCHAR(100) NOT NULL,
	BankCode NVARCHAR(MAX) NOT NULL,
	BankTranNo NVARCHAR(MAX) NOT NULL,
	CardType NVARCHAR(MAX) NOT NULL,
	PaymentInfo NVARCHAR(MAX),
	PayDate DATETIME,
	TransactionNo NVARCHAR(MAX) NOT NULL,
	TransactionStatus INT NOT NULL,
	PaymentAmount DECIMAL(13,2) NOT NULL,
	OrderId INT,
	CONSTRAINT FK_OrderId_Payments FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
)
GO