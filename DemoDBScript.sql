USE [master]
GO
/****** Object:  Database [DEMO]    Script Date: 26/08/2021 21:15:38 ******/
CREATE DATABASE [DEMO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DEMO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MT2SQL\MSSQL\DATA\DEMO.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DEMO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MT2SQL\MSSQL\DATA\DEMO_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DEMO] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DEMO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DEMO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DEMO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DEMO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DEMO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DEMO] SET ARITHABORT OFF 
GO
ALTER DATABASE [DEMO] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DEMO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DEMO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DEMO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DEMO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DEMO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DEMO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DEMO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DEMO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DEMO] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DEMO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DEMO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DEMO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DEMO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DEMO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DEMO] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DEMO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DEMO] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DEMO] SET  MULTI_USER 
GO
ALTER DATABASE [DEMO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DEMO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DEMO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DEMO] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DEMO] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DEMO]
GO
/****** Object:  User [udemo]    Script Date: 26/08/2021 21:15:39 ******/
CREATE USER [udemo] FOR LOGIN [udemo] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [udemo]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [udemo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [udemo]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [udemo]
GO
/****** Object:  Table [dbo].[tbl_BANGDIEM]    Script Date: 26/08/2021 21:15:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_BANGDIEM](
	[MaLop] [varchar](12) NOT NULL,
	[MaSV] [varchar](10) NOT NULL,
	[Diem] [varchar](50) NULL,
 CONSTRAINT [PK_BANGDIEM] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC,
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_KHOA]    Script Date: 26/08/2021 21:15:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_KHOA](
	[MaKhoa] [varchar](8) NOT NULL,
	[TenKhoa] [nvarchar](30) NULL,
 CONSTRAINT [PK_KHOA] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_LOPHP]    Script Date: 26/08/2021 21:15:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_LOPHP](
	[MaNV] [int] NULL,
	[MaLop] [varchar](12) NOT NULL,
	[TenLop] [nvarchar](50) NULL,
 CONSTRAINT [PK_LOPHP] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_SINHVIEN]    Script Date: 26/08/2021 21:15:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_SINHVIEN](
	[MaSV] [varchar](10) NOT NULL,
	[TenSV] [nvarchar](52) NULL,
 CONSTRAINT [PK_SINHVIEN] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_USER]    Script Date: 26/08/2021 21:15:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_USER](
	[MaNV] [int] IDENTITY(1001,1) NOT NULL,
	[TenNV] [nvarchar](52) NULL,
	[Email] [varchar](20) NULL,
	[SDT] [varchar](11) NULL,
	[ChucVu] [int] NULL,
	[MaKhoa] [varchar](8) NULL,
	[TenDN] [varchar](16) NULL,
	[MatKhau] [varbinary](max) NOT NULL,
	[PubKey] [varbinary](max) NULL,
	[PrivKey] [varbinary](max) NULL,
	[WarningStatus] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM101', N'4301123123', N'StPKCvpE8wH6uB0WNVC8Ng==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM101', N'4301456456', N'Kt/ODbpi0ky4SgCVWMb+eA==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM101', N'4402001231', N'vAjtjZITUxWe0T3FmtMeog==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM101', N'4504876985', N'rxBCnJnvKXLtt/KYGAwIIA==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM202', N'4301123123', N'j/1I/813nYFhWKnTbEtxHQ==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM202', N'4301456456', N'aXXfIOEcoBSLGrLxqTajdQ==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM202', N'4402001231', N'vAjtjZITUxWe0T3FmtMeog==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'COM202', N'4402001239', N'8vu294lp2QHqrJ/60E1APw==')
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'TAM101', N'4402001231', NULL)
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'TAM101', N'4402001236', NULL)
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'TAM101', N'4402001239', NULL)
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'TAM301 ', N'4301456456', NULL)
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'TAM301', N'4501187472', NULL)
INSERT [dbo].[tbl_BANGDIEM] ([MaLop], [MaSV], [Diem]) VALUES (N'TAM301', N'4502876546', NULL)
INSERT [dbo].[tbl_KHOA] ([MaKhoa], [TenKhoa]) VALUES (N'CNTT', N'Công nghệ thông tin')
INSERT [dbo].[tbl_KHOA] ([MaKhoa], [TenKhoa]) VALUES (N'TAMLY', N'Tâm lý')
INSERT [dbo].[tbl_LOPHP] ([MaNV], [MaLop], [TenLop]) VALUES (1006, N'COM101', N'Lập trình căn bản')
INSERT [dbo].[tbl_LOPHP] ([MaNV], [MaLop], [TenLop]) VALUES (1006, N'COM202', N'Lập trình nâng cao')
INSERT [dbo].[tbl_LOPHP] ([MaNV], [MaLop], [TenLop]) VALUES (1005, N'TAM101', N'Tâm lý học đại cương')
INSERT [dbo].[tbl_LOPHP] ([MaNV], [MaLop], [TenLop]) VALUES (1005, N'TAM301', N'Tâm lý tổng quát')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4301123123', N'Nguyễn Văn A')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4301456456', N'Trần Thị B')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4402001231', N'Trịnh Kim Hoàn')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4402001234', N'Đào Duy Nghĩa')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4402001236', N'Phan Hải Yến')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4402001239', N'Trần Huy Nghĩa')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4501187472', N'Lê Thị Ý Nhi')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4502876546', N'Trần Văn Hải')
INSERT [dbo].[tbl_SINHVIEN] ([MaSV], [TenSV]) VALUES (N'4504876985', N'Đỗ Hoàng Anh')
SET IDENTITY_INSERT [dbo].[tbl_USER] ON 

INSERT [dbo].[tbl_USER] ([MaNV], [TenNV], [Email], [SDT], [ChucVu], [MaKhoa], [TenDN], [MatKhau], [PubKey], [PrivKey], [WarningStatus]) VALUES (1001, N'Administrator', NULL, NULL, 0, N'CNTT', N'admin', 0xE99A18C428CB38D5F260853678922E03, NULL, NULL, 0)
INSERT [dbo].[tbl_USER] ([MaNV], [TenNV], [Email], [SDT], [ChucVu], [MaKhoa], [TenDN], [MatKhau], [PubKey], [PrivKey], [WarningStatus]) VALUES (1004, N'Trần', N'', N'', 2, N'CNTT', N'test', 0xC4CA4238A0B923820DCC509A6F75849B, NULL, NULL, 1)
INSERT [dbo].[tbl_USER] ([MaNV], [TenNV], [Email], [SDT], [ChucVu], [MaKhoa], [TenDN], [MatKhau], [PubKey], [PrivKey], [WarningStatus]) VALUES (1005, N'Nguyễn Lan Anh', N'', N'', 1, N'TAMLY', N'giaovien2', 0xC4CA4238A0B923820DCC509A6F75849B, 0x6765444F434F58426257385641794B3141546171386971732F30486161667A4D414A2F33474A744A6E79303D, 0x59484E6B5A6D51784D444131414141414141414141414141414141414141414141414141414141414145413D, 3)
INSERT [dbo].[tbl_USER] ([MaNV], [TenNV], [Email], [SDT], [ChucVu], [MaKhoa], [TenDN], [MatKhau], [PubKey], [PrivKey], [WarningStatus]) VALUES (1006, N'Lê Kim Long', N'', N'', 1, N'CNTT', N'giaovien1', 0xC4CA4238A0B923820DCC509A6F75849B, 0x794A346B7852545050424964432B63754C2F4E6345394A516552377146344B473558524F2F2B49354731513D, 0x614739755A327470625445774D4459414141414141414141414141414141414141414141414141414145413D, 0)
INSERT [dbo].[tbl_USER] ([MaNV], [TenNV], [Email], [SDT], [ChucVu], [MaKhoa], [TenDN], [MatKhau], [PubKey], [PrivKey], [WarningStatus]) VALUES (1007, N'Ngô Thị Hà', N'', N'', 2, N'CNTT', N'giaovu1', 0xC4CA4238A0B923820DCC509A6F75849B, 0x41616F663633354A68577948342F496E38342B6130735168785246425363493056595A73594B51347254383D, 0x51453555564550447447356E4947356E614F473768794230614D4F30626D636764476C75414141414145413D, 0)
INSERT [dbo].[tbl_USER] ([MaNV], [TenNV], [Email], [SDT], [ChucVu], [MaKhoa], [TenDN], [MatKhau], [PubKey], [PrivKey], [WarningStatus]) VALUES (1008, N'Phan Thị Lý', N'', N'', 2, N'TAMLY', N'giaovu2', 0xC4CA4238A0B923820DCC509A6F75849B, 0x7041644D444C4464346666423156494848366B43474E7748675075672B575A366D6A496E525A584E4A31343D, 0x5545464E54466C5577364A7449477A447651414141414141414141414141414141414141414141414145413D, 0)
SET IDENTITY_INSERT [dbo].[tbl_USER] OFF
ALTER TABLE [dbo].[tbl_BANGDIEM]  WITH CHECK ADD  CONSTRAINT [FK_BANGDIEM_SINHVIEN] FOREIGN KEY([MaSV])
REFERENCES [dbo].[tbl_SINHVIEN] ([MaSV])
GO
ALTER TABLE [dbo].[tbl_BANGDIEM] CHECK CONSTRAINT [FK_BANGDIEM_SINHVIEN]
GO
ALTER TABLE [dbo].[tbl_LOPHP]  WITH CHECK ADD  CONSTRAINT [FK_LOPHP_USER_MaNV] FOREIGN KEY([MaNV])
REFERENCES [dbo].[tbl_USER] ([MaNV])
GO
ALTER TABLE [dbo].[tbl_LOPHP] CHECK CONSTRAINT [FK_LOPHP_USER_MaNV]
GO
ALTER TABLE [dbo].[tbl_USER]  WITH CHECK ADD  CONSTRAINT [FK_USER_KHOA_MaKhoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[tbl_KHOA] ([MaKhoa])
GO
ALTER TABLE [dbo].[tbl_USER] CHECK CONSTRAINT [FK_USER_KHOA_MaKhoa]
GO
USE [master]
GO
ALTER DATABASE [DEMO] SET  READ_WRITE 
GO
