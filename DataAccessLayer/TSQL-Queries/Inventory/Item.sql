
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 07/18/2025
-- Description:	Table: Inventory Item
-- =============================================

CREATE TABLE [Inventory].[Item] (
	[ItemId] [INT] NOT NULL,
	[ItemName] [VARCHAR](128) NOT NULL,
	[ItemQuantity] [DECIMAL](9,3) NOT NULL,
	
	[CategoryId] [INT] NOT NULL,
	[LocationId] [INT] NOT NULL,
	[StatusId] [INT] NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDataTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_InvItem] PRIMARY KEY CLUSTERED (
		ItemId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Get
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[ItemGet]
	@ItemId		INT,
	@IsActive	BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ITEM.ItemId,
		ITEM.ItemName,
		ITEM.ItemQuantity,

		ITEM.CategoryId,
		CATE.ItemCategoryName,
		ITEM.LocationId,
		LOCA.ItemLocationName,
		ITEM.StatusId,
		STAT.ItemStatusName,

		ITEM.IsActive
	FROM	[Inventory].[Item]	ITEM
		LEFT JOIN	[Inventory].[ItemCategory]	CATE
				ON	CATE.CategoryId = ITEM.CategoryId

		LEFT JOIN	[Inventory].[ItemLocation]	LOCA
				ON	LOCA.LocationId = ITEM.LocationId

		LEFT JOIN	[Inventory].[ItemStatus]	STAT
				ON	STAT.StatusId = ITEM.StatusId
	WHERE	(@ItemId IS NULL OR @ItemId = 0 OR ItemId = @ItemId)
		AND IsActive = @IsActive

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemInsert]
	
	@ItemName		VARCHAR(128),
	@ItemQuantity	DECIMAL(9,3),
	
	@CategoryId		INT,
	@LocationId		INT,
	@StatusId		INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Inventory].[Item]
		WHERE	TRIM(LOWER(ItemName)) = TRIM(LOWER(@ItemName))
	)
	BEGIN
		RAISERROR('Duplicate Item Found', 16, 1);
		RETURN;
	END;

	IF NOT EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemCategory]
		WHERE	[ItemCategoryId] = @CategoryId
	)
	BEGIN
		RAISERROR('No Category Found with that Id', 16, 1);
		RETURN;
	END;

	IF NOT EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemLocation]
		WHERE	[ItemLocationId] = @LocationId
	)
	BEGIN
		RAISERROR('No Location Found with that Id', 16, 1);
		RETURN;
	END;

	IF NOT EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemStatus]
		WHERE	[ItemStatusId] = @StatusId
	)
	BEGIN
		RAISERROR('No Status Found with that Id', 16, 1);
		RETURN;
	END;
	
	DECLARE @ItemId INT = ISNULL((SELECT MAX (ItemId) + 1 FROM Inventory.Item),1);

	INSERT INTO [Inventory].[Item] (
		[ItemId],
		[ItemName],
		[ItemQuantity],
		
		[CategoryId],
		[LocationId],
		[StatusId],

		[IsActive],
		[CreatedDataTime]
	)
	VALUES (
		@ItemId,
		@ItemName,
		@ItemQuantity,

		@CategoryId,
		@LocationId,
		@StatusId,

		1,
		SYSDATETIME()
	);

	SELECT @ItemId
	
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Update
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemUpdate]
	@ItemId			INT,
	@ItemName		VARCHAR(128),
	@ItemQuantity	DECIMAL(9,3),
	
	@CategoryId		INT,
	@LocationId		INT,
	@StatusId		INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Inventory].[Item]
		WHERE	[ItemId] <> @ItemId
			AND TRIM(LOWER(ItemName)) = TRIM(LOWER(@ItemName))
	)
	BEGIN
		RAISERROR('Duplicate Item Found', 16, 1);
		RETURN;
	END;

	IF NOT EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemCategory]
		WHERE	[ItemCategoryId] = @CategoryId
	)
	BEGIN
		RAISERROR('No Category Found with that Id', 16, 1);
		RETURN;
	END;

	IF NOT EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemLocation]
		WHERE	[ItemLocationId] = @LocationId
	)
	BEGIN
		RAISERROR('No Location Found with that Id', 16, 1);
		RETURN;
	END;

	IF NOT EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemStatus]
		WHERE	[ItemStatusId] = @StatusId
	)
	BEGIN
		RAISERROR('No Status Found with that Id', 16, 1);
		RETURN;
	END;

	UPDATE	[Inventory].[Item]
	SET		ItemName = @ItemName,
			ItemQuantity = @ItemQuantity,
			CategoryId = @CategoryId,
			LocationId = @LocationId,
			StatusId = @StatusId
	WHERE	ItemId = @ItemId
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Delete
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemDeleteSoft]
	@ItemId	INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Inventory].[Item]
	SET		IsActive = 0
	WHERE	ItemId = @ItemId
END;

-- =============================================