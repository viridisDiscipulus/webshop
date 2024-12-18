CREATE TABLE [dbo].[NacinIsporuke] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [KratkiNaziv]    NVARCHAR (50)   NULL,
    [VrijemeDostave] NVARCHAR (50)   NULL,
    [Opis]           NVARCHAR (200)  NULL,
    [Cijena]         DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

