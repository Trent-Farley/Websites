﻿ALTER TABLE [Homework] DROP CONSTRAINT [HW_FK_COURSE]
ALTER TABLE [Homework] DROP CONSTRAINT [HW_FK_DETAIL]
ALTER TABLE [Homework] DROP CONSTRAINT [HW_FK_TAG]

DROP TABLE [Course];
DROP TABLE [Homework];

DROP TABLE [Tag];
DROP TABLE [Detail];