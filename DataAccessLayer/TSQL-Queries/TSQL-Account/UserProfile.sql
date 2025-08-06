
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/05/2025
-- Description:	Table: Account User Profile
-- =============================================

CREATE TABLE [Account].[UserProfile] (
	[UserId] [INT] NOT NULL,
	[UserFirstName] [VARCHAR](128) NOT NULL,
	[UserLastName] [VARCHAR](128) NOT NULL,

	[UserCountry] [VARCHAR](4) NOT NULL,
	[UserState] [VARBINARY](4) NOT NULL
	CONSTRAINT [Pk_AccUserProfile] PRIMARY KEY CLUSTERED (
		UserId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Account User Profile Get
-- =============================================

CREATE OR ALTER PROCEDURE [Account].[SpUserVaultGet]
	@UserId		INT
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
	WHERE	UserId = @UserId
END;

-- =============================================