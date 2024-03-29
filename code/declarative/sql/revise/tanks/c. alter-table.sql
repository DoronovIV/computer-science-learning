/*	ALTERING TABLES	*/


-- CHANGE COLUMN DATATYPE
USE QueryRepetition
GO
ALTER TABLE Prices
ALTER COLUMN Currency NVARCHAR(2);



-- ADD NEW COLUMN
USE QueryRepetition
GO
ALTER TABLE Guns
ADD [PriceId] INT FOREIGN KEY REFERENCES Prices (Id);


USE QueryRepetition
GO
ALTER TABLE Engines
ADD [PriceId] INT FOREIGN KEY REFERENCES Prices (Id);


USE QueryRepetition
GO
ALTER TABLE Budgets
ADD [Currency] NVARCHAR(2);



-- DROP COLUMN

USE QueryRepetition
GO
ALTER TABLE Prices
DROP COLUMN Units

USE QueryRepetition
GO
ALTER TABLE Budgets
DROP COLUMN Units