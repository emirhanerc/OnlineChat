CREATE TABLE [dbo].[Chats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserIDs] [nchar](255) NOT NULL,
	[Name] [nchar](255) NULL,
	[LastMessageAt] [datetime2](7) NOT NULL
);

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](30) NOT NULL,
	[Token] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[IsAdmin] [int] NOT NULL,
	[LastOnlineAt] [datetime2](7) NOT NULL,
	[ProfileImage] [varbinary](max) NULL,
	[PictureID] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
);
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](255) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ChatId] [int] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
);
