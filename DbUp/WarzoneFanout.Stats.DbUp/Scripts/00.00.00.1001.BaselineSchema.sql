SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

IF NOT EXISTS ( SELECT  1
                FROM    [sys].[database_principals]
                WHERE   [type] = 'R'
                        AND [name] = 'ApplicationRole' )
    BEGIN
        PRINT N'Creating [ApplicationRole]...';
        CREATE ROLE [ApplicationRole]
        AUTHORIZATION [dbo];
    END;

GO 

CREATE TABLE [dbo].[Usernames](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](150) NOT NULL,
    CONSTRAINT [PK_Usernames] PRIMARY KEY ([Id])
	)
GO

CREATE NONCLUSTERED INDEX [Idx_Usernames_Username] ON [dbo].[Usernames]
(
	[Username] 
)
GO

GRANT INSERT, SELECT, DELETE, UPDATE ON [Usernames] TO [ApplicationRole]
GO

CREATE TABLE [dbo].[MultiplayerStats](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Kills] INT NOT NULL,
	[Deaths] INT NOT NULL,
	[Score] INT NOT NULL,
	[MatchesPlayed] INT NOT NULL,
	[UsernameId] INT NOT NULL
    CONSTRAINT [PK_MultiplayerStats] PRIMARY KEY ([Id])
	CONSTRAINT [FK_MultiplayerStats_UsernamesId] FOREIGN KEY (UsernameId)
	REFERENCES [dbo].[Usernames](Id)
	)
GO

GRANT INSERT, SELECT, DELETE, UPDATE ON [MultiplayerStats] TO [ApplicationRole]
GO

CREATE TABLE [dbo].[BattleRoyalStats](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Kills] INT NOT NULL,
	[Deaths] INT NOT NULL,
	[Score] INT NOT NULL,
	[MatchesPlayed] INT NOT NULL,
	[UsernameId] INT NOT NULL
    CONSTRAINT [PK_BattleRoyalStats] PRIMARY KEY ([Id])
	CONSTRAINT [FK_BattleRoyalStats_UsernamesId] FOREIGN KEY (UsernameId)
	REFERENCES [dbo].[Usernames](Id)
	)
GO

GRANT INSERT, SELECT, DELETE, UPDATE ON [BattleRoyalStats] TO [ApplicationRole]
GO