CREATE TABLE [dbo].[KorisniciUloge] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [KorisnikID] NVARCHAR (450) NULL,
    [UlogaID]    INT            NULL,
    CONSTRAINT [PK_KorisniciUloge] PRIMARY KEY CLUSTERED ([ID] ASC)
);

