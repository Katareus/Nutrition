CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL
);

CREATE TABLE [dbo].[Ingredients]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL,
	[CategoryId] int FOREIGN KEY REFERENCES Categories(Id) NOT NULL
);

CREATE TABLE [dbo].[Synonyms]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL,
	[IngredientId] int FOREIGN KEY REFERENCES Ingredients(Id) NOT NULL
);

