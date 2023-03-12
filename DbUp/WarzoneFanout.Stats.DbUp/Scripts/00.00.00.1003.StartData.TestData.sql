DECLARE @username NVARCHAR(150) = N'TestUser'

INSERT [dbo].[Usernames] ([Username]) VALUES (@username)

DECLARE @usernameId INT = (SELECT [Id] FROM [dbo].[Usernames] WHERE [Username] = @username)

INSERT [dbo].[MultiplayerStats] ([Kills], [Deaths], [Score], [MatchesPlayed], [UsernameId]) VALUES (1000, 700, 152600, 100, @usernameId)
INSERT [dbo].[BattleRoyalStats] ([Kills], [Deaths], [Score], [MatchesPlayed], [UsernameId]) VALUES (100, 70, 15260, 10, @usernameId)