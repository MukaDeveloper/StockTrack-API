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
    [Active] bit NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [PhotoUrl] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [AccessDate] datetime2 NULL,
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
    [Quantity] real NOT NULL,
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Event') AND [object_id] = OBJECT_ID(N'[MovimentationEventEntity]'))
    SET IDENTITY_INSERT [MovimentationEventEntity] ON;
INSERT INTO [MovimentationEventEntity] ([Id], [Event])
VALUES (1, N'Area'),
(2, N'Warehouse'),
(3, N'Material'),
(4, N'Loan'),
(5, N'Maintenance'),
(6, N'General');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Event') AND [object_id] = OBJECT_ID(N'[MovimentationEventEntity]'))
    SET IDENTITY_INSERT [MovimentationEventEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Reason') AND [object_id] = OBJECT_ID(N'[MovimentationReasonEntity]'))
    SET IDENTITY_INSERT [MovimentationReasonEntity] ON;
INSERT INTO [MovimentationReasonEntity] ([Id], [Reason])
VALUES (1, N'Insertion'),
(2, N'Edit'),
(3, N'ReturnFromLoan'),
(4, N'ReturnFromMaintenance'),
(5, N'Disposal'),
(6, N'Loan'),
(7, N'SentToMaintenance'),
(8, N'Removed'),
(9, N'Other');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Reason') AND [object_id] = OBJECT_ID(N'[MovimentationReasonEntity]'))
    SET IDENTITY_INSERT [MovimentationReasonEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[MovimentationTypeEntity]'))
    SET IDENTITY_INSERT [MovimentationTypeEntity] ON;
INSERT INTO [MovimentationTypeEntity] ([Id], [Type])
VALUES (1, N'ENTRY'),
(2, N'EXIT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[MovimentationTypeEntity]'))
    SET IDENTITY_INSERT [MovimentationTypeEntity] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessCode', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] ON;
INSERT INTO [ST_INSTITUTIONS] ([Id], [AccessCode], [CEP], [City], [Complement], [Name], [Neightboor], [Nickname], [State], [StreetName], [StreetNumber])
VALUES (1, N'', N'02110010', N'Sao Paulo', N'', N'Servidor de testes', N'Vila Guilherme', N'Testes', N'SP', N'Rua Alcantara', N'113'),
(64, N'', N'02110010', N'Sao Paulo', N'', N'Horácio Augusto da Silveira', N'Vila Guilherme', N'ETEC Prof. Horácio', N'SP', N'Rua Alcantara', N'113');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessCode', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Active', N'CreatedAt', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] ON;
INSERT INTO [ST_USERS] ([Id], [AccessDate], [Active], [CreatedAt], [Email], [Name], [PasswordHash], [PasswordSalt], [PhotoUrl])
VALUES (1, NULL, CAST(1 AS bit), '2024-10-01T12:51:42.5801310-03:00', N'admin@stocktrack.com', N'Admin', 0xBEC1CB2D3165C9736243E1F41F6DEA1222E4D7EF21441671D0D639DD010B2427ABE0BCD60202790118BA9C87BE17D9BE7A4888DFC9F9702945FC2A98253F10F2, 0xC0697760FB69A849AAA4C04EA93DFA0D12D3FAE59E76F1C65CCB5E8C3F6A15EEA82BDCB9E6BC2D452E758CBC01A80FD4F494D5508D79AA04C8C4742509925D7F31BAD455E95A56A3BA8F13C83B27BEAE7AA3CED9E9EDCD92A69FE3ABE31F71F1BF1A0F04B140A3B4A822F7393B47BF73E7D6A839A2831E0AEE3D9A5983AA9463, N'https://imgur.com/mOXzZLE.png');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Active', N'CreatedAt', N'Email', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
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
VALUES (1, CAST(1 AS bit), '2024-10-01T15:51:42.5801675Z', N'Admin', N'Área de Testes', 1, N'Teste', '2024-10-01T15:51:42.5801676Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_AREAS]'))
    SET IDENTITY_INSERT [ST_AREAS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'Manufacturer', N'MaterialType', N'Measure', N'Name', N'Quantity', N'RecordNumber', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] ON;
INSERT INTO [ST_MATERIALS] ([Id], [Active], [CreatedAt], [CreatedBy], [Description], [ImageURL], [InstitutionId], [Manufacturer], [MaterialType], [Measure], [Name], [Quantity], [RecordNumber], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), '2024-10-01T15:51:42.5801762Z', N'', N'Notebook ThinkPad', N'', 1, N'ThinkPad', 0, N'UN', N'Notebook', CAST(3 AS real), 123456, '2024-10-01T15:51:42.5801762Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'CreatedAt', N'CreatedBy', N'Description', N'ImageURL', N'InstitutionId', N'Manufacturer', N'MaterialType', N'Measure', N'Name', N'Quantity', N'RecordNumber', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_MATERIALS]'))
    SET IDENTITY_INSERT [ST_MATERIALS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId', N'UserRole') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] ON;
INSERT INTO [ST_USER_INSTITUTIONS] ([InstitutionId], [UserId], [UserRole])
VALUES (1, 1, 4),
(64, 1, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId', N'UserRole') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] ON;
INSERT INTO [ST_MOVIMENTATIONS] ([Id], [AreaId], [Date], [Description], [Event], [InstitutionId], [MaterialId], [MovimentationBy], [Name], [Quantity], [Reason], [Type], [UserId], [WarehouseId])
VALUES (1, 1, '2024-10-01T12:51:42.5801794-03:00', N'Adição de área "Teste"', 1, 1, NULL, N'Admin', N'Área Teste', CAST(1 AS real), 1, 1, NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AreaId', N'Date', N'Description', N'Event', N'InstitutionId', N'MaterialId', N'MovimentationBy', N'Name', N'Quantity', N'Reason', N'Type', N'UserId', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ST_MOVIMENTATIONS]'))
    SET IDENTITY_INSERT [ST_MOVIMENTATIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] ON;
INSERT INTO [ST_WAREHOUSES] ([Id], [Active], [AreaId], [CreatedAt], [CreatedBy], [Description], [InstitutionId], [Name], [UpdatedAt], [UpdatedBy])
VALUES (1, CAST(1 AS bit), 1, '2024-10-01T15:51:42.5801718Z', N'', N'Almoxarifado de informática', 1, N'Informática', '2024-10-01T15:51:42.5801719Z', N'');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Active', N'AreaId', N'CreatedAt', N'CreatedBy', N'Description', N'InstitutionId', N'Name', N'UpdatedAt', N'UpdatedBy') AND [object_id] = OBJECT_ID(N'[ST_WAREHOUSES]'))
    SET IDENTITY_INSERT [ST_WAREHOUSES] OFF;
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

CREATE INDEX [IX_ST_WAREHOUSES_AreaId] ON [ST_WAREHOUSES] ([AreaId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_InstitutionId] ON [ST_WAREHOUSES] ([InstitutionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241001155143_InitialCreate', N'8.0.7');
GO

COMMIT;
GO

