USE [DBSyf]
GO

/****** Object:  Table [dbo].[People]    Script Date: 2020/03/16 20:38:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[EmailAddress] [nvarchar](250) NOT NULL,
	[PhoneNumber] [nvarchar](30) NOT NULL,
	[MobileNumber] [nvarchar](30) NOT NULL,
	[PersonPositionId] [uniqueidentifier] NULL,
	[BusinessUnitId] [uniqueidentifier] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[LanguageId] [uniqueidentifier] NULL,
	[RarStartDate] [date] NULL,
	[RarEndDate] [date] NULL,
	[RarFrequency] [int] NULL,
	[Deleted] [bit] NOT NULL,
	[CreateUser] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyUser] [nvarchar](100) NULL,
	[ModifyDate] [datetime] NOT NULL,
	[SiteId] [uniqueidentifier] NOT NULL,
	[RarSections] [int] NOT NULL,
	[TimeZoneId] [int] NULL,
	[TeamId] [uniqueidentifier] NULL,
	[IsShowRarActionsArea] [bit] NULL,
	[RarNumberOfMonthsToList] [int] NULL,
	[BackgroundColor] [varchar](10) NULL,
	[SubDepartments] [uniqueidentifier] NULL,
	[SubDepartmentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO

ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_RarNumberOfMonthsToList]  DEFAULT ((6)) FOR [RarNumberOfMonthsToList]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_BusinessUnits_BusinessUnitId] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[BusinessUnits] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_BusinessUnits_BusinessUnitId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Departments_DepartmentId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Languages_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Languages_LanguageId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_PersonPositions_PersonPositionId] FOREIGN KEY([PersonPositionId])
REFERENCES [dbo].[PersonPositions] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_PersonPositions_PersonPositionId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Sites_SiteId] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Sites] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Sites_SiteId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Teams_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Teams_TeamId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_TimeZones_TimeZoneId] FOREIGN KEY([TimeZoneId])
REFERENCES [dbo].[TimeZones] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_TimeZones_TimeZoneId]
GO


