USE [master]
GO
/****** Object:  Database [FormsAdminGPDB]    Script Date: 3/27/2019 8:29:02 AM ******/
CREATE DATABASE [FormsAdminGPDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FormsAdminGPDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\FormsAdminGPDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FormsAdminGPDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\FormsAdminGPDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FormsAdminGPDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FormsAdminGPDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FormsAdminGPDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FormsAdminGPDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FormsAdminGPDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FormsAdminGPDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FormsAdminGPDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FormsAdminGPDB] SET  MULTI_USER 
GO
ALTER DATABASE [FormsAdminGPDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FormsAdminGPDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FormsAdminGPDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FormsAdminGPDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [FormsAdminGPDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [FormsAdminGPDB]
GO
/****** Object:  Schema [landing]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE SCHEMA [landing]
GO
/****** Object:  Schema [security]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE SCHEMA [security]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/27/2019 8:29:03 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormHdLandingPage]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormHdLandingPage](
	[LandingPageId] [nvarchar](450) NOT NULL,
	[FormHdId] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_FormHdLandingPage] PRIMARY KEY CLUSTERED 
(
	[FormHdId] ASC,
	[LandingPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [landing].[DDLCatalogs]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [landing].[DDLCatalogs](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Name] [nvarchar](225) NOT NULL,
	[FormDetailId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_DDLCatalogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [landing].[FormDetails]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [landing].[FormDetails](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FormHdId] [nvarchar](450) NOT NULL,
	[FieldTypeId] [nvarchar](50) NOT NULL,
	[FieldLabel] [nvarchar](225) NOT NULL,
	[Order] [tinyint] NOT NULL,
	[IsRequired] [bit] NOT NULL,
 CONSTRAINT [PK_FormDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [landing].[FormHds]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [landing].[FormHds](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[Title] [nvarchar](225) NOT NULL,
	[FilePath] [nvarchar](450) NULL,
	[Name] [nvarchar](225) NOT NULL,
 CONSTRAINT [PK_FormHds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [landing].[InfoRequests]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [landing].[InfoRequests](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[InfoRequestData] [nvarchar](max) NOT NULL,
	[LandingPageId] [nvarchar](450) NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_InfoRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [landing].[LandingPages]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [landing].[LandingPages](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[Name] [nvarchar](225) NOT NULL,
	[Description] [nvarchar](450) NULL,
 CONSTRAINT [PK_LandingPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [security].[Roles]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [security].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [security].[UserRoles]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [security].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [security].[Users]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [security].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Username] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[DisplayName] [nvarchar](max) NULL,
	[LastLoggedIn] [datetimeoffset](7) NULL,
	[SerialNumber] [nvarchar](450) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [security].[UserTokens]    Script Date: 3/27/2019 8:29:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [security].[UserTokens](
	[Id] [nvarchar](450) NOT NULL,
	[AccessTokenHash] [nvarchar](max) NULL,
	[AccessTokenExpiresDateTime] [datetimeoffset](7) NOT NULL,
	[RefreshTokenIdHash] [nvarchar](450) NOT NULL,
	[RefreshTokenIdHashSource] [nvarchar](450) NULL,
	[RefreshTokenExpiresDateTime] [datetimeoffset](7) NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FormHdLandingPage_LandingPageId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_FormHdLandingPage_LandingPageId] ON [dbo].[FormHdLandingPage]
(
	[LandingPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_DDLCatalogs_FormDetailId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_DDLCatalogs_FormDetailId] ON [landing].[DDLCatalogs]
(
	[FormDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FormDetails_FormHdId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_FormDetails_FormHdId] ON [landing].[FormDetails]
(
	[FormHdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_InfoRequests_LandingPageId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_InfoRequests_LandingPageId] ON [landing].[InfoRequests]
(
	[LandingPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Roles_Name]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_Name] ON [security].[Roles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [security].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserRoles_UserId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [security].[UserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_Username]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username] ON [security].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserTokens_UserId]    Script Date: 3/27/2019 8:29:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserTokens_UserId] ON [security].[UserTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [landing].[InfoRequests] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [RequestDate]
GO
ALTER TABLE [dbo].[FormHdLandingPage]  WITH CHECK ADD  CONSTRAINT [FK_FormHdLandingPage_FormHds_FormHdId] FOREIGN KEY([FormHdId])
REFERENCES [landing].[FormHds] ([Id])
GO
ALTER TABLE [dbo].[FormHdLandingPage] CHECK CONSTRAINT [FK_FormHdLandingPage_FormHds_FormHdId]
GO
ALTER TABLE [dbo].[FormHdLandingPage]  WITH CHECK ADD  CONSTRAINT [FK_FormHdLandingPage_LandingPages_LandingPageId] FOREIGN KEY([LandingPageId])
REFERENCES [landing].[LandingPages] ([Id])
GO
ALTER TABLE [dbo].[FormHdLandingPage] CHECK CONSTRAINT [FK_FormHdLandingPage_LandingPages_LandingPageId]
GO
ALTER TABLE [landing].[DDLCatalogs]  WITH CHECK ADD  CONSTRAINT [FK_DDLCatalogs_FormDetails_FormDetailId] FOREIGN KEY([FormDetailId])
REFERENCES [landing].[FormDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [landing].[DDLCatalogs] CHECK CONSTRAINT [FK_DDLCatalogs_FormDetails_FormDetailId]
GO
ALTER TABLE [landing].[FormDetails]  WITH CHECK ADD  CONSTRAINT [FK_FormDetails_FormHds_FormHdId] FOREIGN KEY([FormHdId])
REFERENCES [landing].[FormHds] ([Id])
GO
ALTER TABLE [landing].[FormDetails] CHECK CONSTRAINT [FK_FormDetails_FormHds_FormHdId]
GO
ALTER TABLE [landing].[InfoRequests]  WITH CHECK ADD  CONSTRAINT [FK_InfoRequests_LandingPages_LandingPageId] FOREIGN KEY([LandingPageId])
REFERENCES [landing].[LandingPages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [landing].[InfoRequests] CHECK CONSTRAINT [FK_InfoRequests_LandingPages_LandingPageId]
GO
ALTER TABLE [security].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [security].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [security].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [security].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [security].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [security].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [security].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [security].[Users] ([Id])
GO
ALTER TABLE [security].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [FormsAdminGPDB] SET  READ_WRITE 
GO
