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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231122154039_InitialSetup')
BEGIN
    CREATE TABLE [Habits] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Habits] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231122154039_InitialSetup')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Habits]'))
        SET IDENTITY_INSERT [Habits] ON;
    EXEC(N'INSERT INTO [Habits] ([Id], [Description], [Name])
    VALUES (100, N''Become a francophone'', N''Learn French''),
    (101, N''Get really fit'', N''Run a marathon''),
    (102, N''Finish your book project'', N''Write every day'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Habits]'))
        SET IDENTITY_INSERT [Habits] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231122154039_InitialSetup')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231122154039_InitialSetup', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231203133323_MultiTanent')
BEGIN
    ALTER TABLE [Habits] ADD [TenantName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231203133323_MultiTanent')
BEGIN
    EXEC(N'UPDATE [Habits] SET [TenantName] = N''CloudSphere''
    WHERE [Id] = 100;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231203133323_MultiTanent')
BEGIN
    EXEC(N'UPDATE [Habits] SET [TenantName] = N''CloudSphere''
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231203133323_MultiTanent')
BEGIN
    EXEC(N'UPDATE [Habits] SET [TenantName] = N''CloudSphere''
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231203133323_MultiTanent')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231203133323_MultiTanent', N'7.0.0');
END;
GO

COMMIT;
GO

