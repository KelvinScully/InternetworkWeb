
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Table: Account User Role
-- =============================================

CREATE TABLE [Account].[UserRole] (
	[UserRoleId] [INT] NOT NULL,
	[UserRoleName] [VARCHAR](128) NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDateTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_AccUserRole] PRIMARY KEY CLUSTERED (
		UserRoleId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Account User Role Get
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserRoleGet]
	@UserRoleId		INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		UserRoleId,
		UserRoleName,
		IsActive
	FROM	[Account].[UserRole]
	WHERE	(@UserRoleId IS NULL OR @UserRoleId = 0 OR UserRoleId = @UserRoleId)
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Account User Role Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserRoleInsert]

	@UserRoleName VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT *
		FROM	[Account].[UserRole]
		WHERE	TRIM(LOWER(UserRoleName)) = TRIM(LOWER(@UserRoleName))
	)
	BEGIN
		RAISERROR('Duplicate Role Found', 16, 1);
		RETURN;
	END;

	DECLARE @UserRoleId INT = ISNULL((SELECT MAX(UserRoleId) + 1 FROM Account.UserRole),1);

	INSERT INTO [Account].[UserRole] (
		[UserRoleId],
		[UserRoleName],

		[IsActive],
		[CreatedDateTime]
	)
	VALUES (
		@UserRoleId,
		@UserRoleName,
		1,
		SYSDATETIME()
	);

	SELECT @UserRoleId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Account User Role Update
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserRoleUpdate]
	@UserRoleId		INT,
	@UserRoleName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT *
		FROM	[Account].[UserRole]
		WHERE	[UserRoleId] <> @UserRoleId
			AND TRIM(LOWER(UserRoleName)) = TRIM(LOWER(@UserRoleName))
	)
	BEGIN
		RAISERROR('Duplicate Role Found', 16, 1);
		RETURN;
	END;

	UPDATE	[Account].[UserRole]
	SET		UserRoleName = @UserRoleName
	WHERE	UserRoleId = @UserRoleId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Account User Role Delete (Soft Delete)
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserRoleDeleteSoft]
	@UserRoleId	INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Account].[UserRole]
	SET		IsActive = 0
	WHERE	UserRoleId = @UserRoleId

END;
-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/07/2025
-- Description:	Store Procedure: Account User Role Activate
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserRoleActivate]
	@UserRoleId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Account].[UserRole]
	SET		IsActive = 1
	WHERE	UserRoleId = @UserRoleId

END;

-- =============================================