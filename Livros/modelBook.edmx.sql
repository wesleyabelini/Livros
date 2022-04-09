
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/23/2021 21:27:30
-- Generated from EDMX file: C:\Livros\Livros\modelBook.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Livro];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Book];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Book'
CREATE TABLE [dbo].[Book] (
    [id] int IDENTITY(1,1) NOT NULL,
    [NomeLivro] varchar(500)  NOT NULL,
    [NomeAutor] varchar(100)  NOT NULL,
    [Descricao] varchar(max)  NOT NULL,
    [Categoria] varchar(30)  NULL,
    [URL_CAPA] varchar(500)  NOT NULL,
    [URL_BOOK] varchar(500)  NOT NULL,
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Book'
ALTER TABLE [dbo].[Book]
ADD CONSTRAINT [PK_Book]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------