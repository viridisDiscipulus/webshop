CREATE TABLE [dbo].[RobnaMarka] (
    [ID]    INT           IDENTITY (1, 1) NOT NULL,
    [Naziv] NVARCHAR (50) CONSTRAINT [DF_RobnaMarka_Naziv] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_RobnaMarka] PRIMARY KEY CLUSTERED ([ID] ASC)
);

