USE [Haushaltsrechner]
GO
/****** Object:  User [HaushaltsrechnerUser]    Script Date: 01.10.2012 10:21:59 ******/
CREATE USER [HaushaltsrechnerUser] FOR LOGIN [HaushaltsrechnerUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [HaushaltsrechnerUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [HaushaltsrechnerUser]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACCOUNTUSER]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNTUSER](
	[USER_ID] [uniqueidentifier] NOT NULL,
	[ACCOUNT_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ACCOUNTUSER] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC,
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CATEGORY]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MOVEMENT]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MOVEMENT](
	[ID] [uniqueidentifier] NOT NULL,
	[AMOUNT] [decimal](10, 2) NOT NULL,
	[MESSAGE] [nvarchar](max) NULL,
	[DATE_ADDED] [datetime] NOT NULL,
	[DATE_EDIT] [datetime] NULL,
	[ACCOUNT_ID] [uniqueidentifier] NOT NULL,
	[CATEGORY_ID] [uniqueidentifier] NOT NULL,
	[USER_ID] [uniqueidentifier] NOT NULL,
	[REASON_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MOVEMENT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[REASON]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REASON](
	[ID] [uniqueidentifier] NOT NULL,
	[TEXT] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_REASON] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RIGHT]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RIGHT](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RIGHT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RIGHTUSER]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RIGHTUSER](
	[RIGHT_ID] [uniqueidentifier] NOT NULL,
	[USER_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RIGHTUSER] PRIMARY KEY CLUSTERED 
(
	[RIGHT_ID] ASC,
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USER]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](max) NOT NULL,
	[PASSWORT] [nvarchar](max) NULL,
	[ISADMIN] [bit] NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[V_OVERVIEW]    Script Date: 01.10.2012 10:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_OVERVIEW]
AS
SELECT        dbo.MOVEMENT.ID, dbo.MOVEMENT.AMOUNT, dbo.MOVEMENT.MESSAGE, dbo.MOVEMENT.DATE_ADDED, dbo.MOVEMENT.DATE_EDIT, 
                         dbo.MOVEMENT.ACCOUNT_ID, dbo.MOVEMENT.CATEGORY_ID, dbo.MOVEMENT.USER_ID, dbo.[USER].NAME AS USER_NAME, 
                         dbo.CATEGORY.NAME AS CATEGORY_NAME, dbo.ACCOUNT.NAME AS ACCOUNT_NAME, dbo.REASON.ID AS REASON_ID, 
                         dbo.REASON.TEXT AS REASON_TEXT
FROM            dbo.ACCOUNT INNER JOIN
                         dbo.CATEGORY ON dbo.ACCOUNT.ID = dbo.CATEGORY.ID INNER JOIN
                         dbo.MOVEMENT ON dbo.ACCOUNT.ID = dbo.MOVEMENT.ACCOUNT_ID AND dbo.CATEGORY.ID = dbo.MOVEMENT.CATEGORY_ID INNER JOIN
                         dbo.[USER] ON dbo.MOVEMENT.USER_ID = dbo.[USER].ID INNER JOIN
                         dbo.REASON ON dbo.MOVEMENT.REASON_ID = dbo.REASON.ID

GO
ALTER TABLE [dbo].[ACCOUNTUSER]  WITH CHECK ADD  CONSTRAINT [FK_ACCOUNTUSER_ACCOUNT1] FOREIGN KEY([ACCOUNT_ID])
REFERENCES [dbo].[ACCOUNT] ([ID])
GO
ALTER TABLE [dbo].[ACCOUNTUSER] CHECK CONSTRAINT [FK_ACCOUNTUSER_ACCOUNT1]
GO
ALTER TABLE [dbo].[ACCOUNTUSER]  WITH CHECK ADD  CONSTRAINT [FK_ACCOUNTUSER_USER] FOREIGN KEY([USER_ID])
REFERENCES [dbo].[USER] ([ID])
GO
ALTER TABLE [dbo].[ACCOUNTUSER] CHECK CONSTRAINT [FK_ACCOUNTUSER_USER]
GO
ALTER TABLE [dbo].[MOVEMENT]  WITH CHECK ADD  CONSTRAINT [FK_MOVEMENT_ACCOUNT] FOREIGN KEY([ACCOUNT_ID])
REFERENCES [dbo].[ACCOUNT] ([ID])
GO
ALTER TABLE [dbo].[MOVEMENT] CHECK CONSTRAINT [FK_MOVEMENT_ACCOUNT]
GO
ALTER TABLE [dbo].[MOVEMENT]  WITH CHECK ADD  CONSTRAINT [FK_MOVEMENT_CATEGORY] FOREIGN KEY([CATEGORY_ID])
REFERENCES [dbo].[CATEGORY] ([ID])
GO
ALTER TABLE [dbo].[MOVEMENT] CHECK CONSTRAINT [FK_MOVEMENT_CATEGORY]
GO
ALTER TABLE [dbo].[MOVEMENT]  WITH CHECK ADD  CONSTRAINT [FK_MOVEMENT_REASON] FOREIGN KEY([REASON_ID])
REFERENCES [dbo].[REASON] ([ID])
GO
ALTER TABLE [dbo].[MOVEMENT] CHECK CONSTRAINT [FK_MOVEMENT_REASON]
GO
ALTER TABLE [dbo].[MOVEMENT]  WITH CHECK ADD  CONSTRAINT [FK_MOVEMENT_USER] FOREIGN KEY([USER_ID])
REFERENCES [dbo].[USER] ([ID])
GO
ALTER TABLE [dbo].[MOVEMENT] CHECK CONSTRAINT [FK_MOVEMENT_USER]
GO
ALTER TABLE [dbo].[RIGHTUSER]  WITH CHECK ADD  CONSTRAINT [FK_RIGHTUSER_RIGHT] FOREIGN KEY([RIGHT_ID])
REFERENCES [dbo].[RIGHT] ([ID])
GO
ALTER TABLE [dbo].[RIGHTUSER] CHECK CONSTRAINT [FK_RIGHTUSER_RIGHT]
GO
ALTER TABLE [dbo].[RIGHTUSER]  WITH CHECK ADD  CONSTRAINT [FK_RIGHTUSER_USER] FOREIGN KEY([USER_ID])
REFERENCES [dbo].[USER] ([ID])
GO
ALTER TABLE [dbo].[RIGHTUSER] CHECK CONSTRAINT [FK_RIGHTUSER_USER]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ACCOUNT"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 101
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CATEGORY"
            Begin Extent = 
               Top = 6
               Left = 243
               Bottom = 101
               Right = 410
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MOVEMENT"
            Begin Extent = 
               Top = 6
               Left = 448
               Bottom = 135
               Right = 615
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "USER"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 231
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "REASON"
            Begin Extent = 
               Top = 6
               Left = 653
               Bottom = 101
               Right = 820
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1995
         Tabl' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_OVERVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'e = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_OVERVIEW'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_OVERVIEW'
GO
