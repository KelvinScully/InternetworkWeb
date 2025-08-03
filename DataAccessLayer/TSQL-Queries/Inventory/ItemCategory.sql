
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Table: Inventory Item Category
-- =============================================

CREATE TABLE [Inventory].[ItemCategory] (
	[ItemCategoryId] [INT] NOT NULL,
	[ItemCategoryName] [VARCHAR](128) NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDataTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_InvItemCategory] PRIMARY KEY CLUSTERED (
		ItemCategoryId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Inventory Item Category Get
-- =============================================

