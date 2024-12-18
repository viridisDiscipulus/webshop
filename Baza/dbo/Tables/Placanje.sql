CREATE TABLE [dbo].[Placanje] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [VlasnikKartice] NVARCHAR (255) NOT NULL,
    [BrojKartice]    NVARCHAR (255) NOT NULL,
    [DatumIsteka]    NVARCHAR (10)  NOT NULL,
    [CVV]            NVARCHAR (4)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

