
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Table: Inventory Item Status
-- =============================================

CREATE TABLE [Inventory].[ItemStatus] (
	[ItemStatusId] [INT] NOT NULL,
	[ItemStatusName] [VARCHAR](128) NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDateTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_InvItemStatus] PRIMARY KEY CLUSTERED (
		ItemStatusId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Inventory Item Status Get
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemStatusGet]
	@ItemStatusId	INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ItemStatusId,
		ItemStatusName,
		IsActive
	FROM	[Inventory].[ItemStatus]
	WHERE	(@ItemStatusId IS NULL OR @ItemStatusId = 0 OR ItemStatusId = @ItemStatusId)
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Inventory Item Status Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemStatusInsert]

	@ItemStatusName VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT *
		FROM	[Inventory].[ItemStatus]
		WHERE	TRIM(LOWER(ItemStatusName)) = TRIM(LOWER(@ItemStatusName))
	)
	BEGIN
		RAISERROR('Duplicate Status Found', 16, 1);
		RETURN;
	END;

	DECLARE @ItemStatusId INT = ISNULL((SELECT MAX(ItemStatusId) + 1 FROM Inventory.ItemStatus),1);

	INSERT INTO [Inventory].[ItemStatus] (
		[ItemStatusId],
		[ItemStatusName],

		[IsActive],
		[CreatedDateTime]
	)
	VALUES (
		@ItemStatusId,
		@ItemStatusName,
		1,
		SYSDATETIME()
	);

	SELECT @ItemStatusId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Inventory Item Status Update
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemStatusUpdate]
	@ItemStatusId	INT,
	@ItemStatusName VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT *
		FROM	[Inventory].[ItemStatus]
		WHERE	[ItemStatusId] <> @ItemStatusId
			AND TRIM(LOWER(ItemStatusName)) = TRIM(LOWER(@ItemStatusName))
	)
	BEGIN
		RAISERROR('Duplicate Status Found', 16, 1);
		RETURN;
	END;

	UPDATE	[Inventory].[ItemStatus]
	SET		ItemStatusName = @ItemStatusName
	WHERE	ItemStatusId = @ItemStatusId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Inventory Item Status Delete (Soft Delete)
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemStatusDeleteSoft]
	@ItemStatusId	INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Inventory].[ItemStatus]
	SET		IsActive = 0
	WHERE	ItemStatusId = @ItemStatusId

END;
-- =============================================