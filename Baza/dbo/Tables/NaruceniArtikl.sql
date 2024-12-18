CREATE TABLE [dbo].[NaruceniArtikl] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [ArtiklNaruceniId] INT             NULL,
    [Cijena]           DECIMAL (10, 2) NULL,
    [Kolicina]         INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

