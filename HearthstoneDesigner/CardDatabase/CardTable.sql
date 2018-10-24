CREATE TABLE [dbo].[CardTable]
(
	[Name] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [CardType] VARCHAR(MAX) NOT NULL, 
    [Mana] INT NOT NULL, 
    [Rarity] INT NOT NULL, 
    [Attack] INT NOT NULL, 
    [Health] INT NOT NULL, 
    [Text] VARCHAR(MAX) NULL, 
    [IsRestricted] INT NOT NULL, 
    [ImagePath] VARCHAR(MAX) NULL
)
