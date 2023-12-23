CREATE TABLE [dbo].[Widget] (
    [Id]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NULL,
    [Shape] NVARCHAR (50) NULL,
    [Color] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Widget] PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog', 'Octagonal', 'Purple');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Wheel', 'Round', 'Yellow');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Gear', 'Square', 'Red');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog2', 'Octagonal', 'Purple');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog3', 'Octagonal', 'Purple');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog4', 'Octagonal', 'Orange');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog5', 'Rectangular', 'Orange');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog6', 'Rectangular', 'Yellow');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog7', 'Octagonal', 'Green');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog8', 'Round', 'Green');
INSERT INTO dbo.Widget ([Name], Shape, Color) VALUES ('Cog9', 'Square', 'Green');