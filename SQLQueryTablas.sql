USE [ToyoClients]
GO

/****** Object:  Table [dbo].[Encuestas]    Script Date: 5/7/2026 13:47:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Encuestas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Rating] [int] NULL,
	[Comentario] [nvarchar](500) NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 5/7/2026 13:47:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[MovilNumber] [nvarchar](max) NULL,
	[DNINumber] [nvarchar](max) NULL,
	[TipoDNI] [nchar](10) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


