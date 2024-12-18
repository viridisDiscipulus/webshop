CREATE TABLE [dbo].[Proizvod] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Naziv]            NVARCHAR (50)   CONSTRAINT [DF_Proizvod_Naziv] DEFAULT ('') NOT NULL,
    [Opis]             NVARCHAR (150)  CONSTRAINT [DF_Proizvod_Opis] DEFAULT ('') NULL,
    [Cijena]           DECIMAL (18, 2) NOT NULL,
    [SlikaUrl]         NVARCHAR (50)   CONSTRAINT [DF_Proizvod_SlikaUrl] DEFAULT ('') NULL,
    [VrstaProizvodaID] INT             NOT NULL,
    [RobnaMarkaID]     INT             NOT NULL,
    CONSTRAINT [PK_Proizvod] PRIMARY KEY CLUSTERED ([ID] ASC)
);

