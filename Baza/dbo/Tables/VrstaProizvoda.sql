CREATE TABLE [dbo].[VrstaProizvoda] (
    [ID]    INT           IDENTITY (1, 1) NOT NULL,
    [Naziv] NVARCHAR (50) CONSTRAINT [DF_VrstaProizvoda_Naziv] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_VrstaProizvoda] PRIMARY KEY CLUSTERED ([ID] ASC)
);

