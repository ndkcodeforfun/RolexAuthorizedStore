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
	Status INT NOT NULL,
	CONSTRAINT FK_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
)

create table ProductImages (
	ImageId INT IDENTITY(1,1) PRIMARY KEY,
	ProductId INT NOT NULL,
	ImagePath NVARCHAR(MAX),
	CONSTRAINT FK_ProductId_ProductImages FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
)

create table Carts (
	CartId INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	CONSTRAINT FK_CustomerId_Carts FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
)

create table CartItems (
	ItemId INT IDENTITY(1,1) PRIMARY KEY,
	CartId INT NOT NULL,
	ProductId INT NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT FK_CartId FOREIGN KEY (CartId) REFERENCES Carts(CartId),
	CONSTRAINT FK_ProductId_CartItems FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
)

create table Orders (
	OrderId INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	TotalPrice decimal(13,2),
	Status INT NOT NULL,
	CONSTRAINT FK_CustomerId_Orders FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
)

create table OrderDetails (
	OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	PricePerUnit decimal(13,2) NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT FK_ProductId_OrderDetails FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
	CONSTRAINT FK_OrderId_OrderDetails FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
)