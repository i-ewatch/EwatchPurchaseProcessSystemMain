CREATE TABLE [dbo].[Costofferform] (
    [pk]            INT            NOT NULL,
    [ProjectNO]     NVARCHAR (50)  NOT NULL,
    [ProjectItem]   NVARCHAR (50)  NULL,
    [ProjectName]   NVARCHAR (MAX) NULL,
    [ProjectUnit]   NVARCHAR (50)  NULL,
    [ProjectAmount] NVARCHAR (50)  NULL,
    [Price]         INT            NULL,
    [Money]         INT            NULL,
    [Remark]        NVARCHAR (50)  NULL,
    [ProjectCode]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Costofferform] PRIMARY KEY CLUSTERED ([pk] ASC)
);

