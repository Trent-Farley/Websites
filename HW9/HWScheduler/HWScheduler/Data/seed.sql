﻿INSERT INTO [Course] ([Name])
	VALUES
	('CS133'),
	('WR411'),
	('MTH321');

INSERT INTO [Tag] ([Tagname])
	VALUES
	('Easy'),
	('Tougher'),
	('Unbelievable')




INSERT INTO [Homework] ( [ClassId], [Done], [Precedence], [DueDate], [Title], [Description]) 
	VALUES
	( 1, 0,3,'11/17/2020 1:09:00 PM', 'HW1', 'Just kickin it off'),
	( 2,0,5,'11/27/2020 1:19:00 PM', 'HW2', 'Gettin tougher'),
	( 3,0,4, '12/27/2020 1:09:00 PM', 'HW6', 'Outrageous');

INSERT INTO [HomeworkTags] ([Homework_Id], [Tag_Id])
	Values
	(1,1),
	(1,2),
	(2,1),
	(2,3)

