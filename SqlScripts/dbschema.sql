SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE TABLE [dbo].[FightPassVideoScrape](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Link] [nvarchar](max) NULL,
	[SearchTerm1] [nvarchar](max) NULL,
	[SearchTerm2] [nvarchar](max) NULL,
	[VideoId] [int] NULL,
	[CountOfDuplicate] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[NameFixed] [nvarchar](max) NULL,
	[Fighter1Name] [nvarchar](max) NULL,
	[Fighter2Name] [nvarchar](max) NULL,
	[ImageLink] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[CanIgnore] [bit] NULL,
	[AlreadyUsed] [bit] NULL,
	[InsertDateTime] [datetime] NULL,
	[ProcessedNames] [bit] NULL,
	[CopiedOver] [bit] NULL,
	[ActuallyIgnore] [bit] NULL,
	[TitleFight] [bit] NOT NULL,
	[FOTN] [bit] NOT NULL,
	[KOTN] [bit] NOT NULL,
	[POTN] [bit] NOT NULL,
	[SOTN] [bit] NOT NULL,
 CONSTRAINT [PK_FightPassVideoScrape] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[NameChange](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromName] [nvarchar](512) NULL,
	[ToName] [nvarchar](512) NULL,
	[FighterNoChanged] [int] NULL,
	[FightId] [nvarchar](max) NULL,
	[IsNickname] [bit] NULL,
	[Ignore] [bit] NULL,
	[WikifightIdsUpdated] [nvarchar](max) NULL,
	[ManualFixDate] [datetime] NULL,
 CONSTRAINT [PK_NameChange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[fight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SequenceNo] [int] NULL,
	[VideoLink] [nvarchar](1024) NULL,
	[ImagePath] [varchar](max) NULL,
	[ImageUrl] [nvarchar](1024) NULL,
	[Fighter1Name] [nvarchar](1024) NULL,
	[Fighter2Name] [nvarchar](1024) NULL,
	[NameFixed] [nvarchar](1024) NULL,
	[Name] [nvarchar](1024) NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[EventId] [int] NULL,
	[Prelim] [bit] NULL,
	[Promotion] [nvarchar](1024) NULL,
	[Processed] [bit] NULL,
	[DateProcessed] [datetime] NULL,
	[Page] [nvarchar](1024) NULL,
	[UpdateNote] [nvarchar](4000) NULL,
	[UpdateDateTime] [datetime] NULL,
	[IsDuplicateOfAnotherFight] [bit] NULL,
	[IgnoreFight] [bit] NULL,
	[WasIgnoredNowNewFightId] [int] NULL,
	[FromScrapingSerach] [bit] NULL,
	[FromBellatorLibrary] [bit] NULL,
	[BrokenLink] [bit] NULL,
	[InsertDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_fight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[WikiEvent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SequenceNo] [nvarchar](256) NULL,
	[Name] [nvarchar](2056) NULL,
	[DateHeld] [datetime] NULL,
	[Venue] [nvarchar](2056) NULL,
	[Location] [nvarchar](2056) NULL,
	[Attendance] [nvarchar](256) NULL,
	[Promotion] [nvarchar](256) NULL,
	[Link] [nvarchar](2056) NULL,
	[Processed] [bit] NULL,
 CONSTRAINT [PK_WikiEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [ufc]
GO

/****** Object:  Table [dbo].[wikifight]    Script Date: 2018-12-29 12:14:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[wikifight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WeightClass] [nvarchar](2056) NULL,
	[Fighter1Name] [nvarchar](2056) NULL,
	[Fighter1WikiLink] [nvarchar](2056) NULL,
	[Fighter1Pic] [nvarchar](max) NULL,
	[Fighter2Name] [nvarchar](2056) NULL,
	[Fighter2WikiLink] [nvarchar](2056) NULL,
	[Fighter2Pic] [nvarchar](max) NULL,
	[FightResult] [nvarchar](2056) NULL,
	[FightResultHow] [nvarchar](2056) NULL,
	[FightResultType] [nvarchar](2056) NULL,
	[FightResultSubType] [nvarchar](2056) NULL,
	[Round] [nvarchar](2056) NULL,
	[Time] [nvarchar](2056) NULL,
	[TotalTime] [time](7) NULL,
	[Notes] [nvarchar](2056) NULL,
	[CardType] [nvarchar](2056) NULL,
	[LinkUpdateNote] [nvarchar](2056) NULL,
	[DateUpdated] [datetime] NULL,
	[EventId] [int] NULL,
	[FightId] [int] NULL,
	[DuplicateFightIds] [nvarchar](2056) NULL,
	[ImageForWeb] [nvarchar](2056) NULL,
	[WatchCount] [int] NULL,
	[RedditTopFights] [int] NULL,
	[FOTN] [bit] NULL,
	[POTN] [bit] NULL,
	[SOTN] [bit] NULL,
	[KOTN] [bit] NULL,
	[TitleFight] [bit] NULL,
	[VideoLink] [nvarchar](1024) NULL,
	[ImagePath] [nvarchar](2056) NULL,
 CONSTRAINT [PK_wikifight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[wikifight]  WITH NOCHECK ADD  CONSTRAINT [FK_wikifight_fight] FOREIGN KEY([FightId])
REFERENCES [dbo].[fight] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[wikifight] NOCHECK CONSTRAINT [FK_wikifight_fight]
GO

ALTER TABLE [dbo].[wikifight]  WITH NOCHECK ADD  CONSTRAINT [FK_wikifight_WikiEvent] FOREIGN KEY([EventId])
REFERENCES [dbo].[WikiEvent] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[wikifight] NOCHECK CONSTRAINT [FK_wikifight_WikiEvent]
GO

USE [ufc]
GO

/****** Object:  Table [dbo].[wikifightWeb]    Script Date: 2018-12-29 12:15:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[wikifightWeb](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WikiFightId] [int] NULL,
	[WeightClass] [nvarchar](2056) NULL,
	[Fighter1Name] [nvarchar](2056) NULL,
	[Fighter2Name] [nvarchar](2056) NULL,
	[FightResult] [nvarchar](2056) NULL,
	[FightResultHow] [nvarchar](2056) NULL,
	[FightResultType] [nvarchar](2056) NULL,
	[FightResultSubType] [nvarchar](2056) NULL,
	[Round] [nvarchar](2056) NULL,
	[Time] [nvarchar](2056) NULL,
	[TotalTime] [time](7) NULL,
	[TotalTimeEnum] [int] NULL,
	[EventId] [int] NULL,
	[FightId] [int] NULL,
	[ImageForWeb] [varchar](2056) NULL,
	[RedditTopFights] [int] NOT NULL,
	[FOTN] [bit] NOT NULL,
	[POTN] [bit] NOT NULL,
	[TitleFight] [bit] NOT NULL,
	[VideoLink] [nvarchar](1024) NULL,
	[ImagePath] [varchar](2056) NULL,
	[DateHeld] [datetime] NULL,
	[EventName] [nvarchar](2056) NULL,
	[Promotion] [nvarchar](256) NULL,
 CONSTRAINT [PK_WikifightWeb] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[wikifightWeb]  WITH NOCHECK ADD  CONSTRAINT [FK_wikifightWeb_WikiEvent] FOREIGN KEY([EventId])
REFERENCES [dbo].[WikiEvent] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[wikifightWeb] NOCHECK CONSTRAINT [FK_wikifightWeb_WikiEvent]
GO



