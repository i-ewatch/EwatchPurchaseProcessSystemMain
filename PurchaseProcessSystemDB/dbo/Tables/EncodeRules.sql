CREATE TABLE [dbo].[EncodeRules] (
    [pk]       INT            NOT NULL,
    [Code]     NVARCHAR (50)  NOT NULL,
    [CodeMeam] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_EncodeRules] PRIMARY KEY CLUSTERED ([pk] ASC)
);

