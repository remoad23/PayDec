CREATE TABLE [dbo].[Purchase] (
    [Id] BIGINT IDENTITY(1,1) NOT NULL,
    CONSTRAINT PK_PurchaseId PRIMARY KEY CLUSTERED (Id),
    [BoughtItemId] BIGINT  NOT NULL,
    [Amount] INT  NOT NULL,
    [Price] INT  NOT NULL,
    [BuyerAdress] VARCHAR(500),
    CONSTRAINT fk_BoughtItem FOREIGN KEY (BoughtItemId)
    REFERENCES Item(Id) 
);

