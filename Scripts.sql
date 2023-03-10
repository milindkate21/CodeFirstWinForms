USE [Test]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01-02-2023 18:19:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 01-02-2023 18:19:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentInfo]    Script Date: 01-02-2023 18:19:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](max) NOT NULL,
	[lastname] [nvarchar](max) NOT NULL,
	[phonenumber] [nvarchar](max) NOT NULL,
	[city] [nvarchar](max) NOT NULL,
	[typeValue] [nvarchar](max) NOT NULL,
	[recordCount] [int] NOT NULL,
	[countryID] [int] NOT NULL,
 CONSTRAINT [PK_StudentInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[StudentInfo]  WITH CHECK ADD  CONSTRAINT [FK_StudentInfo_Country_countryID] FOREIGN KEY([countryID])
REFERENCES [dbo].[Country] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StudentInfo] CHECK CONSTRAINT [FK_StudentInfo_Country_countryID]
GO

/****** Object:  Insert Data - Country table    Script Date: 01-02-2023 18:19:11 ******/
--insert into country values('Canada')
--insert into country values('India')
--insert into country values('US')
--insert into country values('UK')
--insert into country values('Japan')
--insert into country values('China')