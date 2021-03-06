USE [master]
GO
/****** Object:  Database [FormsGP]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE DATABASE [FormsGP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FormsGP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\FormsGP.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FormsGP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\FormsGP_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FormsGP] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FormsGP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FormsGP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FormsGP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FormsGP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FormsGP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FormsGP] SET ARITHABORT OFF 
GO
ALTER DATABASE [FormsGP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FormsGP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FormsGP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FormsGP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FormsGP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FormsGP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FormsGP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FormsGP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FormsGP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FormsGP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FormsGP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FormsGP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FormsGP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FormsGP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FormsGP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FormsGP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FormsGP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FormsGP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FormsGP] SET  MULTI_USER 
GO
ALTER DATABASE [FormsGP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FormsGP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FormsGP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FormsGP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [FormsGP] SET DELAYED_DURABILITY = DISABLED 
GO
USE [FormsGP]
GO
/****** Object:  Schema [landing]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE SCHEMA [landing]
GO
/****** Object:  Schema [security]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE SCHEMA [security]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/10/2019 1:12:29 PM ******/
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
/****** Object:  Table [landing].[DDLCatalogs]    Script Date: 4/10/2019 1:12:29 PM ******/
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
/****** Object:  Table [landing].[FormDetails]    Script Date: 4/10/2019 1:12:29 PM ******/
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
	[IsMandatory] [bit] NOT NULL,
 CONSTRAINT [PK_FormDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [landing].[FormHds]    Script Date: 4/10/2019 1:12:29 PM ******/
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
	[MailTemplateId] [nvarchar](450) NULL,
 CONSTRAINT [PK_FormHds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [landing].[InfoRequests]    Script Date: 4/10/2019 1:12:29 PM ******/
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
	[FormHdId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_InfoRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [landing].[LandingPages]    Script Date: 4/10/2019 1:12:29 PM ******/
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
	[FormHdId] [nvarchar](450) NULL,
 CONSTRAINT [PK_LandingPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [landing].[MailTemplates]    Script Date: 4/10/2019 1:12:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [landing].[MailTemplates](
	[Id] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[Subject] [nvarchar](225) NOT NULL,
	[Salut] [nvarchar](max) NULL,
	[Body] [nvarchar](1024) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MailTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [security].[Roles]    Script Date: 4/10/2019 1:12:29 PM ******/
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
/****** Object:  Table [security].[UserRoles]    Script Date: 4/10/2019 1:12:29 PM ******/
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
/****** Object:  Table [security].[Users]    Script Date: 4/10/2019 1:12:29 PM ******/
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
/****** Object:  Table [security].[UserTokens]    Script Date: 4/10/2019 1:12:29 PM ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190309161017_firstRun', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190312212810_firstRun02', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190312213026_firstRun03', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190313210505_firstRun01', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190314064227_firstRun00', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190314235939_firstRun0', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318004058_firstRun1', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318011740_firstRun2', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318011957_firstRun3', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318014228_firstRun4', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318232511_firstRun5', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190327213959_iitial', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190328000035_initial1', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190328223322_initlizr', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190328223449_initlizr3', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190329165227_initz', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190401174420_intlfx', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190402155423_intux', N'2.2.3-servicing-35854')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190405163503_emailmax1000', N'2.2.3-servicing-35854')
INSERT [security].[Roles] ([Id], [Name]) VALUES (N'701c25af-c14e-4950-9af1-de65270c2939', N'MASTER')
INSERT [security].[UserRoles] ([UserId], [RoleId]) VALUES (N'645b4c1b-fae2-4efa-a4d4-7a15727aff88', N'701c25af-c14e-4950-9af1-de65270c2939')
INSERT [security].[UserRoles] ([UserId], [RoleId]) VALUES (N'a5a4228d-5f0b-4da0-96e9-377ed7fc0d3a', N'701c25af-c14e-4950-9af1-de65270c2939')
INSERT [security].[UserRoles] ([UserId], [RoleId]) VALUES (N'c06e1a6a-e29e-4e46-8d31-baf06660b710', N'701c25af-c14e-4950-9af1-de65270c2939')
INSERT [security].[UserRoles] ([UserId], [RoleId]) VALUES (N'c123d1b4-593a-4874-bca4-07abedaacaf5', N'701c25af-c14e-4950-9af1-de65270c2939')
INSERT [security].[Users] ([Id], [IsActive], [Username], [Password], [DisplayName], [LastLoggedIn], [SerialNumber]) VALUES (N'645b4c1b-fae2-4efa-a4d4-7a15727aff88', 1, N'margarita.arellano@grupoperti.com.mx', N'e5bkyvrHgIqPl5iC2Mer/g4TQpM4Jfzd2NVfq1tstik=', N'MAGGI', NULL, N'c58126f5-7e22-47e2-bdce-8cd1fcd35738')
INSERT [security].[Users] ([Id], [IsActive], [Username], [Password], [DisplayName], [LastLoggedIn], [SerialNumber]) VALUES (N'a5a4228d-5f0b-4da0-96e9-377ed7fc0d3a', 1, N'ana.caballero@grupoperti.com.mx', N'LuK0LF8Wy0Dvc4a7iiLZv//QTJBa4w6BK3yetp5G9pg=', N'Ana Caballero', CAST(N'2019-03-11T00:00:00.0000000-06:00' AS DateTimeOffset), N'cb48f2bf-2633-42fd-abb5-e38eebae1d8b')
INSERT [security].[Users] ([Id], [IsActive], [Username], [Password], [DisplayName], [LastLoggedIn], [SerialNumber]) VALUES (N'c06e1a6a-e29e-4e46-8d31-baf06660b710', 1, N'miguel.munoz@grupoperti.com.mx', N'e5bkyvrHgIqPl5iC2Mer/g4TQpM4Jfzd2NVfq1tstik=', N'MIKE', NULL, N'1f42b082-a91a-4f49-afe9-01e6c6f5e14c')
INSERT [security].[Users] ([Id], [IsActive], [Username], [Password], [DisplayName], [LastLoggedIn], [SerialNumber]) VALUES (N'c123d1b4-593a-4874-bca4-07abedaacaf5', 1, N'maudelaire.cius@grupoperti.com.mx', N'XtGsB1L6vFtaDi5p8PvQmKGo8sWc+jXNr6UZuuBrG3g=', N'JAN MOY', CAST(N'2019-03-11T00:00:00.0000000-06:00' AS DateTimeOffset), N'49c6027c-0d5b-45fd-8110-c23343cfb293')
INSERT [security].[UserTokens] ([Id], [AccessTokenHash], [AccessTokenExpiresDateTime], [RefreshTokenIdHash], [RefreshTokenIdHashSource], [RefreshTokenExpiresDateTime], [UserId]) VALUES (N'00c5d95f-efc7-4aef-80ac-546e44e34c68', N'JRgGgkIwN7/165CfNif1dz6GJ0/hm/26wOOkVAfPUEw=', CAST(N'2019-04-02T17:44:00.6304257+00:00' AS DateTimeOffset), N'QJh7llgYfa09Ke9TItFiMEROrjIYXjFJk1RIGqD/s8A=', NULL, CAST(N'2019-04-02T17:49:00.6304257+00:00' AS DateTimeOffset), N'c06e1a6a-e29e-4e46-8d31-baf06660b710')
INSERT [security].[UserTokens] ([Id], [AccessTokenHash], [AccessTokenExpiresDateTime], [RefreshTokenIdHash], [RefreshTokenIdHashSource], [RefreshTokenExpiresDateTime], [UserId]) VALUES (N'8a560621-b378-4cc9-acf2-695a78625279', N'svAOnW82P3LZ8X4RhP5qA1fMgD+lIBppWze+OZYU96w=', CAST(N'2019-04-10T16:50:54.7068135+00:00' AS DateTimeOffset), N'QylGzVbuKnHan8X/x/1pSngNPEaaaggbjDPMIifoxRc=', NULL, CAST(N'2019-04-10T16:55:54.7068135+00:00' AS DateTimeOffset), N'645b4c1b-fae2-4efa-a4d4-7a15727aff88')
INSERT [security].[UserTokens] ([Id], [AccessTokenHash], [AccessTokenExpiresDateTime], [RefreshTokenIdHash], [RefreshTokenIdHashSource], [RefreshTokenExpiresDateTime], [UserId]) VALUES (N'a63ae643-8c81-413f-8934-d32fd86b38ff', N'HBGnW6FTtt6YTXCz6ltugig5Dija6Ms+HlYINK/aTnY=', CAST(N'2019-04-08T22:41:55.8194265+00:00' AS DateTimeOffset), N'1rIhWpIevckmOCj1Bx0G6PJUnBTccCT68qEmJ4KNaqk=', NULL, CAST(N'2019-04-08T22:46:55.8194265+00:00' AS DateTimeOffset), N'a5a4228d-5f0b-4da0-96e9-377ed7fc0d3a')
INSERT [security].[UserTokens] ([Id], [AccessTokenHash], [AccessTokenExpiresDateTime], [RefreshTokenIdHash], [RefreshTokenIdHashSource], [RefreshTokenExpiresDateTime], [UserId]) VALUES (N'afcfb7bf-7793-4af1-b580-0583f4e696b4', N'rWWO9Xph+EjWIeyWS9gxv2PXpesBz8fPENDPHjZYSHg=', CAST(N'2019-04-10T16:36:41.1284586+00:00' AS DateTimeOffset), N'drLY+ZToPEp5wA03W+hs0CJgGB9cfNMGgqlBctpShhk=', NULL, CAST(N'2019-04-10T16:41:41.1284586+00:00' AS DateTimeOffset), N'c123d1b4-593a-4874-bca4-07abedaacaf5')
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_DDLCatalogs_FormDetailId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_DDLCatalogs_FormDetailId] ON [landing].[DDLCatalogs]
(
	[FormDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FormDetails_FormHdId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_FormDetails_FormHdId] ON [landing].[FormDetails]
(
	[FormHdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FormHds_MailTemplateId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_FormHds_MailTemplateId] ON [landing].[FormHds]
(
	[MailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_InfoRequests_FormHdId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_InfoRequests_FormHdId] ON [landing].[InfoRequests]
(
	[FormHdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_InfoRequests_LandingPageId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_InfoRequests_LandingPageId] ON [landing].[InfoRequests]
(
	[LandingPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_LandingPages_FormHdId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_LandingPages_FormHdId] ON [landing].[LandingPages]
(
	[FormHdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Roles_Name]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_Name] ON [security].[Roles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [security].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserRoles_UserId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [security].[UserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_Username]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username] ON [security].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserTokens_UserId]    Script Date: 4/10/2019 1:12:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserTokens_UserId] ON [security].[UserTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [landing].[FormDetails] ADD  DEFAULT ((0)) FOR [IsMandatory]
GO
ALTER TABLE [landing].[InfoRequests] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [RequestDate]
GO
ALTER TABLE [landing].[MailTemplates] ADD  DEFAULT (N'') FOR [Name]
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
ALTER TABLE [landing].[FormHds]  WITH CHECK ADD  CONSTRAINT [FK_FormHds_MailTemplates_MailTemplateId] FOREIGN KEY([MailTemplateId])
REFERENCES [landing].[MailTemplates] ([Id])
GO
ALTER TABLE [landing].[FormHds] CHECK CONSTRAINT [FK_FormHds_MailTemplates_MailTemplateId]
GO
ALTER TABLE [landing].[InfoRequests]  WITH CHECK ADD  CONSTRAINT [FK_InfoRequests_FormHds_FormHdId] FOREIGN KEY([FormHdId])
REFERENCES [landing].[FormHds] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [landing].[InfoRequests] CHECK CONSTRAINT [FK_InfoRequests_FormHds_FormHdId]
GO
ALTER TABLE [landing].[InfoRequests]  WITH CHECK ADD  CONSTRAINT [FK_InfoRequests_LandingPages_LandingPageId] FOREIGN KEY([LandingPageId])
REFERENCES [landing].[LandingPages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [landing].[InfoRequests] CHECK CONSTRAINT [FK_InfoRequests_LandingPages_LandingPageId]
GO
ALTER TABLE [landing].[LandingPages]  WITH CHECK ADD  CONSTRAINT [FK_LandingPages_FormHds_FormHdId] FOREIGN KEY([FormHdId])
REFERENCES [landing].[FormHds] ([Id])
GO
ALTER TABLE [landing].[LandingPages] CHECK CONSTRAINT [FK_LandingPages_FormHds_FormHdId]
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
ALTER DATABASE [FormsGP] SET  READ_WRITE 
GO
