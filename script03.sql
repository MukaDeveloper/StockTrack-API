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

CREATE TABLE [ST_AREAS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_AREAS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_AREAS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ST_USERS] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserType] int NOT NULL DEFAULT 1,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [PhotoUrl] nvarchar(max) NOT NULL,
    [AccessDate] datetime2 NULL,
    [InstitutionId] int NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ST_USERS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ST_USERS_ST_INSTITUTIONS_InstitutionId] FOREIGN KEY ([InstitutionId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE
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
VALUES (1, N'02110010', N'Sao Paulo', N'', N'Horácio Augusto da Silveira', N'Vila Guilherme', N'ETEC Prof. Horácio', N'SP', N'Rua Alcantara', N'113');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Email', N'InstitutionId', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'Token', N'UserType') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] ON;
INSERT INTO [ST_USERS] ([Id], [AccessDate], [Email], [InstitutionId], [Name], [PasswordHash], [PasswordSalt], [PhotoUrl], [Token], [UserType])
VALUES (1, NULL, N'admin@stocktrack.com', 1, N'Admin', 0x2223C759F0A3743743CB7B5FEC02A945EAB5F424694EFE9C21B99F0ABC35224446A2EED3BB1F662B416468386C29E4F7B19E8EAB69DFE8F4FC21A5E8645776E8, 0xEFB54191713B5EF9013F9441C4840800733ECDFD9C57F21FA03C4C5DE7F27A38B251145B28A02691AD4B8C7138BA22FFDD27B192A322D2FE2B48646A8CBBFE5FCC39859DC096CA65F6FC43FB9F5FF0C0681E14B502BD10CDF03171ADEA2C146D7C610A58650F2A6C62E2974CBD5AC7B69BC45B974A5810050DBCE588B4511205, N'', N'', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessDate', N'Email', N'InstitutionId', N'Name', N'PasswordHash', N'PasswordSalt', N'PhotoUrl', N'Token', N'UserType') AND [object_id] = OBJECT_ID(N'[ST_USERS]'))
    SET IDENTITY_INSERT [ST_USERS] OFF;
GO

CREATE INDEX [IX_ST_AREAS_InstitutionId] ON [ST_AREAS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_MATERIALS_WarehouseId] ON [ST_MATERIALS] ([WarehouseId]);
GO

CREATE INDEX [IX_ST_USERS_InstitutionId] ON [ST_USERS] ([InstitutionId]);
GO

CREATE INDEX [IX_ST_WAREHOUSES_AreaId] ON [ST_WAREHOUSES] ([AreaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240813225030_AuthCreate', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ST_USERS] DROP CONSTRAINT [FK_ST_USERS_ST_INSTITUTIONS_InstitutionId];
GO

DROP INDEX [IX_ST_USERS_InstitutionId] ON [ST_USERS];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ST_USERS]') AND [c].[name] = N'InstitutionId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ST_USERS] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ST_USERS] DROP COLUMN [InstitutionId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ST_USERS]') AND [c].[name] = N'Token');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ST_USERS] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ST_USERS] DROP COLUMN [Token];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ST_USERS]') AND [c].[name] = N'UserType');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ST_USERS] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ST_USERS] ALTER COLUMN [UserType] int NULL;
ALTER TABLE [ST_USERS] ADD DEFAULT 1 FOR [UserType];
GO

CREATE TABLE [ST_USER_INSTITUTIONS] (
    [UserId] int NOT NULL,
    [InstitutionId] int NOT NULL,
    CONSTRAINT [PK_ST_USER_INSTITUTIONS] PRIMARY KEY ([UserId], [InstitutionId]),
    CONSTRAINT [FK_ST_USER_INSTITUTIONS_ST_INSTITUTIONS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_INSTITUTIONS] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ST_USER_INSTITUTIONS_ST_USERS_UserId] FOREIGN KEY ([UserId]) REFERENCES [ST_USERS] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [ST_INSTITUTIONS] SET [Name] = N'Servidor de testes', [Nickname] = N'Testes'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] ON;
INSERT INTO [ST_INSTITUTIONS] ([Id], [CEP], [City], [Complement], [Name], [Neightboor], [Nickname], [State], [StreetName], [StreetNumber])
VALUES (64, N'02110010', N'Sao Paulo', N'', N'Horácio Augusto da Silveira', N'Vila Guilherme', N'ETEC Prof. Horácio', N'SP', N'Rua Alcantara', N'113');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CEP', N'City', N'Complement', N'Name', N'Neightboor', N'Nickname', N'State', N'StreetName', N'StreetNumber') AND [object_id] = OBJECT_ID(N'[ST_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_INSTITUTIONS] OFF;
GO

UPDATE [ST_USERS] SET [PasswordHash] = 0x18E4F43481B8C78ACC9DB4F5C29CB5E238B5CA2E536BE750ADAAF6B3B90BDA200A36324282D736E612767E0A75421E0A3EDED660A721A3C00C16A706D03F2A00, [PasswordSalt] = 0xAA4C2BD958A58A1BC19C72C8CBCF34EF5D07C7833DE1C9CE545C3A5252EF2F5610962BDC977F535F301106F54EE7F5A35C220D02D8472327C8E698EA430CBBCD599CA324856901C9721D382948424751DB2B6CC1607A8B755934AD8F0AE1C81EBBE130D4E378BD207A2C97938D8AAB7FC817390BC4E8D67795EE2D6BFC971499, [PhotoUrl] = N'https://imgur.com/mOXzZLE.png', [UserType] = 4
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] ON;
INSERT INTO [ST_USER_INSTITUTIONS] ([InstitutionId], [UserId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240902234421_UserInstitution', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [ST_USERS] SET [PasswordHash] = 0x0CFF1039169448F2B2B3B5C1A3700A61F5171D05FA0FA91BDB678E1428E7ABDEB727BAA29D439DBB2B6474EE84BBA2AABC8896EE24844E85F319881A65D5ADCC, [PasswordSalt] = 0xF1EAE3A2E2964E2E4DE6F1BD46B8F4BFB7FCD37E0229D54FB9A242CD48CA5764BD57E23CF85A74F406D4D26A013D9465FC37CE2D38B00A5C3F24CFBAC322CCA038F30BDEF63B29BF712FEE477F9C0DBD614287DBB0DFC1E0751EA2C8A86C1BFCF39D4D70AFFDD791C443AA2661630AA57DAC36A75CAB3C7163F68541B12BCB68
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] ON;
INSERT INTO [ST_USER_INSTITUTIONS] ([InstitutionId], [UserId])
VALUES (64, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InstitutionId', N'UserId') AND [object_id] = OBJECT_ID(N'[ST_USER_INSTITUTIONS]'))
    SET IDENTITY_INSERT [ST_USER_INSTITUTIONS] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240903205930_Institution', N'8.0.7');
GO

COMMIT;
GO

