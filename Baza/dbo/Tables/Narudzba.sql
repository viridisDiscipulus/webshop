CREATE TABLE [dbo].[Narudzba] (
    [Id]                INT                IDENTITY (1, 1) NOT NULL,
    [KupacEmail]        NVARCHAR (100)     NULL,
    [DatumNarudzbe]     DATETIMEOFFSET (7) CONSTRAINT [DF__Narudzba__DatumN__1F98B2C1] DEFAULT (sysdatetimeoffset()) NULL,
    [AdresaDostaveId]   INT                NULL,
    [NacinIsporukeId]   INT                NULL,
    [NaruceniArtikliId] NVARCHAR (100)     NULL,
    [UkupnaCijena]      DECIMAL (10, 2)    NULL,
    [Status]            NVARCHAR (50)      NULL,
    CONSTRAINT [PK__Narudzba__3214EC0781D3908C] PRIMARY KEY CLUSTERED ([Id] ASC)
);

