CREATE TABLE [dbo].[Korisnici] (
    [Id]            NVARCHAR (450) NOT NULL,
    [Alias]         NVARCHAR (256) NULL,
    [KorisnickoIme] NVARCHAR (256) NULL,
    [Lozinka]       NVARCHAR (MAX) NOT NULL,
    [Email]         NVARCHAR (256) NULL,
    [AdresaID]      INT            NOT NULL,
    CONSTRAINT [PK__Korisnic__3214EC0748616748] PRIMARY KEY CLUSTERED ([Id] ASC)
);

