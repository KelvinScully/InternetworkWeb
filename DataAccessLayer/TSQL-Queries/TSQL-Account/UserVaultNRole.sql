
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/05/2025
-- Description:	Table: Account User Vault N Role
-- =============================================

CREATE TABLE [Account].[UserVaultNRole] (
	[UserId] [INT] NOT NULL,
	[UserRoleId] [INT] NOT NULL,

	[CreatedDateTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_AccUserVaultNRole] PRIMARY KEY CLUSTERED (
		UserId		ASC,
		UserRoleId	ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/05/2025
-- Description:	Store Procedure: Account User Vault N Role Get
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[UserVaultNRoleGet]
	@UserId		INT,
	@UserRoleId	INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		AccUSERNROLE.UserId,
		AccUSERNROLE.UserRoleId,
		AccROLE.UserRoleName
	FROM	[Account].[UserVaultNRole]	AccUSERNROLE
		LEFT JOIN	[Account].[UserRole]	AccROLE
				ON	AccROLE.UserRoleId = AccUSERNROLE.UserRoleId
		LEFT JOIN	[Account].[UserVault]	AccUSER
				ON	AccUSER.UserId = AccUSERNROLE.UserId
	WHERE	(@UserId IS NULL OR @UserId = 0 OR AccUSERNROLE.UserId = @UserId)
		AND (@UserRoleId IS NULL OR @UserRoleId = 0 OR AccUSERNROLE.UserRoleId = @UserRoleId)
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/05/2025
-- Description:	Store Procedure: Account User Vault N Role Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[UserVaultNRoleInsert]
	@UserId		INT,
	@UserRoleId	INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Account].[UserVaultNRole]
		WHERE	UserId = @UserId AND UserRoleId = @UserRoleId
	)
	BEGIN
		RAISERROR('User already has that role assigned.', 16, 1);
		RETURN;
	END

	INSERT INTO [Account].[UserVaultNRole] (
		UserId,
		UserRoleId,
		CreatedDateTime
	)
	VALUES (
		@UserId,
		@UserRoleId,
		SYSDATETIME()
	);
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/05/2025
-- Description:	Store Procedure: Account User Vault N Role Delete
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[UserVaultNRoleDelete]
	@UserId		INT,
	@UserRoleId	INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Account].[UserVaultNRole]
	WHERE UserId = @UserId AND UserRoleId = @UserRoleId
END;

-- =============================================