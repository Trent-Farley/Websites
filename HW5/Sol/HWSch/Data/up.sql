﻿CREATE TABLE Homeworks
(
	ID  INT PRIMARY KEY IDENTITY(1,1),
	Precedence INT NOT NULL,
	DueDate DATETIME NOT NULL,
	Course NVARCHAR(64) NOT NULL,
	Title NVARCHAR(64) NOT NULL,
	Note NVARCHAR(512),
	Fin bit default 0, 
);
GO