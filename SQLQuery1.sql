CREATE TABLE [dbo].[Chats] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [UserOneId]     NVARCHAR (20)  NOT NULL,
    [UserTwoId]     NVARCHAR (20)  NOT NULL,
    [LastMessageAt] DATETIME2 DEFAULT GETDATE()  NOT NULL,
    CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[GroupChats] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [UserIDs]     NVARCHAR (20)  NOT NULL,
    [Name]     NVARCHAR (20)  NOT NULL,
    [LastMessageAt] DATETIME2 DEFAULT GETDATE()  NOT NULL,
    CONSTRAINT [PK_GroupChats] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (30)  NOT NULL,
    [Token]        NVARCHAR (30)  NOT NULL,
    [Password]     NVARCHAR (30)  NOT NULL,
    [IsAdmin]      INT             NOT NULL,
    [LastOnlineAt] DATETIME2 DEFAULT GETDATE()  NOT NULL,
    [ProfileImage] VARBINARY (MAX) DEFAULT (0x) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);