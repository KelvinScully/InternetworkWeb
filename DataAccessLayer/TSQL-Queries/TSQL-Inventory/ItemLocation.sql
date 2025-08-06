
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Table: Inventory Item Location
-- =============================================

CREATE TABLE [Inventory].[ItemLocation] (
	[ItemLocationId] [INT] NOT NULL,
	[ItemLocationName] [VARCHAR](128) NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDateTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_InvItemLocation] PRIMARY KEY CLUSTERED (
		ItemLocationId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Location Get
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemLocationGet]
	@ItemLocationId	INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		ItemLocationId,
		ItemLocationName,
		IsActive
	FROM	[Inventory].[ItemLocation]
	WHERE	(@ItemLocationId IS NULL OR @ItemLocationId = 0 OR ItemLocationId = @ItemLocationId)
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Location Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemLocationInsert]
	
	@ItemLocationName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemLocation]
		WHERE	TRIM(LOWER(ItemLocationName)) = TRIM(LOWER(@ItemLocationName))
	)
	BEGIN
		RAISERROR('Duplicate Location Found', 16, 1);
		RETURN;
	END;

	DECLARE @ItemLocationId INT = ISNULL((SELECT MAX (ItemLocationId) + 1 FROM Inventory.ItemLocation),1);

	INSERT INTO [Inventory].[ItemLocation] (
		[ItemLocationId],
		[ItemLocationName],

		[IsActive],
		[CreatedDateTime]
	)
	VALUES (
		@ItemLocationId,
		@ItemLocationName,
		1,
		SYSDATETIME()
	);

	SELECT @ItemLocationId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Location Update
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemLocationUpdate]
	@ItemLocationId		INT,
	@ItemLocationName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemLocation]
		WHERE	[ItemLocationId] <> @ItemLocationId
			AND TRIM(LOWER(ItemLocationName)) = TRIM(LOWER(@ItemLocationName))
	)
	BEGIN
		RAISERROR('Duplicate Location Found', 16, 1);
		RETURN;
	END;

	UPDATE	[Inventory].[ItemLocation]
	SET		ItemLocationName = @ItemLocationName
	WHERE	ItemLocationId = @ItemLocationId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Location Delete (Soft Delete)
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemLocationDeleteSoft]
	@ItemLocationId	INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Inventory].[ItemLocation]
	SET		IsActive = 0
	WHERE	ItemLocationId = @ItemLocationId
END;

-- =============================================