IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Dados] (
    [CustomerId] int NOT NULL IDENTITY,
    [CardNumber] bigint NOT NULL,
    [CVV] int NOT NULL,
	[CardId] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Dados] PRIMARY KEY ([CustomerId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201118172520_InitialCreate', N'2.1.14-servicing-32113');

GO

