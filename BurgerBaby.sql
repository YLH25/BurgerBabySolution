create database [BurgerBaby]
GO
USE [BurgerBaby]
GO
/****** Object:  Table [dbo].[Imgs]    Script Date: 2024/12/24 下午 07:44:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imgs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NOT NULL,
	[ImgName] [nvarchar](200) NOT NULL,
	[SaveName] [nvarchar](200) NOT NULL,
	[IsCover] [bit] NULL,
 CONSTRAINT [PK__Imgs__3214EC07B2DC4426] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 2024/12/24 下午 07:44:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Id] [int] NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK__Members__3214EC0751E6DED8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2024/12/24 下午 07:44:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Intro] [nvarchar](1000) NULL,
 CONSTRAINT [PK__Products__3214EC074F8D0BBE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2024/12/24 下午 07:44:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Imgs] ON 
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3032, 3008, N'雙層牛肉吉事堡.jpg', N'7465fb52-c620-45ea-98e5-5372d2198672.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3033, 2, N'牛五花.jpg', N'c5cb7439-b0d3-4210-9e03-2f9b3fefbf76.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3034, 3, N'功夫雞翅.jpg', N'7375b19b-2801-4dd2-8638-7af3efe500a7.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3035, 5, N'經典凱薩.jpg', N'5491a963-fd55-4d07-b872-eaf84b2cd567.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3036, 1004, N'大麥克.jpg', N'26ffa601-b168-420e-9b64-4791f1373e10.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3039, 2005, N'珍珠奶茶.jpg', N'c2eb8f0d-0203-41ae-a1fb-f12ee83b6be5.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3040, 3004, N'家鄉薯條.jpeg', N'14ba1b1e-3618-4f61-b79f-1d68a0c4dbd5.jpeg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3041, 3006, N'麥香鷄.jpg', N'ae18fe80-165f-4ed7-aba5-cbc0f5826b20.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3042, 3007, N'嫩煎鷄腿堡.jpg', N'a5d139b9-0e84-4b58-a515-5340eaa3caab.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3043, 3009, N'黑胡椒牛排.jpg', N'5bed6b64-27d5-429b-be0e-be2c0e8f5edb.jpg', 1)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3048, 2004, N'玉米濃湯.jpg', N'88bdd592-9fe7-40b4-82ca-921032763a44.jpg', 0)
GO
INSERT [dbo].[Imgs] ([Id], [Product_Id], [ImgName], [SaveName], [IsCover]) VALUES (3049, 2004, N'玉米濃湯.jpg', N'ae63050e-fca6-4aab-b536-e5450efc96fb.jpg', 1)
GO
SET IDENTITY_INSERT [dbo].[Imgs] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 
GO
INSERT [dbo].[Members] ([Id], [Role_Id], [Phone], [Address], [Email], [Password], [Name]) VALUES (3016, 1, N'09123456789', N'台北市士林區重慶北路四段123號', N'111@gmail.com', N'111', N'管理員')
GO
INSERT [dbo].[Members] ([Id], [Role_Id], [Phone], [Address], [Email], [Password], [Name]) VALUES (3018, 2, N'09223456789', N'台北市士林區重慶北路四段113號', N'123@123', N'123', N'會員')
GO
INSERT [dbo].[Members] ([Id], [Role_Id], [Phone], [Address], [Email], [Password], [Name]) VALUES (3019, 1, N'1', N'1', N'111', N'111', N'測試')
GO
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (2, N'牛五花', CAST(180 AS Decimal(18, 0)), N'精選多汁的牛五花肉，擁有豐富的大理石花紋，燒烤至表面微焦，鎖住內部的肉汁，呈現出外脆內嫩的完美口感。這道菜的調味以特製醬料為基底，既能提升肉質的鮮美，也能平衡油脂的濃郁感。每一口咀嚼時，牛肉的鮮香和醬料的濃郁交織在一起，口感層次分明，無論是搭配米飯還是蔬菜，都能增添食欲。這款牛五花無論是作為主餐或配菜都極為合適，讓您在每一餐中享受美味與飽足感。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (3, N'功夫雞翅', CAST(99 AS Decimal(18, 0)), N'這道功夫雞翅以酥脆的外皮和鮮嫩多汁的肉質著稱，經過獨特醃製過程，吸收了各種香料與調味料，使雞翅的風味更加豐富。經過精細的炸製，外層酥脆金黃，內部卻保持著柔軟多汁的口感。這道雞翅可以搭配不同的調味醬，如辣味、蜂蜜芥末等，帶來多層次的味覺享受。無論是當作小吃還是與其他主餐搭配，都是聚會或日常餐點中的受歡迎選擇。每一口咬下去，都能感受到濃郁的香氣和完美的口感。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (5, N'經典凱薩', CAST(75 AS Decimal(18, 0)), N'經典的凱薩沙拉以新鮮脆口的生菜為基底，配上烤至金黃的麵包丁，口感既酥脆又不失爽脆感。再加入厚切的帕瑪森起司，增添沙拉的風味與濃郁口感。這道沙拉的亮點在於其醬料，選用經典的凱薩醬，濃郁的奶香和些微的蒜味將整道沙拉的味道提升到新高度。這款沙拉不僅適合作為開胃菜，也能作為健康的輕食，搭配燒烤、漢堡等主餐一起食用，總能帶來絕佳的口感平衡。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (1004, N'大麥克', CAST(120 AS Decimal(18, 0)), N'大麥克漢堡是速食界的經典之作，由兩片厚實的牛肉餅夾著濃郁的切達起司、鮮嫩的生菜、酸黃瓜和特製醬料，形成完美的味覺搭配。每一口都能感受到豐滿的牛肉香氣，並且與爽脆的蔬菜和醬料形成鮮明對比。這款大麥克不僅是肉食愛好者的最愛，也適合各種口味偏好的人群。豐富的層次感讓這款漢堡既能滿足飽足感，又不失多樣的味覺體驗，是快餐店中最受歡迎的選擇之一。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (2004, N'玉米濃湯', CAST(50 AS Decimal(18, 0)), N'玉米濃湯是經典的舒適餐點，湯品的質地濃郁而絲滑，玉米粒為湯底增添了自然的甜味。奶香味十足的湯底，配上新鮮玉米粒，營造出既溫暖又滿足的口感。每一口都是順滑的享受，濃厚的奶香與玉米的清甜相得益彰，讓人不禁回味無窮。這款玉米濃湯不僅適合作為寒冷天氣中的暖身飲品，也能與各式主餐搭配，增加餐點的豐富度，是許多人喜愛的配餐選擇。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (2005, N'珍珠奶茶', CAST(60 AS Decimal(18, 0)), N'珍珠奶茶是台灣的經典飲品，融合了香濃的奶茶與Q彈的珍珠，口感層次豐富。奶茶的香氣濃郁，甜度適中，搭配著彈牙的珍珠，讓每一口都充滿愉悅的口感。珍珠的彈性讓整體飲品增添了趣味，無論是搭配餐點或是作為下午茶飲品，都極為合適。這款珍珠奶茶是許多速食店的招牌飲品，無論是炎熱的夏季還是寒冷的冬季，都能帶來滿足的味覺享受。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (3004, N'家鄉薯條', CAST(55 AS Decimal(18, 0)), N'經典的家鄉薯條外酥內軟，經過精緻炸製，外皮呈現出金黃色的酥脆感，咬下去則是綿密的薯泥口感。每根薯條均衡的鹽味提升了其風味，簡單卻極具誘惑。這款薯條可以單獨食用，或是搭配各種醬料，如番茄醬、沙拉醬、或是美乃滋，讓每一口都充滿風味。無論是搭配漢堡、炸雞，還是作為小吃，都能帶來滿足的口感，絕對是速食店中的經典小吃。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (3006, N'麥香鷄', CAST(899 AS Decimal(18, 0)), N'麥香鷄是速食店中的經典款，以雞胸肉為主，經過麥片包裹並炸至金黃酥脆，外層的酥脆口感與內部雞肉的嫩滑形成鮮明對比。這款麥香鷄搭配新鮮生菜，並使用特製的醬料，無論是醇厚的美乃滋還是微辣的醬料，都能提升其風味。每一口都擁有香脆的麥片和鮮嫩的雞肉，是快速且美味的選擇。這款麥香鷄適合與任何主餐搭配，或單獨作為輕食享用。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (3007, N'嫩煎鷄腿堡', CAST(115 AS Decimal(18, 0)), N'嫩煎雞腿堡選用鮮嫩多汁的雞腿肉，煎至外焦內嫩，帶有濃郁的香氣。雞腿肉的鮮美與生菜、番茄、酸黃瓜的清爽搭配，讓整個口感平衡又美味。特製的醬料更是點綴其中，帶來更多的層次感。這款嫩煎雞腿堡的豐富風味，無論是作為午餐還是晚餐，都能給予飽足感與滿足。簡單卻又不失精緻，讓每個食客都能享受這份美味。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (3008, N'雙層牛肉吉事堡', CAST(130 AS Decimal(18, 0)), N'雙層牛肉吉事堡由兩層厚實的牛肉餅組成，夾著融化的切達起司、酸黃瓜、生菜和特製醬料。每一口咀嚼，都能感受到牛肉的鮮香和起司的濃郁，搭配醬料的酸甜味道，讓整個漢堡層次分明，口感十足。這款雙層牛肉吉事堡非常適合肉食愛好者，豐富的牛肉和多樣的配料使它成為速食店中的明星產品。它不僅能提供飽足感，也帶來滿足的味覺享受。')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Intro]) VALUES (3009, N'黑胡椒牛排', CAST(180 AS Decimal(18, 0)), N'這款黑胡椒牛排選用上等的牛肉，煎至外焦內嫩，搭配濃烈的黑胡椒醬，增添牛肉的香氣與層次感。牛排的肉質鮮嫩多汁，每一口都散發著香濃的肉味，黑胡椒醬的辛辣與牛肉的鮮美相得益')
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'PowerUser')
GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'Member')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Members]    Script Date: 2024/12/24 下午 07:44:57 ******/
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [IX_Members] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = OFF, ALLOW_PAGE_LOCKS = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Imgs]  WITH CHECK ADD  CONSTRAINT [FK__Imgs__Product_Id__282DF8C2] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Imgs] CHECK CONSTRAINT [FK__Imgs__Product_Id__282DF8C2]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK__Members__Role_Id__22751F6C] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK__Members__Role_Id__22751F6C]
GO
