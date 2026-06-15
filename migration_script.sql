IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [People] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Role] nvarchar(100) NOT NULL,
    [Department] nvarchar(100) NOT NULL,
    [Email] nvarchar(150) NOT NULL,
    [Status] nvarchar(20) NOT NULL DEFAULT N'active',
    [Summary] nvarchar(500) NOT NULL,
    [CreatedAtUtc] datetimeoffset NOT NULL,
    [LastUpdatedAtUtc] datetimeoffset NOT NULL,
    CONSTRAINT [PK_People] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260615230517_InitialCreate', N'9.0.8');

COMMIT;
GO

