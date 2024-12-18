CREATE TABLE [dbo].[Adrese] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Ime]           NVARCHAR (50)  NULL,
    [Prezime]       NVARCHAR (50)  NULL,
    [Ulica]         NVARCHAR (256) NULL,
    [Grad]          NVARCHAR (128) NULL,
    [PostanskiBroj] NVARCHAR (20)  NULL,
    [Drzava]        NVARCHAR (128) NULL,
    [KorisnikId]    NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK__Adrese__3214EC07DEF9E315] PRIMARY KEY CLUSTERED ([Id] ASC)
);

