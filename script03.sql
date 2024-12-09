﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

CREATE TABLE [MaterialTypeEntity] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MaterialTypeEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovimentationEventEntity] (
    [Id] int NOT NULL IDENTITY,
    [Event] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MovimentationEventEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovimentationReasonEntity] (
    [Id] int NOT NULL IDENTITY,
    [Reason] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MovimentationReasonEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovimentationTypeEntity] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MovimentationTypeEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ST_INSTITUTIONS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Nickname] nvarchar(max) NOT NULL,
    [AccessCode] nvarchar(max) NOT NULL,
    [StreetName] nvarchar(max) NOT NULL,
    [StreetNumber] nvarchar(max) NOT NULL,
    [Complement] nvarchar(max) NOT NULL,
    [Neighborhood] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [CEP] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ST_INSTITUTIONS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ST_USERS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [PhotoUrl] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [AccessDate] datetime2 NULL,
    [Verified] bit NOT NULL,
    [VerifiedToken] nvarchar(max) NULL,
    [VerifiedAt] datetime2 NULL,
    [VerifiedScheduled] datetime2 NULL,
    CONSTRAINT [PK_ST_USERS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRoleEntity] (
    [Id] int NOT NULL IDENTITY,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserRoleEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ST_AREAS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_AREAS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_AREAS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE NO ACTION
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
    [Measure] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_MATERIALS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_MATERIALS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_USER_INSTITUTIONS] (
    [UserId] int NOT NULL,
    [InstitutionId] int NOT NULL,
    [Active] bit NOT NULL,
    [UserRole] int NOT NULL,
    CONSTRAINT [PK_ST_USER_INSTITUTIONS] PRIMARY KEY ([UserId], [InstitutionId]),
    CONSTRAINT [FK_ST_USER_INSTITUTIONS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ST_USER_INSTITUTIONS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_WAREHOUSES] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AreaId] int NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_WAREHOUSES] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_WAREHOUSES_ST_AREAS_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [ST_AREAS] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ST_WAREHOUSES_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_MATERIALS_STATUS] (
    [Status] int NOT NULL,
    [MaterialId] int NOT NULL,
    [Quantity] real NOT NULL,
    CONSTRAINT [PK_ST_MATERIALS_STATUS] PRIMARY KEY ([MaterialId], [Status]),
    CONSTRAINT [FK_ST_MATERIALS_STATUS_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_SOLICITATIONS] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [UserId] int NOT NULL,
    [InstitutionId] int NOT NULL,
    [Status] int NOT NULL,
    [SolicitedAt] datetime2 NOT NULL,
    [ExpectReturnAt] datetime2 NOT NULL,
    [ApprovedAt] datetime2 NULL,
    [DeclinedAt] datetime2 NULL,
    [BorroadAt] datetime2 NULL,
    [ReturnedAt] datetime2 NULL,
    CONSTRAINT [PK_ST_SOLICITATIONS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_SOLICITATIONS_ST_USER_INSTITUTIONS_UserId_InstitutionId] FOREIGN KEY ([UserId], [InstitutionId]) REFERENCES [ST_USER_INSTITUTIONS] ([UserId], [InstitutionId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_MATERIAL_WAREHOUSES] (
    [MaterialId] int NOT NULL,
    [WarehouseId] int NOT NULL,
    CONSTRAINT [PK_ST_MATERIAL_WAREHOUSES] PRIMARY KEY ([MaterialId], [WarehouseId]),
    CONSTRAINT [FK_ST_MATERIAL_WAREHOUSES_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ST_MATERIAL_WAREHOUSES_ST_WAREHOUSES_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [ST_WAREHOUSES] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ST_MOVIMENTATIONS] (
    [Id] int NOT NULL IDENTITY,
    [InstitutionId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [MovimentationBy] nvarchar(max) NOT NULL,
    [MaterialId] int NULL,
    [WarehouseId] int NULL,
    [AreaId] int NULL,
    [UserId] int NULL,
    [Event] int NOT NULL,
    [Type] int NOT NULL,
    [Reason] int NOT NULL,
    [Quantity] real NOT NULL,
    [Date] datetime2 NOT NULL,
    CONSTRAINT [PK_ST_MOVIMENTATIONS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_AREAS_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [ST_AREAS] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_WAREHOUSES_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [ST_WAREHOUSES] ([Id])
);
GO

CREATE TABLE [ST_WAREHOUSE_USERS] (
    [WarehouseId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_ST_WAREHOUSE_USERS] PRIMARY KEY ([UserId], [WarehouseId]),
    CONSTRAINT [FK_ST_WAREHOUSE_USERS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ST_WAREHOUSE_USERS_ST_WAREHOUSES_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [ST_WAREHOUSES] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ST_SOLICITATION_MATERIALS] (
    [MaterialId] int NOT NULL,
    [SolicitationId] int NOT NULL,
    [Quantity] real NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_ST_SOLICITATION_MATERIALS] PRIMARY KEY ([MaterialId], [SolicitationId]),
    CONSTRAINT [FK_ST_SOLICITATION_MATERIALS_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ST_SOLICITATION_MATERIALS_ST_SOLICITATIONS_SolicitationId] FOREIGN KEY ([SolicitationId]) REFERENCES [ST_SOLICITATIONS] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[MaterialTypeEntity]'))
    SET IDENTITY_INSERT [MaterialTypeEntity] ON;
INSERT INTO [MaterialTypeEntity] ([Id], [Type])
VALUES (1, N'LOAN'),
(2, N'CONSUMPTION');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[MaterialTypeEntity]'))
    SET IDENTITY_INSERT [MaterialTypeEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Event') AND [object_id] = OBJECT_ID(N'[MovimentationEventEntity]'))
    SET IDENTITY_INSERT [MovimentationEventEntity] ON;
INSERT INTO [MovimentationEventEntity] ([Id], [Event])
VALUES (1, N'ENTRY'),
(2, N'EDIT'),
(3, N'EXIT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Event') AND [object_id] = OBJECT_ID(N'[MovimentationEventEntity]'))
    SET IDENTITY_INSERT [MovimentationEventEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Reason') AND [object_id] = OBJECT_ID(N'[MovimentationReasonEntity]'))
    SET IDENTITY_INSERT [MovimentationReasonEntity] ON;
INSERT INTO [MovimentationReasonEntity] ([Id], [Reason])
VALUES (1, N'INSERTION'),
(2, N'EDIT'),
(3, N'RETURNFROMLOAN'),
(4, N'RETURNFROMMAINTENANCE'),
(5, N'DISPOSAL'),
(6, N'LOAN'),
(7, N'SENTTOMAINTENANCE'),
(8, N'REMOVED'),
(9, N'OTHER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Reason') AND [object_id] = OBJECT_ID(N'[MovimentationReasonEntity]'))
    SET IDENTITY_INSERT [MovimentationReasonEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[MovimentationTypeEntity]'))
    SET IDENTITY_INSERT [MovimentationTypeEntity] ON;
INSERT INTO [MovimentationTypeEntity] ([Id], [Type])
VALUES (1, N'USER'),
(2, N'AREA'),
(3, N'WAREHOUSE'),
(4, N'MATERIAL'),
(5, N'LOAN'),
(6, N'MAINTENANCE'),
(7, N'GENERAL'),
(8, N'CONSUMPTION');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[MovimentationTypeEntity]'))
    SET IDENTITY_INSERT [MovimentationTypeEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessCode', N'CEP', N'City', N'Complement', N'Name', N'Neighborhood', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] ON;
INSERT INTO [ST_INSTITUTIONS] ([Id], [AccessCode], [CEP], [City], [Complement], [Name], [Neighborhood], [Nickname], [State], [StreetName], [StreetNumber])
VALUES (1, N'000', N'02110010', N'Sao Paulo', N'', N'Servidor de testes', N'Vila Guilherme', N'Testes', N'SP', N'Rua Alcantara', N'113'),
(2, N'064', N'02110010', N'Sao Paulo', N'', N'Horácio Augusto da Silveira', N'Vila Guilherme', N'ETEC Prof. Horácio', N'SP', N'Rua Alcantara', N'113');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessCode', N'CEP', N'City', N'Complement', N'Name', N'Neighborhood', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'CreatedAt', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'Verified', N'VerifiedAt', N'VerifiedScheduled', N'VerifiedToken') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] ON;
INSERT INTO [ST_USERS] ([Id], [AccessDate], [CreatedAt], [Email], [Name], [PasswordHash], [PasswordSalt], [PhotoUrl], [Verified], [VerifiedAt], [VerifiedScheduled], [VerifiedToken])
VALUES (1, NULL, '2024-12-03T21:19:25.2019285-03:00', N'admin@stocktrack.com', N'Admin', 0xA5B12A5D56E6FBC7E5EBBF48F101CCFC59CB03B1BC1A5EB5EF608C4B18F877B725FF2663E401BC90D945CAA476A82AE1DEB3812E89BA05272E386392357B2099, 0xDE1DB69203E374FA6F40912663D602545560C3BF30A7C64CF173A79AECA5E140E21D6500A7DA4FD360DF5EBCA30C9FE6C70A6949F939877D72B59C1C6A47AEB22BA48B3800798C370FEF225F141286733CE278AF7B0B47EE853FDD2049F082BC98E16B1A405D133FAEBB538B471C747B803316725A6413AE68D8F0A69AC3E3F6, N'https://imgur.com/mOXzZLE.png', CAST(1 AS bit), '2024-12-03T21:19:25.2019302-03:00', NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'CreatedAt', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'Verified', N'VerifiedAt', N'VerifiedScheduled', N'VerifiedToken') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Role') AND [object_id] = OBJECT_ID(N'[UserRoleEntity]'))
    SET IDENTITY_INSERT [UserRoleEntity] ON;
INSERT INTO [UserRoleEntity] ([Id], [Role])
VALUES (1, N'USER'),
(2, N'WAREHOUSEMAN'),
(3, N'COORDINATOR'),
(4, N'SUPPORT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Role') AND [object_id] = OBJECT_ID(N'[UserRoleEntity]'))
    SET IDENTITY_INSERT [UserRoleEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] ON;
INSERT INTO [ST_AREAS] ([Id], [Active], [CreatedAt], [CreatedBy], [Description], [InstitutionId], [Name], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), '2024-12-04T00:19:25.2019570Z', N'Admin', N'Área de Testes', 1, N'Teste', '2024-12-04T00:19:25.2019570Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'Manufacturer', N'MaterialType', N'Measure', N'Name', N'RecordNumber', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] ON;
INSERT INTO [ST_MATERIALS] ([Id], [Active], [CreatedAt], [CreatedBy], [Description], [ImageURL], [InstitutionId], [Manufacturer], [MaterialType], [Measure], [Name], [RecordNumber], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), '2024-12-04T00:19:25.2019636Z', N'', N'Notebook ThinkPad', N'', 1, N'ThinkPad', 0, N'UN', N'Notebook', 123456, '2024-12-04T00:19:25.2019637Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'Manufacturer', N'MaterialType', N'Measure', N'Name', N'RecordNumber', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId', N'Active', N'UserRole') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] ON;
INSERT INTO [ST_USER_INSTITUTIONS] ([InstitutionId], [UserId], [Active], [UserRole])
VALUES (1, 1, CAST(1 AS bit), 3),
(2, 1, CAST(1 AS bit), 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId', N'Active', N'UserRole') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaterialId', N'Status', N'Quantity') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS_STATUS]'))
    SET IDENTITY_INSERT [ST_MATERIALS_STATUS] ON;
INSERT INTO [ST_MATERIALS_STATUS] ([MaterialId], [Status], [Quantity])
VALUES (1, 0, CAST(3 AS real)),
(1, 1, CAST(1 AS real)),
(1, 2, CAST(1 AS real)),
(1, 3, CAST(5 AS real)),
(1, 4, CAST(1 AS real));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaterialId', N'Status', N'Quantity') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS_STATUS]'))
    SET IDENTITY_INSERT [ST_MATERIALS_STATUS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] ON;
INSERT INTO [ST_MOVIMENTATIONS] ([Id], [AreaId], [Date], [Description], [Event], [InstitutionId], [MaterialId], [MovimentationBy], [Name], [Quantity], [Reason], [Type], [UserId], [WarehouseId])
VALUES (1, 1, '2024-12-03T21:19:25.2019736-03:00', N'Adição de área "Teste"', 0, 1, NULL, N'Admin', N'Área Teste', CAST(1 AS real), 0, 1, NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] ON;
INSERT INTO [ST_WAREHOUSES] ([Id], [Active], [AreaId], [CreatedAt], [CreatedBy], [Description], [InstitutionId], [Name], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), 1, '2024-12-04T00:19:25.2019602Z', N'', N'Almoxarifado de informática', 1, N'Informática', '2024-12-04T00:19:25.2019602Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaterialId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MATERIAL_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_MATERIAL_WAREHOUSES] ON;
INSERT INTO [ST_MATERIAL_WAREHOUSES] ([MaterialId], [WarehouseId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MaterialId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MATERIAL_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_MATERIAL_WAREHOUSES] OFF;
GO

CREATE INDEX [IX_ST_AREAS_InstitutionId] ON [ST_AREAS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_MATERIAL_WAREHOUSES_WarehouseId] ON [ST_MATERIAL_WAREHOUSES] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_MATERIALS_InstitutionId] ON [ST_MATERIALS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_AreaId] ON [ST_MOVIMENTATIONS] ([AreaId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_MaterialId] ON [ST_MOVIMENTATIONS] ([MaterialId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_UserId] ON [ST_MOVIMENTATIONS] ([UserId]);
GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_WarehouseId] ON [ST_MOVIMENTATIONS] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_SOLICITATION_MATERIALS_SolicitationId] ON [ST_SOLICITATION_MATERIALS] ([SolicitationId]);
GO

CREATE INDEX [IX_ST_SOLICITATIONS_UserId_InstitutionId] ON [ST_SOLICITATIONS] ([UserId], [InstitutionId]);
GO

CREATE INDEX [IX_ST_USER_INSTITUTIONS_InstitutionId] ON [ST_USER_INSTITUTIONS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_WAREHOUSE_USERS_WarehouseId] ON [ST_WAREHOUSE_USERS] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_AreaId] ON [ST_WAREHOUSES] ([AreaId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_InstitutionId] ON [ST_WAREHOUSES] ([InstitutionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204001927_AllTables', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [ST_AREAS] SET [CreatedAt] = '2024-12-08T20:32:06.5406818Z', [Description] = N'Conjunto de almoxarifados da área norte', [Name] = N'Norte', [UpdatedAt] = '2024-12-08T20:32:06.5406818Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_INSTITUTIONS] SET [Name] = N'Manutenção', [Nickname] = N'Manutenção'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MATERIALS] SET [CreatedAt] = '2024-12-08T20:32:06.5406871Z', [UpdatedAt] = '2024-12-08T20:32:06.5406872Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MOVIMENTATIONS] SET [Date] = '2024-12-08T17:32:06.5406965-03:00', [Description] = N'Adição de área "Norte"', [Name] = N'Norte'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_USERS] SET [CreatedAt] = '2024-12-08T17:32:06.5406538-03:00', [PasswordHash] = 0xCBA50F1D66D2D74BAF0A5AEA2DF24645BBC3FACBCF66B9AEECFB10E08C7E5761C792FEB7369388E5691F6927D19947C9F3719B90D651E653B88E1D792A3766C6, [PasswordSalt] = 0x2C6178B9132E986724198436DB5C8940A8429692DC185CE530ECECE122F05E1575B224C3B02F927BF6646DC3AB02EE055AEA5F410D895CC126602DDA1C3D04249AC7CBBFC8DDC003F1600090649E504AEABF442D2C77285B8BD6686548884F4C07D084289C6F0883BE5E1AA5E82D7CAF7CEEA7A3432C3279A117125F1058FA8E, [VerifiedAt] = '2024-12-08T17:32:06.5406551-03:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_WAREHOUSES] SET [CreatedAt] = '2024-12-08T20:32:06.5406844Z', [UpdatedAt] = '2024-12-08T20:32:06.5406844Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241208203207_testeToMaintenance', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD [SolicitationId] int NOT NULL DEFAULT 0;
GO

UPDATE [ST_AREAS] SET [CreatedAt] = '2024-12-08T23:59:12.8819493Z', [UpdatedAt] = '2024-12-08T23:59:12.8819493Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MATERIALS] SET [CreatedAt] = '2024-12-08T23:59:12.8819551Z', [UpdatedAt] = '2024-12-08T23:59:12.8819551Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MOVIMENTATIONS] SET [Date] = '2024-12-08T20:59:12.8819669-03:00', [SolicitationId] = 0
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'SolicitationId', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] ON;
INSERT INTO [ST_MOVIMENTATIONS] ([Id], [AreaId], [Date], [Description], [Event], [InstitutionId], [MaterialId], [MovimentationBy], [Name], [Quantity], [Reason], [SolicitationId], [Type], [UserId], [WarehouseId])
VALUES (2, NULL, '2024-12-08T20:59:12.8819675-03:00', N'Adição de almoxarifado "Informática"', 0, 1, NULL, N'Admin', N'Informática', CAST(1 AS real), 0, 0, 2, NULL, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'SolicitationId', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] OFF;
GO

UPDATE [ST_USERS] SET [CreatedAt] = '2024-12-08T20:59:12.8819286-03:00', [PasswordHash] = 0x0834B4B4B334F9DA7742351A6B57A995EC6236CBE6A2ABBDD1565FA2CAAF7343297A7CE9125154F1C8E143E12D180A7EE3E6B48C00F0F92272CBF90E632E3AD7, [PasswordSalt] = 0xA74B778CE89E8E8B39905B8771266530392A0759C2A796B4717950060AB8E372A62C1BA6CA1FF7EB2B016D27DF2C3F57555699F49169727B0DD255E4263D03671AAFFA0E4D0C893D4901BE472AA0F910068EF0678EB6DA32E2497CA02CA4FC6122FABB4AA1B690683122906EF2E78CFB2955E7AD019748D580E31745A460FAF0, [VerifiedAt] = '2024-12-08T20:59:12.8819303-03:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_WAREHOUSES] SET [CreatedAt] = '2024-12-08T23:59:12.8819519Z', [UpdatedAt] = '2024-12-08T23:59:12.8819520Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ST_MOVIMENTATIONS_SolicitationId] ON [ST_MOVIMENTATIONS] ([SolicitationId]);
GO

ALTER TABLE [ST_MOVIMENTATIONS] ADD CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_SOLICITATIONS_SolicitationId] FOREIGN KEY ([SolicitationId]) REFERENCES [ST_SOLICITATIONS] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241208235914_SolicitationMovimentation', N'8.0.7');
GO

COMMIT;
GO

