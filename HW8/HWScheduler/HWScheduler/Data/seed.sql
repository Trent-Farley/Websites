INSERT INTO [Course] ([Name])
	VALUES
	('CS133'),
	('WR411'),
	('MTH321');
INSERT INTO [Detail] ([Precedence], [DueDate], [Title], [Description])
	VALUES
	(3,'11/17/2020 1:09:00 PM', 'HW1', 'Just kickin it off'),
	(5,'11/27/2020 1:19:00 PM', 'HW2', 'Gettin tougher'),
	(4, '12/27/2020 1:09:00 PM', 'HW6', 'Outrageous');

INSERT INTO [Tag] ([Tagname])
	VALUES
	('Easy'),
	('Tougher'),
	('Unbelievable')


INSERT INTO [Homework] ( [ClassId],[InfoId] ,[Done] , [LineId] ) 
	VALUES
	( 1, 1, 0, 1),
	( 2,2,0,2),
	( 3,3,0,3);