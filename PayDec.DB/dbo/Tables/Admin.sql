CREATE TABLE [dbo].[Admin] (
    [Id] BIGINT IDENTITY (1,1) NOT NULL,
    CONSTRAINT PK_AdminId PRIMARY KEY CLUSTERED (Id),
    [Name] NVARCHAR (100) NOT NULL,
    [Password] NVARCHAR (100) NOT NULL,
    [Email] NVARCHAR (100) NOT NULL
);

