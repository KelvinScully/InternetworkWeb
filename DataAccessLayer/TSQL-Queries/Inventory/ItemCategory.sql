
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Table: Inventory Item Category
-- =============================================

CREATE TABLE [Inventory].[ItemCategory] (
	[ItemCategoryId] [INT] NOT NULL,
	[ItemCategoryName] [VARCHAR](128) NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDateTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_InvItemCategory] PRIMARY KEY CLUSTERED (
		ItemCategoryId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Category Get
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemCategoryGet]
	@ItemCategoryId	INT,
	@IsActive		BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ItemCategoryId,
		ItemCategoryName,
		IsActive
	FROM	[Inventory].[ItemCategory]
	WHERE	(@ItemCategoryId IS NULL OR @ItemCategoryId = 0 OR ItemCategoryId = @ItemCategoryId)
		AND IsActive = @IsActive
END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Category Insert
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemCategoryInsert]
	
	@ItemCategoryName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemCategory]
		WHERE	TRIM(LOWER(ItemCategoryName)) = TRIM(LOWER(@ItemCategoryName))
	)
	BEGIN
		RAISERROR('Duplicate Category Found', 16, 1);
		RETURN;
	END;

	DECLARE @ItemCategoryId INT = ISNULL((SELECT MAX(ItemCategoryId) + 1 FROM Inventory.ItemCategory),1);

	INSERT INTO [Inventory].[ItemCategory] (
		[ItemCategoryId],
		[ItemCategoryName],

		[IsActive],
		[CreatedDateTime]
	)
	VALUES (
		@ItemCategoryId,
		@ItemCategoryName,
		1,
		SYSDATETIME()
	);

	SELECT @ItemCategoryId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Category Update
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemCategoryUpdate]
	@ItemCategoryId		INT,
	@ItemCategoryName	VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT	*
		FROM	[Inventory].[ItemCategory]
		WHERE	[ItemCategoryId] <> @ItemCategoryId
			AND TRIM(LOWER(ItemCategoryName)) = TRIM(LOWER(@ItemCategoryName))
	)
	BEGIN
		RAISERROR('Duplicate Category Found', 16, 1);
		RETURN;
	END;

	UPDATE	[Inventory].[ItemCategory]
	SET		ItemCategoryName = @ItemCategoryName
	WHERE	ItemCategoryId = @ItemCategoryId

END;

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/04/2025
-- Description:	Store Procedure: Inventory Item Category Delete (Soft Delete)
-- =============================================

CREATE OR ALTER PROCEDURE [Inventory].[SpItemCategoryDeleteSoft]
	@ItemCategoryId	INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[Inventory].[ItemCategory]
	SET		IsActive = 0
	WHERE	ItemCategoryId = @ItemCategoryId
END;

-- =============================================