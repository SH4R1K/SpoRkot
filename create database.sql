CREATE DATABASE [SPO_RKOT_HaHaTun]
GO

USE [SPO_RKOT_HaHaTun]
GO

CREATE LOGIN [HahatunUser] WITH PASSWORD=N'12345', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[русский], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
/****** Object:  User [writer]    Script Date: 12.11.2023 18:30:33 ******/
CREATE USER [writer] FOR LOGIN [HahatunUser] WITH DEFAULT_SCHEMA=[dbo]
GO

EXEC sp_addrolemember 'db_datareader', 'writer'
EXEC sp_addrolemember 'db_datawriter', 'writer'
GO

/****** Object:  Table [dbo].[HttpQuality]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HttpQuality](
	[ReportId] [int] NOT NULL,
	[SessionFailureRatio] [decimal](2, 1) NOT NULL,
	[ULMeanUserDataRate] [decimal](5, 1) NOT NULL,
	[DLMeanUserDataRate] [decimal](6, 1) NOT NULL,
	[SessionTime] [decimal](3, 1) NOT NULL,
 CONSTRAINT [PK_QualityHTTP] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operator]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operator](
	[OperatorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Operator] PRIMARY KEY CLUSTERED 
(
	[OperatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[OperatorId] [int] NOT NULL,
	[ReportInfoId] [int] NOT NULL,
 CONSTRAINT [PK_Report_1] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportInfo]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportInfo](
	[ReportInfoId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[FederalDistrict] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_ReportInfo] PRIMARY KEY CLUSTERED 
(
	[ReportInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsQuality]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsQuality](
	[ReportId] [int] NOT NULL,
	[ShareUndelivered] [decimal](2, 1) NOT NULL,
	[AverageDeliveryTime] [decimal](2, 1) NOT NULL,
 CONSTRAINT [PK_QualitySMS] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stat]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stat](
	[ReportId] [int] NOT NULL,
	[CountTestConnection] [smallint] NOT NULL,
	[POLQA] [int] NOT NULL,
	[NegativeMOSSamplesCount] [smallint] NOT NULL,
	[CountSMS] [smallint] NOT NULL,
	[CountLoadFile] [smallint] NOT NULL,
	[CountWebBrowsing] [smallint] NOT NULL,
 CONSTRAINT [PK_Stats] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoiceQuality]    Script Date: 12.11.2023 12:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoiceQuality](
	[ReportId] [int] NOT NULL,
	[NonAccessibilityRatio] [decimal](2, 1) NOT NULL,
	[CutoffRatio] [decimal](2, 1) NOT NULL,
	[MOSPOLQA] [decimal](2, 1) NOT NULL,
	[NegativeMOSSamplesRatio] [decimal](2, 1) NOT NULL,
 CONSTRAINT [PK_QualityVoice] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HttpQuality]  WITH CHECK ADD  CONSTRAINT [FK_QualityHTTP_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([ReportId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HttpQuality] CHECK CONSTRAINT [FK_QualityHTTP_Report]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Operator] FOREIGN KEY([OperatorId])
REFERENCES [dbo].[Operator] ([OperatorId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Operator]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_ReportInfo] FOREIGN KEY([ReportInfoId])
REFERENCES [dbo].[ReportInfo] ([ReportInfoId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_ReportInfo]
GO
ALTER TABLE [dbo].[SmsQuality]  WITH CHECK ADD  CONSTRAINT [FK_QualitySMS_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([ReportId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SmsQuality] CHECK CONSTRAINT [FK_QualitySMS_Report]
GO
ALTER TABLE [dbo].[Stat]  WITH CHECK ADD  CONSTRAINT [FK_Stats_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([ReportId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stat] CHECK CONSTRAINT [FK_Stats_Report]
GO
ALTER TABLE [dbo].[VoiceQuality]  WITH CHECK ADD  CONSTRAINT [FK_VoiceQuality_Report] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Report] ([ReportId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VoiceQuality] CHECK CONSTRAINT [FK_VoiceQuality_Report]
GO
