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
    [Neightboor] nvarchar(max) NOT NULL,
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
    CONSTRAINT [FK_ST_AREAS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
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
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_AREAS_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [ST_AREAS] ([Id]),
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]),
    CONSTRAINT [FK_ST_MOVIMENTATIONS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]),
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessCode', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] ON;
INSERT INTO [ST_INSTITUTIONS] ([Id], [AccessCode], [CEP], [City], [Complement], [Name], [Neightboor], [Nickname], [State], [StreetName], [StreetNumber])
VALUES (1, N'000', N'02110010', N'Sao Paulo', N'', N'Servidor de testes', N'Vila Guilherme', N'Testes', N'SP', N'Rua Alcantara', N'113'),
(2, N'064', N'02110010', N'Sao Paulo', N'', N'Horácio Augusto da Silveira', N'Vila Guilherme', N'ETEC Prof. Horácio', N'SP', N'Rua Alcantara', N'113');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessCode', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'CreatedAt', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'Verified', N'VerifiedAt', N'VerifiedScheduled', N'VerifiedToken') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] ON;
INSERT INTO [ST_USERS] ([Id], [AccessDate], [CreatedAt], [Email], [Name], [PasswordHash], [PasswordSalt], [PhotoUrl], [Verified], [VerifiedAt], [VerifiedScheduled], [VerifiedToken])
VALUES (1, NULL, '2024-10-24T21:23:25.2217463-03:00', N'admin@stocktrack.com', N'Admin', 0xBBEB8254FB0A8F1952C9C4FFA1A3B1EBD60FCDAA35AB2D61A7B0C07EBDF2A34C3EB98E130C12284AE14076C237580770A282F307874AFF1D834EEB5101FFD0E9, 0x005232E3C3C31B5AB380F59962CED8E27EDF5387504D40A70225DC10E15257A5B3FE804146E62980BCCA332D8F0D3D40E83B4E6CA2E9784696E43559FD5060D8BA3C35401B1EC56DC78EFD3E07A2A389D4F89EB66ADF4F77FBA6838412650D368FA4570A276DE55650A2810B688A9340BC26A42A5C06DCC13DEC9FBDCAF5F8BA, N'https://imgur.com/mOXzZLE.png', CAST(1 AS bit), '2024-10-24T21:23:25.2217473-03:00', NULL, NULL);
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
VALUES (1, CAST(1 AS bit), '2024-10-25T00:23:25.2217660Z', N'Admin', N'Área de Testes', 1, N'Teste', '2024-10-25T00:23:25.2217660Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'Manufacturer', N'MaterialType', N'Measure', N'Name', N'RecordNumber', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] ON;
INSERT INTO [ST_MATERIALS] ([Id], [Active], [CreatedAt], [CreatedBy], [Description], [ImageURL], [InstitutionId], [Manufacturer], [MaterialType], [Measure], [Name], [RecordNumber], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), '2024-10-25T00:23:25.2217697Z', N'', N'Notebook ThinkPad', N'', 1, N'ThinkPad', 0, N'UN', N'Notebook', 123456, '2024-10-25T00:23:25.2217697Z', N'');
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
VALUES (1, 1, '2024-10-24T21:23:25.2217761-03:00', N'Adição de área "Teste"', 0, 1, NULL, N'Admin', N'Área Teste', CAST(1 AS real), 0, 1, NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] ON;
INSERT INTO [ST_WAREHOUSES] ([Id], [Active], [AreaId], [CreatedAt], [CreatedBy], [Description], [InstitutionId], [Name], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), 1, '2024-10-25T00:23:25.2217679Z', N'', N'Almoxarifado de informática', 1, N'Informática', '2024-10-25T00:23:25.2217680Z', N'');
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

CREATE INDEX [IX_ST_USER_INSTITUTIONS_InstitutionId] ON [ST_USER_INSTITUTIONS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_WAREHOUSE_USERS_WarehouseId] ON [ST_WAREHOUSE_USERS] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_AreaId] ON [ST_WAREHOUSES] ([AreaId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_InstitutionId] ON [ST_WAREHOUSES] ([InstitutionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241025002326_create', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ST_SOLICITATIONS] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [UserId] int NOT NULL,
    [InstitutionId] int NOT NULL,
    [SolicitedAt] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_ST_SOLICITATIONS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_SOLICITATIONS_ST_USER_INSTITUTIONS_UserId_InstitutionId] FOREIGN KEY ([UserId], [InstitutionId]) REFERENCES [ST_USER_INSTITUTIONS] ([UserId], [InstitutionId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_SOLICITATION_MATERIALS] (
    [MaterialId] int NOT NULL,
    [SolicitationId] int NOT NULL,
    [Quantity] real NOT NULL,
    CONSTRAINT [PK_ST_SOLICITATION_MATERIALS] PRIMARY KEY ([MaterialId], [SolicitationId]),
    CONSTRAINT [FK_ST_SOLICITATION_MATERIALS_ST_MATERIALS_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [ST_MATERIALS] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ST_SOLICITATION_MATERIALS_ST_SOLICITATIONS_SolicitationId] FOREIGN KEY ([SolicitationId]) REFERENCES [ST_SOLICITATIONS] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [ST_AREAS] SET [CreatedAt] = '2024-11-21T23:33:09.5749614Z', [UpdatedAt] = '2024-11-21T23:33:09.5749614Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MATERIALS] SET [CreatedAt] = '2024-11-21T23:33:09.5749674Z', [UpdatedAt] = '2024-11-21T23:33:09.5749674Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_MOVIMENTATIONS] SET [Date] = '2024-11-21T20:33:09.5749737-03:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_USERS] SET [CreatedAt] = '2024-11-21T20:33:09.5749432-03:00', [PasswordHash] = 0xB097D2D99D0574A4B8866617DD9258189D2E9406672D76D5DC4CDCB19CD370B286FB5702B0B9B2915425E8DF06D532285B2E079E118346AAC29CE6F2F00700C8, [PasswordSalt] = 0x643F8C11404C2A54E99E14A50CD95D3D2FEE28D890B3B1E396B68FA0946A65A52457E142E8039AC062CCD776F6087E4D06625EB72FA76ABA920B995C9E98F99A46F13DE685C8FA627E9744164CBF3B12D59B71376593713CB35271681F05CB840C774499D455DCC4C42F3F6F300CB018919C4C9B9BE089BDF072AFC7ACED538E, [VerifiedAt] = '2024-11-21T20:33:09.5749446-03:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [ST_WAREHOUSES] SET [CreatedAt] = '2024-11-21T23:33:09.5749632Z', [UpdatedAt] = '2024-11-21T23:33:09.5749632Z'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ST_SOLICITATION_MATERIALS_SolicitationId] ON [ST_SOLICITATION_MATERIALS] ([SolicitationId]);
GO

CREATE INDEX [IX_ST_SOLICITATIONS_UserId_InstitutionId] ON [ST_SOLICITATIONS] ([UserId], [InstitutionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241121233309_SolicitationTables', N'8.0.7');
GO

COMMIT;
GO
