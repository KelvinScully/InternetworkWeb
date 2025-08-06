
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 07/18/2025
-- Description:	Table: Account User Vault
-- =============================================

CREATE TABLE [Account].[UserVault] (
	[UserId] [INT] NOT NULL,
	[UserName] [VARCHAR](128) NOT NULL,
	[UserEmail] [VARCHAR](128) NOT NULL,

	[UserHash] [VARBINARY](32) NOT NULL,
	[UserSalt] [VARBINARY](16) NOT NULL,

	[IsEmailVerified] [BIT] NOT NULL,
	[IsActive] [BIT] NOT NULL,
	[CreatedDateTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [Pk_AccUserVault] PRIMARY KEY CLUSTERED (
		UserId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Get By Id
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultGetById]
	@UserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		UserId,
		UserName,
		UserEmail,

		IsEmailVerified,
		IsActive
	FROM	[Account].[UserVault]
	WHERE	(@UserId IS NULL OR @UserId = 0 OR UserId = @UserId)
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Get By UserName
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultGetByUserName]
	@UserName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		UserId,
		UserName,
		UserEmail,

		IsEmailVerified,
		CreatedDateTime
	FROM	[Account].[UserVault]
	WHERE	UserName = @UserName
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Get Hash N Salt
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultGetHashNSalt]
	@UserName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
        UserHash,
        UserSalt
	FROM	[Account].[UserVault]
	WHERE	UserName = @UserName
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultInsert]

	@UserName	VARCHAR(128),
	@UserEmail	VARCHAR(128),

	@UserHash	VARBINARY(32),
	@UserSalt	VARBINARY(16)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Account].[UserVault]
		WHERE	TRIM(LOWER(UserName)) = TRIM(LOWER(@UserName))
	)
	BEGIN
		RAISERROR('Duplicate UserName Found', 16, 1)
		RETURN;
	END;

	IF EXISTS (
		SELECT	*
		FROM	[Account].[UserVault]
		WHERE	TRIM(LOWER(UserEmail)) = TRIM(LOWER(@UserEmail))
	)
	BEGIN
		RAISERROR('Duplicate UserEmail Found', 16, 1)
		RETURN;
	END;

	DECLARE @UserId INT = ISNULL((SELECT MAX(UserId) + 1 FROM Account.UserVault),1);

	INSERT INTO [Account].[UserVault] (
		[UserId],
		[UserName],
		[UserEmail],

		[UserHash],
		[UserSalt],

		[IsEmailVerified],
		[IsActive],
		[CreatedDateTime]
	)
	VALUES (
		@UserId,
		@UserName,
		@UserEmail,
		@UserHash,
		@UserSalt,
		0,
		1,
		SYSDATETIME()
	);

	SELECT @UserId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Update
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultUpdate]
	@UserId		INT,
	@UserName	VARCHAR(128),
	@UserEmail	VARCHAR(128),

	@UserHash	VARBINARY(32),
	@UserSalt	VARBINARY(16)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Account].[UserVault]
		WHERE	UserId <> @UserId
			AND TRIM(LOWER(UserName)) = TRIM(LOWER(@UserName))
	)
	BEGIN
		RAISERROR('Duplicate UserName Found', 16, 1)
		RETURN;
	END;

	IF EXISTS (
		SELECT	*
		FROM	[Account].[UserVault]
		WHERE	TRIM(LOWER(UserEmail)) = TRIM(LOWER(@UserEmail))
	)
	BEGIN
		RAISERROR('Duplicate UserEmail Found', 16, 1)
		RETURN;
	END;

	UPDATE	[Account].[UserVault]
	SET		UserName = @UserName,
			UserEmail = @UserEmail,
			UserHash = @UserHash,
			UserSalt = @UserSalt
	WHERE	UserId = @UserId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Verify
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultVerify]
	@UserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Account].[UserVault]
	SET		IsEmailVerified = 1
	WHERE	UserId = @UserId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Vault Delete Soft
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultDeleteSoft]
	@UserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Account].[UserVault]
	SET		IsActive = 0
	WHERE	UserId = @UserId

END;

-- =============================================