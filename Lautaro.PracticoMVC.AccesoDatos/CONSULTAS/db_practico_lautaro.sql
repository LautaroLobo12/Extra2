--create database

ALTER DATABASE [db_practico_lautaro] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_practico_lautaro] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_practico_lautaro] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_practico_lautaro] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_practico_lautaro] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET RECOVERY FULL 
GO
ALTER DATABASE [db_practico_lautaro] SET  MULTI_USER 
GO
ALTER DATABASE [db_practico_lautaro] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_practico_lautaro] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_practico_lautaro] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_practico_lautaro] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_practico_lautaro] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_practico_lautaro', N'ON'
GO
USE [db_practico_lautaro]
GO
/****** Object:  Table [Clientes]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Clientes](
	[Codigo] [int] IDENTITY(1000,1) NOT NULL,
	[RazonSocial] [varchar](100) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[IdUsuario] [bigint] NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [DetallesPedidos]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DetallesPedidos](
	[NumeroPedido] [int] NOT NULL,
	[NumeroItem] [int] NOT NULL,
	[CodigoProducto] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_DetallesPedidos] PRIMARY KEY CLUSTERED 
(
	[NumeroPedido] ASC,
	[NumeroItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Marcas]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Marcas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Marcas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pedidos]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pedidos](
	[NumeroPedido] [int] IDENTITY(1,1) NOT NULL,
	[CodigoCliente] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Observacion] [nvarchar](100) NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[NumeroPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Productos]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Productos](
	[Codigo] [int] IDENTITY(1000,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](255) NOT NULL,
	[IdMarca] [int] NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[Activo] [bit] NOT NULL,
	[UrlImange] [varchar](255) NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Roles]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Roles](
	[Id] [varchar](5) NOT NULL,
	[Descripcion] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_RolesUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Usuarios]    Script Date: 30/6/2019 02:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Usuarios](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdRol] [varchar](5) NOT NULL,
	[Usuario] [nvarchar](10) NOT NULL,
	[Nombre] [nvarchar](70) NOT NULL,
	[Apellido] [nvarchar](70) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PasswordSalt] [nvarchar](255) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Clientes] ON 

INSERT [Clientes] ([Codigo], [RazonSocial], [FechaCreacion], [IdUsuario]) VALUES (1000, N'KUN SA', CAST(N'2019-06-28T22:12:36.000' AS DateTime), 2)
INSERT [Clientes] ([Codigo], [RazonSocial], [FechaCreacion], [IdUsuario]) VALUES (1001, N'LAUTA SRL', CAST(N'2019-06-28T22:12:36.000' AS DateTime), 3)
SET IDENTITY_INSERT [Clientes] OFF
INSERT [DetallesPedidos] ([NumeroPedido], [NumeroItem], [CodigoProducto], [Cantidad], [PrecioUnitario]) VALUES (1, 1, 1001, 1, CAST(39999.00 AS Decimal(18, 2)))
INSERT [DetallesPedidos] ([NumeroPedido], [NumeroItem], [CodigoProducto], [Cantidad], [PrecioUnitario]) VALUES (1, 2, 1002, 2, CAST(2499.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [Marcas] ON 

INSERT [Marcas] ([Id], [Nombre]) VALUES (1, N'SONY')
INSERT [Marcas] ([Id], [Nombre]) VALUES (2, N'Nintendo')
INSERT [Marcas] ([Id], [Nombre]) VALUES (3, N'Logitech')
SET IDENTITY_INSERT [Marcas] OFF
SET IDENTITY_INSERT [Pedidos] ON 

INSERT [Pedidos] ([NumeroPedido], [CodigoCliente], [Fecha], [Observacion]) VALUES (1, 1000, CAST(N'2019-06-29T00:00:00.000' AS DateTime), N'Primer pedido')
SET IDENTITY_INSERT [Pedidos] OFF
SET IDENTITY_INSERT [Productos] ON 

INSERT [Productos] ([Codigo], [Nombre], [Descripcion], [IdMarca], [PrecioUnitario], [Activo], [UrlImange]) VALUES (1000, N'Playstation 4 c/FIFA19', N'Playstation 4 | 1TB | 1 Joystick | FIFA 19', 1, CAST(29999.00 AS Decimal(18, 2)), 1, N'https://http2.mlstatic.com/consola-play-station-4-joystick-fifa-19-ps4-2050d-D_NQ_NP_941877-MLA31116375176_062019-F.jpg')
INSERT [Productos] ([Codigo], [Nombre], [Descripcion], [IdMarca], [PrecioUnitario], [Activo], [UrlImange]) VALUES (1001, N'Playstation 4 Pro', N'Playstation 4 Pro | 1 TB | 1 Joystick | 4K', 1, CAST(39999.00 AS Decimal(18, 2)), 1, N'https://media.4rgos.it/i/Argos/8361590_R_Z001A?w=500&h=500&qlt=70')
INSERT [Productos] ([Codigo], [Nombre], [Descripcion], [IdMarca], [PrecioUnitario], [Activo], [UrlImange]) VALUES (1002, N'Mouse G502', N'Mouse Logitech G502 | RGB | 12000 DPI', 3, CAST(2499.00 AS Decimal(18, 2)), 1, N'https://ae01.alicdn.com/kf/HTB1w5VBSXXXXXccXpXXq6xXFXXXp/Logitech-G502-Proteus-juegos-rat-n-ratones.jpg')
SET IDENTITY_INSERT [Productos] OFF
INSERT [Roles] ([Id], [Descripcion]) VALUES (N'ADMIN', N'Administrador')
INSERT [Roles] ([Id], [Descripcion]) VALUES (N'CLI', N'Cliente')
SET IDENTITY_INSERT [Usuarios] ON 

INSERT [Usuarios] ([Id], [IdRol], [Usuario], [Nombre], [Apellido], [Password], [PasswordSalt], [FechaCreacion], [Activo]) VALUES (1, N'ADMIN', N'admin', N'Lionel', N'Messi', N'123', N'COMPLETAR', CAST(N'2019-06-28T21:58:03.000' AS DateTime), 1)
INSERT [Usuarios] ([Id], [IdRol], [Usuario], [Nombre], [Apellido], [Password], [PasswordSalt], [FechaCreacion], [Activo]) VALUES (2, N'CLI', N'kun.aguero', N'Sergio', N'Agüero', N'123', N'COMPLETAR', CAST(N'2019-06-28T21:59:48.000' AS DateTime), 1)
INSERT [Usuarios] ([Id], [IdRol], [Usuario], [Nombre], [Apellido], [Password], [PasswordSalt], [FechaCreacion], [Activo]) VALUES (3, N'CLI', N'lautaro', N'Lautaro', N'Martinez', N'123', N'COMPLETAR', CAST(N'2019-06-28T22:00:34.000' AS DateTime), 1)
SET IDENTITY_INSERT [Usuarios] OFF
/****** Object:  Index [IX_DetallesPedidos]    Script Date: 30/6/2019 02:31:02 ******/
ALTER TABLE [DetallesPedidos] ADD  CONSTRAINT [IX_DetallesPedidos] UNIQUE NONCLUSTERED 
(
	[NumeroPedido] ASC,
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Clientes] ADD  CONSTRAINT [DF__Clientes__FechaC__4D94879B]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [Usuarios] ADD  CONSTRAINT [DF_Usuarios_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [Usuarios] ([Id])
GO
ALTER TABLE [Clientes] CHECK CONSTRAINT [FK_Clientes_Usuarios]
GO
ALTER TABLE [DetallesPedidos]  WITH CHECK ADD  CONSTRAINT [FK_DetallesPedidos_DetallesPedidos] FOREIGN KEY([NumeroPedido])
REFERENCES [Pedidos] ([NumeroPedido])
GO
ALTER TABLE [DetallesPedidos] CHECK CONSTRAINT [FK_DetallesPedidos_DetallesPedidos]
GO
ALTER TABLE [DetallesPedidos]  WITH CHECK ADD  CONSTRAINT [FK_DetallesPedidos_Productos] FOREIGN KEY([CodigoProducto])
REFERENCES [Productos] ([Codigo])
GO
ALTER TABLE [DetallesPedidos] CHECK CONSTRAINT [FK_DetallesPedidos_Productos]
GO
ALTER TABLE [Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Clientes] FOREIGN KEY([CodigoCliente])
REFERENCES [Clientes] ([Codigo])
GO
ALTER TABLE [Pedidos] CHECK CONSTRAINT [FK_Pedidos_Clientes]
GO
ALTER TABLE [Productos]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Marcas] FOREIGN KEY([IdMarca])
REFERENCES [Marcas] ([Id])
GO
ALTER TABLE [Productos] CHECK CONSTRAINT [FK_Producto_Marcas]
GO
ALTER TABLE [Usuarios]  WITH CHECK ADD  CONSTRAINT [Usuarios_RolesUsuario_FK] FOREIGN KEY([IdRol])
REFERENCES [Roles] ([Id])
GO
ALTER TABLE [Usuarios] CHECK CONSTRAINT [Usuarios_RolesUsuario_FK]
GO
