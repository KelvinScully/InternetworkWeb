
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Table: Inventory Item Location
-- =============================================

CREATE TABLE [Inventory].[ItemLocation] (
	[ItemLocationId] [INT] NOT NULL,
	[ItemLocationName] [VARCHAR](128) NOT NULL,

	[IsActive] [BIT] NOT NULL,
	[CreatedDataTime] [DATETIME2](7) NOT NULL
	CONSTRAINT [PK_InvItemLocation] PRIMARY KEY CLUSTERED (
		ItemLocationId ASC
	)
)

-- =============================================
GO;
-- =============================================
-- Author:		<Vasquez, Dillon>
-- Create date: 08/03/2025
-- Description:	Store Procedure: Inventory Item Location Get
-- =============================================

