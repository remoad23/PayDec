CREATE TABLE [dbo].[Purchase] (
    [Id] BIGINT IDENTITY NOT NULL,
    CONSTRAINT PK_PurchaseId PRIMARY KEY CLUSTERED (Id),
    [BoughtItemId] BIGINT  NOT NULL,
    [BuyerAdress] VARCHAR(500),
    CONSTRAINT fk_BoughtItem FOREIGN KEY (BoughtItemId)
    REFERENCES Item(Id) 
);

