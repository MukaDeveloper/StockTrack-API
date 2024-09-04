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

CREATE TABLE [ST_INSTITUTIONS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Nickname] nvarchar(max) NOT NULL,
    [StreetName] nvarchar(max) NOT NULL,
    [StreetNumber] nvarchar(max) NOT NULL,
    [Complement] nvarchar(max) NOT NULL,
    [Neightboor] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [CEP] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ST_INSTITUTIONS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ST_MOVIMENTATIONS] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_ST_MOVIMENTATIONS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ST_USERS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserType] int NULL DEFAULT 1,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [PhotoUrl] nvarchar(max) NOT NULL,
    [AccessDate] datetime2 NULL,
    CONSTRAINT [PK_ST_USERS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ST_AREAS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_AREAS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_AREAS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_USER_INSTITUTIONS] (
    [UserId] int NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_USER_INSTITUTIONS] PRIMARY KEY ([UserId], [InstitutionId]),
    CONSTRAINT [FK_ST_USER_INSTITUTIONS_ST_INSTITUTIONS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ST_USER_INSTITUTIONS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_WAREHOUSES] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AreaId] int NOT NULL,
    CONSTRAINT [PK_ST_WAREHOUSES] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_WAREHOUSES_ST_AREAS_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [ST_AREAS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_MATERIALS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImageURL] nvarchar(max) NOT NULL,
    [Manufacturer] nvarchar(max) NOT NULL,
    [RecordNumber] int NOT NULL,
    [MaterialType] int NOT NULL,
    [WarehouseId] int NOT NULL,
    CONSTRAINT [PK_ST_MATERIALS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_MATERIALS_ST_WAREHOUSES_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [ST_WAREHOUSES] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] ON;
INSERT INTO [ST_INSTITUTIONS] ([Id], [CEP], [City], [Complement], [Name], [Neightboor], [Nickname], [State], [StreetName], [StreetNumber])
VALUES (1, N'02110010', N'Sao Paulo', N'', N'Servidor de testes', N'Vila Guilherme', N'Testes', N'SP', N'Rua Alcantara', N'113'),
(64, N'02110010', N'Sao Paulo', N'', N'Horácio Augusto da Silveira', N'Vila Guilherme', N'ETEC Prof. Horácio', N'SP', N'Rua Alcantara', N'113');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'UserType') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] ON;
INSERT INTO [ST_USERS] ([Id], [AccessDate], [Email], [Name], [PasswordHash], [PasswordSalt], [PhotoUrl], [UserType])
VALUES (1, NULL, N'admin@stocktrack.com', N'Admin', 0xE389F545B8C76A975C68DF8329274DD93EEC303FBEF8C4414D97A9D5293AD6AAAA5E9BBE3F1EADD6AB804A2F81DCC254DEF6A4C1DCA208E73F3676300B68033A, 0xFCDFA7A8E5F095EB315A631B0FFDC8DBFCC0D5DCDF45BD53F367519A2C6EA2F132AE09118A180625A988CA6DF489AAC30F3CEC38C6B7152655BFAB177315C2FFD9F98E1AC5E0F97DB509FBE75A309DE31352E709C7829390FB871C9FBF094DB3158253490F186CC298EEF244955A13CA5D1B7686489E75E25D45F667E2D700C3, N'https://imgur.com/mOXzZLE.png', 4);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'UserType') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'InstitutionId', N'Name') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] ON;
INSERT INTO [ST_AREAS] ([Id], [Description], [InstitutionId], [Name])
VALUES (1, N'Área de Testes', 1, N'Teste');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'InstitutionId', N'Name') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] ON;
INSERT INTO [ST_USER_INSTITUTIONS] ([InstitutionId], [UserId])
VALUES (1, 1),
(64, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] ON;
INSERT INTO [ST_WAREHOUSES] ([Id], [AreaId], [Description], [Name])
VALUES (1, 1, N'Almoxarifado de informática', N'Informática');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'ImageURL', N'Manufacturer', N'MaterialType', N'Name', N'RecordNumber', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] ON;
INSERT INTO [ST_MATERIALS] ([Id], [Description], [ImageURL], [Manufacturer], [MaterialType], [Name], [RecordNumber], [WarehouseId])
VALUES (1, N'Notebook ThinkPad', N'', N'ThinkPad', 0, N'Notebook', 123456, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'ImageURL', N'Manufacturer', N'MaterialType', N'Name', N'RecordNumber', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] OFF;
GO

CREATE INDEX [IX_ST_AREAS_InstitutionId] ON [ST_AREAS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_MATERIALS_WarehouseId] ON [ST_MATERIALS] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_AreaId] ON [ST_WAREHOUSES] ([AreaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240904014459_InitialCreate', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ST_USERS]') AND [c].[name] = N'UserType');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ST_USERS] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ST_USERS] DROP COLUMN [UserType];
GO

ALTER TABLE [ST_WAREHOUSES] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ST_WAREHOUSES] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ST_WAREHOUSES] ADD [CreatedBy] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ST_WAREHOUSES] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [ST_WAREHOUSES] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [ST_USERS] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ST_USER_INSTITUTIONS] ADD [UserType] int NULL;
GO

ALTER TABLE [ST_MATERIALS] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ST_MATERIALS] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ST_MATERIALS] ADD [CreatedBy] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ST_MATERIALS] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [ST_MATERIALS] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [ST_AREAS] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ST_AREAS] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ST_AREAS] ADD [CreatedBy] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ST_AREAS] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [ST_AREAS] ADD [UpdatedBy] nvarchar(max) NULL;
GO

UPDATE [ST_AREAS] SET [Active] = CAST(1 AS bit), [CreatedAt] = '2024-09-04T20:58:22.7537625Z', [CreatedBy] = N'', [UpdatedAt] = NULL, [UpdatedBy] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MATERIALS] SET [Active] = CAST(1 AS bit), [CreatedAt] = '2024-09-04T20:58:22.7537706Z', [CreatedBy] = N'', [UpdatedAt] = NULL, [UpdatedBy] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_USERS] SET [Active] = CAST(1 AS bit), [PasswordHash] = 0x2AFE93EF2DE070616809CD5F37B7F3011029F762DAD22BA235E2506FA35DC4E28336DF71BA5FDDC19C5A44970C72B0E2B7760B1AD2DBF20DE87A1354E4A1FE7A, [PasswordSalt] = 0xBE9121428F97B6DC89C5F24FE59965B873B69D338396A649B33EE2659F1B6F489F355F6B7851DF596D391279B26440F8CF5C61FE74EFEC2D6D9BC0270219C4F5CD1BDDE7AE6A96433FB3E46E440523F15C66AF34F38BD1DF5C2B1CD2784DE06D618BAF65DD0BCC960C325362FA1A5B8A8EE6CECC084E2796EA58A9B59CE399AF
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_USER_INSTITUTIONS] SET [UserType] = 4
WHERE [InstitutionId] = 1 AND [UserId] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_USER_INSTITUTIONS] SET [UserType] = 3
WHERE [InstitutionId] = 64 AND [UserId] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_WAREHOUSES] SET [Active] = CAST(1 AS bit), [CreatedAt] = '2024-09-04T20:58:22.7537670Z', [CreatedBy] = N'', [UpdatedAt] = NULL, [UpdatedBy] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240904205823_BaseModel', N'8.0.7');
GO

COMMIT;
GO

