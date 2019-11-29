
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/09/2017 00:05:56
-- Generated from EDMX file: D:\WORKS\ashkabar\reklama\Reklama.Data\Entities\ReklamaEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [maximahmedov_custom_web];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Banners_BannerTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Banners] DROP CONSTRAINT [FK_Banners_BannerTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Banners_Images]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Banners] DROP CONSTRAINT [FK_Banners_Images];
GO
IF OBJECT_ID(N'[dbo].[FK_Category_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Category] DROP CONSTRAINT [FK_Category_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryParameter_CategoryParametersSection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryParameter] DROP CONSTRAINT [FK_CategoryParameter_CategoryParametersSection];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryParametersSection_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryParametersSection] DROP CONSTRAINT [FK_CategoryParametersSection_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Manufacturer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Manufacturer];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductBookmark_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductBookmark] DROP CONSTRAINT [FK_ProductBookmark_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductFeedback_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductFeedback] DROP CONSTRAINT [FK_ProductFeedback_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductImage_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductImage] DROP CONSTRAINT [FK_ProductImage_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductParameterValue_CategoryParameter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductParameterValue] DROP CONSTRAINT [FK_ProductParameterValue_CategoryParameter];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductParameterValue_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductParameterValue] DROP CONSTRAINT [FK_ProductParameterValue_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductParameterValue_Unit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductParameterValue] DROP CONSTRAINT [FK_ProductParameterValue_Unit];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopFeedback_Shop]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopFeedback] DROP CONSTRAINT [FK_ShopFeedback_Shop];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopProduct_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopProduct] DROP CONSTRAINT [FK_ShopProduct_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopProduct_Shop]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopProduct] DROP CONSTRAINT [FK_ShopProduct_Shop];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopProduct_ShopProductStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShopProduct] DROP CONSTRAINT [FK_ShopProduct_ShopProductStatus];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Banners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Banners];
GO
IF OBJECT_ID(N'[dbo].[BannerTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BannerTypes];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[CategoryParameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryParameter];
GO
IF OBJECT_ID(N'[dbo].[CategoryParametersSection]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryParametersSection];
GO
IF OBJECT_ID(N'[dbo].[Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Group];
GO
IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Manufacturer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manufacturer];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductBookmark]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductBookmark];
GO
IF OBJECT_ID(N'[dbo].[ProductFeedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductFeedback];
GO
IF OBJECT_ID(N'[dbo].[ProductImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductImage];
GO
IF OBJECT_ID(N'[dbo].[ProductParameterValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductParameterValue];
GO
IF OBJECT_ID(N'[dbo].[Shop]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Shop];
GO
IF OBJECT_ID(N'[dbo].[ShopFeedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShopFeedback];
GO
IF OBJECT_ID(N'[dbo].[ShopProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShopProduct];
GO
IF OBJECT_ID(N'[dbo].[ShopProductStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShopProductStatus];
GO
IF OBJECT_ID(N'[dbo].[Unit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Unit];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Banners'
CREATE TABLE [dbo].[Banners] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Controller] nvarchar(128)  NOT NULL,
    [Action] nvarchar(128)  NOT NULL,
    [BannerType] int  NOT NULL,
    [ImageID] int  NULL,
    [CountOfClicks] int  NULL,
    [Comments] nvarchar(max)  NULL,
    [IsShow] bit  NOT NULL,
    [Link] nvarchar(500)  NULL
);
GO

-- Creating table 'BannerTypes'
CREATE TABLE [dbo].[BannerTypes] (
    [ID] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Desc] nvarchar(200)  NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ImagePath] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'Manufacturer'
CREATE TABLE [dbo].[Manufacturer] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [IsPopular] bit  NOT NULL
);
GO

-- Creating table 'ProductBookmark'
CREATE TABLE [dbo].[ProductBookmark] (
    [ProductID] bigint  NOT NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'ProductFeedback'
CREATE TABLE [dbo].[ProductFeedback] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [ProductID] bigint  NOT NULL,
    [Comment] nvarchar(1000)  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO

-- Creating table 'ProductImage'
CREATE TABLE [dbo].[ProductImage] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [ProductID] bigint  NOT NULL,
    [ImageName] nvarchar(256)  NOT NULL,
    [IsTitular] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Shop'
CREATE TABLE [dbo].[Shop] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [CityID] int  NULL,
    [Debt] float  NOT NULL,
    [Logo] nvarchar(256)  NULL,
    [Site] nvarchar(256)  NULL,
    [Title] nvarchar(64)  NOT NULL,
    [Phone] nvarchar(256)  NULL,
    [FullTitle] nvarchar(128)  NOT NULL,
    [CompanyName] nvarchar(128)  NOT NULL,
    [Description] nvarchar(1000)  NULL,
    [IsActive] bit  NOT NULL,
    [Monday] bit  NOT NULL,
    [Tuesday] bit  NOT NULL,
    [Wednesday] bit  NOT NULL,
    [Thursday] bit  NOT NULL,
    [Friday] bit  NOT NULL,
    [Saturday] bit  NOT NULL,
    [Sunday] bit  NOT NULL,
    [Requisites] nvarchar(1000)  NOT NULL,
    [Icq] nvarchar(10)  NULL,
    [Skype] nvarchar(32)  NULL
);
GO

-- Creating table 'ShopFeedback'
CREATE TABLE [dbo].[ShopFeedback] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ShopID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Comment] nvarchar(1000)  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO

-- Creating table 'ShopProduct'
CREATE TABLE [dbo].[ShopProduct] (
    [ShopID] int  NOT NULL,
    [ProductID] bigint  NOT NULL,
    [ShopProductStatusID] int  NOT NULL,
    [Price] decimal(18,2)  NULL
);
GO

-- Creating table 'ShopProductStatus'
CREATE TABLE [dbo].[ShopProductStatus] (
    [ID] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Unit'
CREATE TABLE [dbo].[Unit] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(16)  NOT NULL
);
GO

-- Creating table 'CategoryParameter'
CREATE TABLE [dbo].[CategoryParameter] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SectionID] int  NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Description] nvarchar(512)  NULL
);
GO

-- Creating table 'CategoryParametersSection'
CREATE TABLE [dbo].[CategoryParametersSection] (
    [ID] int  NOT NULL,
    [CategoryID] int  NOT NULL,
    [Name] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'ProductParameterValue'
CREATE TABLE [dbo].[ProductParameterValue] (
    [ProductID] bigint  NOT NULL,
    [CategoryParameterID] int  NOT NULL,
    [Value] nvarchar(128)  NOT NULL,
    [UnitID] int  NULL
);
GO

-- Creating table 'Group'
CREATE TABLE [dbo].[Group] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [CategoryID] int  NOT NULL,
    [ManufacturerID] int  NOT NULL,
    [ReviewLink] nvarchar(256)  NULL,
    [Title] nvarchar(180)  NOT NULL,
    [SmallDescription] nvarchar(600)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsPopular] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GroupID] int  NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [Price] decimal(18,2)  NOT NULL,
    [IsNew] bit  NOT NULL,
    [IsPopular] bit  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Banners'
ALTER TABLE [dbo].[Banners]
ADD CONSTRAINT [PK_Banners]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BannerTypes'
ALTER TABLE [dbo].[BannerTypes]
ADD CONSTRAINT [PK_BannerTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Manufacturer'
ALTER TABLE [dbo].[Manufacturer]
ADD CONSTRAINT [PK_Manufacturer]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ProductID], [UserID] in table 'ProductBookmark'
ALTER TABLE [dbo].[ProductBookmark]
ADD CONSTRAINT [PK_ProductBookmark]
    PRIMARY KEY CLUSTERED ([ProductID], [UserID] ASC);
GO

-- Creating primary key on [ID] in table 'ProductFeedback'
ALTER TABLE [dbo].[ProductFeedback]
ADD CONSTRAINT [PK_ProductFeedback]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ProductImage'
ALTER TABLE [dbo].[ProductImage]
ADD CONSTRAINT [PK_ProductImage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Shop'
ALTER TABLE [dbo].[Shop]
ADD CONSTRAINT [PK_Shop]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ShopFeedback'
ALTER TABLE [dbo].[ShopFeedback]
ADD CONSTRAINT [PK_ShopFeedback]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ShopID], [ProductID] in table 'ShopProduct'
ALTER TABLE [dbo].[ShopProduct]
ADD CONSTRAINT [PK_ShopProduct]
    PRIMARY KEY CLUSTERED ([ShopID], [ProductID] ASC);
GO

-- Creating primary key on [ID] in table 'ShopProductStatus'
ALTER TABLE [dbo].[ShopProductStatus]
ADD CONSTRAINT [PK_ShopProductStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Unit'
ALTER TABLE [dbo].[Unit]
ADD CONSTRAINT [PK_Unit]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CategoryParameter'
ALTER TABLE [dbo].[CategoryParameter]
ADD CONSTRAINT [PK_CategoryParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CategoryParametersSection'
ALTER TABLE [dbo].[CategoryParametersSection]
ADD CONSTRAINT [PK_CategoryParametersSection]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ProductID], [CategoryParameterID] in table 'ProductParameterValue'
ALTER TABLE [dbo].[ProductParameterValue]
ADD CONSTRAINT [PK_ProductParameterValue]
    PRIMARY KEY CLUSTERED ([ProductID], [CategoryParameterID] ASC);
GO

-- Creating primary key on [ID] in table 'Group'
ALTER TABLE [dbo].[Group]
ADD CONSTRAINT [PK_Group]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BannerType] in table 'Banners'
ALTER TABLE [dbo].[Banners]
ADD CONSTRAINT [FK_Banners_BannerTypes]
    FOREIGN KEY ([BannerType])
    REFERENCES [dbo].[BannerTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Banners_BannerTypes'
CREATE INDEX [IX_FK_Banners_BannerTypes]
ON [dbo].[Banners]
    ([BannerType]);
GO

-- Creating foreign key on [ImageID] in table 'Banners'
ALTER TABLE [dbo].[Banners]
ADD CONSTRAINT [FK_Banners_Images]
    FOREIGN KEY ([ImageID])
    REFERENCES [dbo].[Images]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Banners_Images'
CREATE INDEX [IX_FK_Banners_Images]
ON [dbo].[Banners]
    ([ImageID]);
GO

-- Creating foreign key on [ShopID] in table 'ShopFeedback'
ALTER TABLE [dbo].[ShopFeedback]
ADD CONSTRAINT [FK_ShopFeedback_Shop]
    FOREIGN KEY ([ShopID])
    REFERENCES [dbo].[Shop]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopFeedback_Shop'
CREATE INDEX [IX_FK_ShopFeedback_Shop]
ON [dbo].[ShopFeedback]
    ([ShopID]);
GO

-- Creating foreign key on [ShopID] in table 'ShopProduct'
ALTER TABLE [dbo].[ShopProduct]
ADD CONSTRAINT [FK_ShopProduct_Shop]
    FOREIGN KEY ([ShopID])
    REFERENCES [dbo].[Shop]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ShopProductStatusID] in table 'ShopProduct'
ALTER TABLE [dbo].[ShopProduct]
ADD CONSTRAINT [FK_ShopProduct_ShopProductStatus]
    FOREIGN KEY ([ShopProductStatusID])
    REFERENCES [dbo].[ShopProductStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopProduct_ShopProductStatus'
CREATE INDEX [IX_FK_ShopProduct_ShopProductStatus]
ON [dbo].[ShopProduct]
    ([ShopProductStatusID]);
GO

-- Creating foreign key on [SectionID] in table 'CategoryParameter'
ALTER TABLE [dbo].[CategoryParameter]
ADD CONSTRAINT [FK_CategoryParameter_CategoryParametersSection]
    FOREIGN KEY ([SectionID])
    REFERENCES [dbo].[CategoryParametersSection]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryParameter_CategoryParametersSection'
CREATE INDEX [IX_FK_CategoryParameter_CategoryParametersSection]
ON [dbo].[CategoryParameter]
    ([SectionID]);
GO

-- Creating foreign key on [CategoryParameterID] in table 'ProductParameterValue'
ALTER TABLE [dbo].[ProductParameterValue]
ADD CONSTRAINT [FK_ProductParameterValue_CategoryParameter]
    FOREIGN KEY ([CategoryParameterID])
    REFERENCES [dbo].[CategoryParameter]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductParameterValue_CategoryParameter'
CREATE INDEX [IX_FK_ProductParameterValue_CategoryParameter]
ON [dbo].[ProductParameterValue]
    ([CategoryParameterID]);
GO

-- Creating foreign key on [UnitID] in table 'ProductParameterValue'
ALTER TABLE [dbo].[ProductParameterValue]
ADD CONSTRAINT [FK_ProductParameterValue_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductParameterValue_Unit'
CREATE INDEX [IX_FK_ProductParameterValue_Unit]
ON [dbo].[ProductParameterValue]
    ([UnitID]);
GO

-- Creating foreign key on [ManufacturerID] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_Product_Manufacturer]
    FOREIGN KEY ([ManufacturerID])
    REFERENCES [dbo].[Manufacturer]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Manufacturer'
CREATE INDEX [IX_FK_Product_Manufacturer]
ON [dbo].[Product]
    ([ManufacturerID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductBookmark'
ALTER TABLE [dbo].[ProductBookmark]
ADD CONSTRAINT [FK_ProductBookmark_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductID] in table 'ProductFeedback'
ALTER TABLE [dbo].[ProductFeedback]
ADD CONSTRAINT [FK_ProductFeedback_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductFeedback_Product'
CREATE INDEX [IX_FK_ProductFeedback_Product]
ON [dbo].[ProductFeedback]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductImage'
ALTER TABLE [dbo].[ProductImage]
ADD CONSTRAINT [FK_ProductImage_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductImage_Product'
CREATE INDEX [IX_FK_ProductImage_Product]
ON [dbo].[ProductImage]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductParameterValue'
ALTER TABLE [dbo].[ProductParameterValue]
ADD CONSTRAINT [FK_ProductParameterValue_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductID] in table 'ShopProduct'
ALTER TABLE [dbo].[ShopProduct]
ADD CONSTRAINT [FK_ShopProduct_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopProduct_Product'
CREATE INDEX [IX_FK_ShopProduct_Product]
ON [dbo].[ShopProduct]
    ([ProductID]);
GO

-- Creating foreign key on [GroupID] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [FK_Category_Group]
    FOREIGN KEY ([GroupID])
    REFERENCES [dbo].[Group]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Category_Group'
CREATE INDEX [IX_FK_Category_Group]
ON [dbo].[Category]
    ([GroupID]);
GO

-- Creating foreign key on [CategoryID] in table 'CategoryParametersSection'
ALTER TABLE [dbo].[CategoryParametersSection]
ADD CONSTRAINT [FK_CategoryParametersSection_Category]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Category]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryParametersSection_Category'
CREATE INDEX [IX_FK_CategoryParametersSection_Category]
ON [dbo].[CategoryParametersSection]
    ([CategoryID]);
GO

-- Creating foreign key on [CategoryID] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_Product_Category]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Category]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Category'
CREATE INDEX [IX_FK_Product_Category]
ON [dbo].[Product]
    ([CategoryID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------