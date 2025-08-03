
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
-- Create date: 07/18/2025
-- Description:	Store Procedure: Inventory Item Get
-- =============================================

