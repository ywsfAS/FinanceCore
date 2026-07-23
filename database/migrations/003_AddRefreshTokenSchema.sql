CREATE TABLE RefreshTokens
(
    Id UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,

    TokenHash NVARCHAR(128) NOT NULL,

    ExpiresAt DATETIME2 NOT NULL,
    RevokedAt DATETIME2 NULL,

    DeviceLabel NVARCHAR(200) NULL,
    UserAgent NVARCHAR(1000) NULL,

    LastUsedAt DATETIME2 NULL,

    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_RefreshTokens_CreatedAt
        DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_RefreshTokens
        PRIMARY KEY (Id),

    CONSTRAINT FK_RefreshTokens_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(Id)
);
CREATE INDEX IX_RefreshTokens_UserId
ON RefreshTokens(UserId);
CREATE INDEX IX_RefreshTokens_TokenHash
ON RefreshTokens(TokenHash);
