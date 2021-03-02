CREATE TABLE [dbo].[SignOffList]
(
    [pk] INT NOT NULL ,
	[ProjectNO] NVARCHAR(50) NOT NULL , 
    [Code] NVARCHAR(50) NOT NULL, 
    [ProjectCode] NVARCHAR(50) NOT NULL, 
    [PurchaseNumber] NVARCHAR(50) NOT NULL, 
    [ApplicationSector] NVARCHAR(50) NOT NULL, 
    [ApplicationDate] DATETIME NOT NULL, 
    [PurchaseDate] DATETIME NOT NULL, 
    [NeedDate] DATETIME NOT NULL, 
    [Receiver] NVARCHAR(50) NOT NULL, 
    [TradingLocation] NVARCHAR(50) NOT NULL, 
    [Content] NVARCHAR(50) NOT NULL, 
    [Brand] NVARCHAR(50) NOT NULL, 
    [Check] BIT NOT NULL, 
    CONSTRAINT [PK_SignOffList] PRIMARY KEY ([pk])
)
