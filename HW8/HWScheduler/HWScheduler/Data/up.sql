CREATE TABLE [Homework] (
  [Id] int PRIMARY KEY IDENTITY(1, 1) ,
  [ClassId] int,
  [LineId] int,
  [Precedence] int NOT NULL,
  [Duedate] datetime NOT NULL,
  [Title] nvarchar(64) NOT NULL,
  [Description] nvarchar(512),
  [Done] bit default 0
)
GO

CREATE TABLE [Tag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Tagname] nvarchar(512)
)
GO

CREATE TABLE [Course] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(25) NOT NULL,

)
GO
CREATE TABLE [HomeworkTags] (
	[HwId] int NOT NULL,
	[LabelId] int NOT NULL,
	FOREIGN KEY ([HwId]) REFERENCES [Homework]([Id]),
	FOREIGN KEY ([LabelId]) REFERENCES [Tag]([Id]),
	Unique([HwId], [LabelId])
)
GO
ALTER TABLE [Homework] ADD CONSTRAINT [HW_FK_COURSE] FOREIGN KEY ([ClassId]) REFERENCES [Course] ([Id])
GO

ALTER TABLE [Homework] ADD CONSTRAINT [HW_FK_TAG] FOREIGN KEY ([LineId]) REFERENCES [Tag] ([Id])
GO