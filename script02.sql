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
    [Active] bit NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
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
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [Active] bit NOT NULL,
    [InstitutionId] int NOT NULL,
    [InstitutionName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ST_AREAS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_AREAS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_USER_INSTITUTIONS] (
    [UserId] int NOT NULL,
    [InstitutionId] int NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [InstitutionName] nvarchar(max) NOT NULL,
    [UserType] int NULL,
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
    [AreaName] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [Active] bit NOT NULL,
    [InstitutionId] int NOT NULL,
    [InstitutionName] nvarchar(max) NOT NULL,
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
    [AreaId] int NOT NULL,
    [AreaName] nvarchar(max) NOT NULL,
    [WarehouseId] int NOT NULL,
    [WarehouseName] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [Active] bit NOT NULL,
    [InstitutionId] int NOT NULL,
    [InstitutionName] nvarchar(max) NOT NULL,
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Active', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] ON;
INSERT INTO [ST_USERS] ([Id], [AccessDate], [Active], [Email], [Name], [PasswordHash], [PasswordSalt], [PhotoUrl])
VALUES (1, NULL, CAST(1 AS bit), N'admin@stocktrack.com', N'Admin', 0xFACDFA724B417BAC68DBDB7C7B0532CB8BBB554131DF99D32286080BFE5E15E6A87593FF975321E02388CE15C148DACAF052A2B3FC0A823ED771D22A24B98148, 0xD3E212F8BA6D11E719196941F7EE87D040355BD7E838A73C788FDD3DDEE802B221C13B1DB0650D8E5D6CFDE3EC00DAD2225D4B342C08AECFB5A29324B902077B13C25CB823FC8A8A0E0D9F25CB50C0719D07657BBA9E661C3F0317CA99CFEF52D31FEA60BBCBF780AF4584385E2E7D8D28A80C4F5FF55C91DAC3157D45DE93CF, N'https://imgur.com/mOXzZLE.png');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Active', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'InstitutionName', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] ON;
INSERT INTO [ST_AREAS] ([Id], [Active], [CreatedAt], [CreatedBy], [Description], [InstitutionId], [InstitutionName], [Name], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), '2024-09-04T23:46:21.8346900Z', N'', N'Área de Testes', 1, N'Servidor de testes', N'Teste', NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'InstitutionName', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId', N'InstitutionName', N'UserName', N'UserType') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] ON;
INSERT INTO [ST_USER_INSTITUTIONS] ([InstitutionId], [UserId], [InstitutionName], [UserName], [UserType])
VALUES (1, 1, N'Servidor de testes', N'Admin', 4),
(64, 1, N'Horácio Augusto da Silveira', N'Admin', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId', N'InstitutionName', N'UserName', N'UserType') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'AreaName', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'InstitutionName', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] ON;
INSERT INTO [ST_WAREHOUSES] ([Id], [Active], [AreaId], [AreaName], [CreatedAt], [CreatedBy], [Description], [InstitutionId], [InstitutionName], [Name], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), 1, N'Teste', '2024-09-04T23:46:21.8346918Z', N'', N'Almoxarifado de informática', 1, N'Servidor de testes', N'Informática', NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'AreaName', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'InstitutionName', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'AreaName', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'InstitutionName', N'Manufacturer', N'MaterialType', N'Name', N'RecordNumber', N'UpdatedAt', N'UpdatedBy', N'WarehouseId', N'WarehouseName') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] ON;
INSERT INTO [ST_MATERIALS] ([Id], [Active], [AreaId], [AreaName], [CreatedAt], [CreatedBy], [Description], [ImageURL], [InstitutionId], [InstitutionName], [Manufacturer], [MaterialType], [Name], [RecordNumber], [UpdatedAt], [UpdatedBy], [WarehouseId], [WarehouseName])
VALUES (1, CAST(1 AS bit), 1, N'Teste', '2024-09-04T23:46:21.8346931Z', N'', N'Notebook ThinkPad', N'', 1, N'Servidor de testes', N'ThinkPad', 0, N'Notebook', 123456, NULL, NULL, 1, N'Informática');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'AreaName', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'InstitutionName', N'Manufacturer', N'MaterialType', N'Name', N'RecordNumber', N'UpdatedAt', N'UpdatedBy', N'WarehouseId', N'WarehouseName') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] OFF;
GO

CREATE INDEX [IX_ST_AREAS_InstitutionId] ON [ST_AREAS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_MATERIALS_WarehouseId] ON [ST_MATERIALS] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_AreaId] ON [ST_WAREHOUSES] ([AreaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240904234622_RebuildContext', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [AreaId] int NULL;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [Date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [Description] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [InstitutionId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [MaterialId] int NULL;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [Reason] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [Type] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [UserId] int NULL;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [WarehouseId] int NULL;
GO

CREATE TABLE [MovimentationTypeEntity] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MovimentationTypeEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserTypeEntity] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserTypeEntity] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[MovimentationTypeEntity]'))
    SET IDENTITY_INSERT [MovimentationTypeEntity] ON;
INSERT INTO [MovimentationTypeEntity] ([Id], [Name])
VALUES (1, N'ENTRY'),
(2, N'EXIT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[MovimentationTypeEntity]'))
    SET IDENTITY_INSERT [MovimentationTypeEntity] OFF;
GO

UPDATE [ST_AREAS] SET [CreatedAt] = '2024-09-15T04:19:11.4493370Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MATERIALS] SET [CreatedAt] = '2024-09-15T04:19:11.4493421Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'InstitutionId', N'MaterialId', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] ON;
INSERT INTO [ST_MOVIMENTATIONS] ([Id], [AreaId], [Date], [Description], [InstitutionId], [MaterialId], [Reason], [Type], [UserId], [WarehouseId])
VALUES (1, 1, '2024-09-15T01:19:11.4493481-03:00', N'Adição de área "Teste"', 1, NULL, 1, 1, 1, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'InstitutionId', N'MaterialId', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] OFF;
GO

UPDATE [ST_USERS] SET [PasswordHash] = 0x83015A4D4383256189A43DD14A228D1BE2A205D84D20218CF297FD6AA4E8AA7FC8F87E7376F4CD76ACAA623093D6520DCC226613A458B388C43D59C65E803B95, [PasswordSalt] = 0x2583084FB6D21934CF2DCCFA853AE7FD54C7198691DB960D748396E11F02A7BB88F5FA95B6B222CCBC6DA3644DE46D58D5A1D4E4A49FCA33CC76725824F29FBB2436464C63CCB0FE36318256D78A83F70C7D0C368141B4D60FFF1CC3F2D0711E00C047F14F982A5C9FDED2609E0794C0B6A6F0F2468706FB80F9FB87EF9A5B3E
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_WAREHOUSES] SET [CreatedAt] = '2024-09-15T04:19:11.4493400Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[UserTypeEntity]'))
    SET IDENTITY_INSERT [UserTypeEntity] ON;
INSERT INTO [UserTypeEntity] ([Id], [Name])
VALUES (1, N'USER'),
(2, N'WAREHOUSEMAN'),
(3, N'COORDINATOR'),
(4, N'SUPPORT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[UserTypeEntity]'))
    SET IDENTITY_INSERT [UserTypeEntity] OFF;
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_AreaId] ON [ST_MOVIMENTATIONS] ([AreaId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_MaterialId] ON [ST_MOVIMENTATIONS] ([MaterialId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_UserId] ON [ST_MOVIMENTATIONS] ([UserId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_WarehouseId] ON [ST_MOVIMENTATIONS] ([WarehouseId]);
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_AREAS_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [ST_AREAS] ([Id]);
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]);
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]);
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_WAREHOUSES_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [ST_WAREHOUSES] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240915041912_Movimentations', N'8.0.7');
GO

COMMIT;
GO

