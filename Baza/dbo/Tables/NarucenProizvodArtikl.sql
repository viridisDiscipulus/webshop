CREATE TABLE [dbo].[NarucenProizvodArtikl] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ProizvodArtiklId] INT            NULL,
    [ProizvodNaziv]    NVARCHAR (100) NULL,
    [SlikaUrl]         NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

