IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE type_desc = 'SQL_LOGIN' AND name = 'WarzoneFanoutApplicationUser')
BEGIN 
	PRINT N'Creating Login [WarzoneFanoutApplicationUser]...';
	CREATE LOGIN [WarzoneFanoutApplicationUser]
		WITH PASSWORD=0x0200E554ED9E28E647B1B1E1B421562BE4AFB7D7E0E82ABF421D185422CA7F338DF2C009D5D6C339CB23C6B7B19859BECBA043A01D6F5551D1218F30C8EECB075C70B184F715 HASHED,
		DEFAULT_DATABASE=[master],
		CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
END
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'WarzoneFanoutApplicationUser')
BEGIN 
	PRINT N'Creating User [WarzoneFanoutApplicationUser]...';
	CREATE USER [WarzoneFanoutApplicationUser]
		FOR LOGIN [WarzoneFanoutApplicationUser];

	PRINT N'Adding User [WarzoneFanoutApplicationUser] to Role [ApplicationRole]...';
	EXECUTE sp_addrolemember @rolename = N'ApplicationRole', @membername = N'WarzoneFanoutApplicationUser';
END
GO