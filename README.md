# Rota de Viagem #

Escolha a rota de viagem mais barata independente da quantidade de conexões.

Para isso precisamos inserir as rotas.
 
# API

## CRUD de cadastro de ROTAS ##

* Deverá construir um endpoint de CRUD as rotas disponíveis:

Origem: GRU, Destino: BRC, Valor: 10

Origem: BRC, Destino: SCL, Valor: 5

Origem: GRU, Destino: CDG, Valor: 75

Origem: GRU, Destino: SCL, Valor: 20

Origem: GRU, Destino: ORL, Valor: 56

Origem: ORL, Destino: CDG, Valor: 5

Origem: SCL, Destino: ORL, Valor: 20

 
## Explicando ## 

Uma viajem de **GRU** para **CDG** existem as seguintes rotas:
 
1. GRU - BRC - SCL - ORL - CDG ao custo de $40

2. GRU - ORL - CDG ao custo de $61

3. GRU - CDG ao custo de $75

4. GRU - SCL - ORL - CDG ao custo de $45
 
O melhor preço é da rota **1**, apesar de mais conexões, seu valor final é menor.

O resultado da consulta deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.
 
Sendo assim, o endpoint de consulta deverá efetuar o calculo de melhor rota.
  
# FRONT-END (Opcional)

Tela para consumir a API (incluir/alterar/excluir)

Tela para consultar melhor rota

* Pode ser apenas uma tela, ou mais, fica a critério do desenvolvedor
 
 
### Projeto ###

- Interface Front-End (opcional)

	Cadastro: CRUD de Rotas

	Consulta: Deverá ter 2 campos para consulta de rota: **Origem-Destino** e exibir o resultado da consulta chamando a API

- Interface Rest (Obrigatório)

    A interface Rest deverá suportar o CRUD de rotas:

    - Manipulação de rotas, dados podendo ser persistidos em arquivo, bd local, etc...

    - Consulta de melhor rota entre dois pontos.

  Exemplo:

   Consulte a rota: GRU-CGD

  Resposta: GRU - BRC - SCL - ORL - CDG ao custo de $40

  Consulte a rota: BRC-SCL

  Resposta: BRC - SCL ao custo de $5
 
 
## Entregáveis ##

* Envie apenas o código fonte

* Preferência no github ou no OneDrive (zipado)

* Priorize/Estruturar sua aplicação seguindo as boas práticas de desenvolvimento

* Evite o uso de frameworks ou bibliotecas externas à linguagem


## API
https://localhost:7059/swagger/index.html

## FRONT
https://localhost:7209


## criação do banco

USE [master]
GO


CREATE DATABASE [RotaDeViagemDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RotaDeViagemDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RotaDeViagemDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RotaDeViagemDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RotaDeViagemDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RotaDeViagemDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [RotaDeViagemDb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET ARITHABORT OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [RotaDeViagemDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [RotaDeViagemDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [RotaDeViagemDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [RotaDeViagemDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET RECOVERY FULL 
GO

ALTER DATABASE [RotaDeViagemDb] SET  MULTI_USER 
GO

ALTER DATABASE [RotaDeViagemDb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [RotaDeViagemDb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [RotaDeViagemDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [RotaDeViagemDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [RotaDeViagemDb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [RotaDeViagemDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [RotaDeViagemDb] SET QUERY_STORE = ON
GO

ALTER DATABASE [RotaDeViagemDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [RotaDeViagemDb] SET  READ_WRITE 
GO


## script sql server tabelas

USE [RotaDeViagemDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Rotas](
	[Id] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Origem] [varchar](MAX) NOT NULL,
	[Destino] [varchar](MAX) NOT NULL,
	[Valor] [NUMERIC](10,2) NOT NULL DEFAULT (0),
	[DataCriacao] [datetime] NOT NULL DEFAULT GETDATE(),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [Escalas](
	[Id] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[IdRota] [bigint] NOT NULL,
	[Destino] [varchar](MAX) NOT NULL,
	[DataCriacao] [datetime] NOT NULL DEFAULT GETDATE(),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO