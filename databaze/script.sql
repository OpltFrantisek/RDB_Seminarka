USE [master]
GO
/****** Object:  Database [RDB_Seminarka]    Script Date: 26.04.2019 11:07:48 ******/
CREATE DATABASE [RDB_Seminarka]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RDB_Seminarka', FILENAME = N'/var/opt/mssql/data/RDB_Seminarka.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RDB_Seminarka_log', FILENAME = N'/var/opt/mssql/data/RDB_Seminarka_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RDB_Seminarka] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RDB_Seminarka].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RDB_Seminarka] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET ARITHABORT OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RDB_Seminarka] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RDB_Seminarka] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RDB_Seminarka] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RDB_Seminarka] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET RECOVERY FULL 
GO
ALTER DATABASE [RDB_Seminarka] SET  MULTI_USER 
GO
ALTER DATABASE [RDB_Seminarka] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RDB_Seminarka] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RDB_Seminarka] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RDB_Seminarka] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RDB_Seminarka] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RDB_Seminarka] SET QUERY_STORE = OFF
GO
USE [RDB_Seminarka]
GO
/****** Object:  Table [dbo].[Autobus]    Script Date: 26.04.2019 11:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autobus](
	[spz] [nvarchar](50) NOT NULL,
	[znacka] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[spz] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_1]    Script Date: 26.04.2019 11:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
SELECT        dbo.Autobus.*, spz AS Expr1, znacka AS Expr2
FROM            dbo.Autobus
GO
/****** Object:  Table [dbo].[Hash]    Script Date: 26.04.2019 11:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hash](
	[hash] [varchar](200) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Hash] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jizda]    Script Date: 26.04.2019 11:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jizda](
	[linka] [nvarchar](50) NOT NULL,
	[spz] [nvarchar](50) NOT NULL,
	[cislo_rp] [nvarchar](50) NOT NULL,
	[cas] [datetime] NOT NULL,
 CONSTRAINT [PK__Jizda__61309BF31579B2C3] PRIMARY KEY CLUSTERED 
(
	[linka] ASC,
	[cas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jizdenka]    Script Date: 26.04.2019 11:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jizdenka](
	[linka] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[cas] [datetime] NOT NULL,
	[cislo] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK__Jizdenka__56F14500B15A12C1] PRIMARY KEY CLUSTERED 
(
	[cislo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Klient]    Script Date: 26.04.2019 11:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Klient](
	[email] [nvarchar](50) NOT NULL,
	[jmeno] [nvarchar](50) NOT NULL,
	[prijmeni] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kontakt]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kontakt](
	[hodnota] [nvarchar](50) NOT NULL,
	[typ] [nvarchar](50) NOT NULL,
	[cislo_rp] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[hodnota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lokalita]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lokalita](
	[nazev] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[nazev] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mezizastavka]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mezizastavka](
	[nazev] [nvarchar](50) NOT NULL,
	[linka] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[nazev] ASC,
	[linka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ridic]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ridic](
	[cislo_rp] [nvarchar](50) NOT NULL,
	[jmeno] [nvarchar](50) NOT NULL,
	[prijmeni] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cislo_rp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trasy]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trasy](
	[linka] [nvarchar](50) NOT NULL,
	[odkud] [nvarchar](50) NOT NULL,
	[kam] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[linka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypKontaktu]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypKontaktu](
	[typ] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[typ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Znacka]    Script Date: 26.04.2019 11:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Znacka](
	[znacka] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[znacka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Autobus]  WITH CHECK ADD FOREIGN KEY([znacka])
REFERENCES [dbo].[Znacka] ([znacka])
GO
ALTER TABLE [dbo].[Jizda]  WITH CHECK ADD  CONSTRAINT [FK__Jizda__cislo_rp__74AE54BC] FOREIGN KEY([cislo_rp])
REFERENCES [dbo].[Ridic] ([cislo_rp])
GO
ALTER TABLE [dbo].[Jizda] CHECK CONSTRAINT [FK__Jizda__cislo_rp__74AE54BC]
GO
ALTER TABLE [dbo].[Jizda]  WITH CHECK ADD  CONSTRAINT [FK__Jizda__linka__70DDC3D8] FOREIGN KEY([linka])
REFERENCES [dbo].[Trasy] ([linka])
GO
ALTER TABLE [dbo].[Jizda] CHECK CONSTRAINT [FK__Jizda__linka__70DDC3D8]
GO
ALTER TABLE [dbo].[Jizda]  WITH CHECK ADD  CONSTRAINT [FK__Jizda__spz__72C60C4A] FOREIGN KEY([spz])
REFERENCES [dbo].[Autobus] ([spz])
GO
ALTER TABLE [dbo].[Jizda] CHECK CONSTRAINT [FK__Jizda__spz__72C60C4A]
GO
ALTER TABLE [dbo].[Jizdenka]  WITH CHECK ADD  CONSTRAINT [FK__Jizdenka__71D1E811] FOREIGN KEY([linka], [cas])
REFERENCES [dbo].[Jizda] ([linka], [cas])
GO
ALTER TABLE [dbo].[Jizdenka] CHECK CONSTRAINT [FK__Jizdenka__71D1E811]
GO
ALTER TABLE [dbo].[Jizdenka]  WITH CHECK ADD  CONSTRAINT [FK__Jizdenka__email__778AC167] FOREIGN KEY([email])
REFERENCES [dbo].[Klient] ([email])
GO
ALTER TABLE [dbo].[Jizdenka] CHECK CONSTRAINT [FK__Jizdenka__email__778AC167]
GO
ALTER TABLE [dbo].[Kontakt]  WITH CHECK ADD FOREIGN KEY([cislo_rp])
REFERENCES [dbo].[Ridic] ([cislo_rp])
GO
ALTER TABLE [dbo].[Kontakt]  WITH CHECK ADD FOREIGN KEY([typ])
REFERENCES [dbo].[TypKontaktu] ([typ])
GO
ALTER TABLE [dbo].[Mezizastavka]  WITH CHECK ADD FOREIGN KEY([linka])
REFERENCES [dbo].[Trasy] ([linka])
GO
ALTER TABLE [dbo].[Mezizastavka]  WITH CHECK ADD FOREIGN KEY([nazev])
REFERENCES [dbo].[Lokalita] ([nazev])
GO
ALTER TABLE [dbo].[Trasy]  WITH CHECK ADD FOREIGN KEY([kam])
REFERENCES [dbo].[Lokalita] ([nazev])
GO
ALTER TABLE [dbo].[Trasy]  WITH CHECK ADD FOREIGN KEY([odkud])
REFERENCES [dbo].[Lokalita] ([nazev])
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
         Begin Table = "Autobus"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 245
               Right = 442
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
USE [master]
GO
ALTER DATABASE [RDB_Seminarka] SET  READ_WRITE 
GO
