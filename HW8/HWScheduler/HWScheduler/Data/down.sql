﻿DROP TABLE [HomeworkTags];

ALTER TABLE [Homework] DROP CONSTRAINT [HW_FK_COURSE];
DROP TABLE [Course];
DROP TABLE [Homework];
DROP TABLE [Tag];
