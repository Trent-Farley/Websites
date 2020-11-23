CREATE TABLE [Homework] (
  [Id] int PRIMARY KEY IDENTITY(1, 1) ,
  [ClassId] int,
  [InfoId] int,
  [Done] bit default 0,
  [LineId] int
)
GO

CREATE TABLE [Detail] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Precedence] int NOT NULL,
  [Duedate] datetime NOT NULL,
  [Title] nvarchar(64) NOT NULL,
  [Description] nvarchar(512)
)
GO

CREATE TABLE [Tag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Tagname] nvarchar(40)
)
GO

CREATE TABLE [Course] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Department] nvarchar(6) NOT NULL,
  [CourseNumber] int not null
)
GO

ALTER TABLE [Homework] ADD CONSTRAINT [HW_FK_COURSE] FOREIGN KEY ([ClassId]) REFERENCES [Course] ([Id])
GO

ALTER TABLE [Homework] ADD CONSTRAINT [HW_FK_DETAIL] FOREIGN KEY ([InfoId]) REFERENCES [Detail] ([Id])
GO

ALTER TABLE [Homework] ADD CONSTRAINT [HW_FK_TAG] FOREIGN KEY ([LineId]) REFERENCES [Tag] ([Id])
GO