USE `DOCUMENT_MANAGEMENT`;
Create table TrainingDetails_data (`DATA_ID` INT NOT NULL auto_increment,`Training_index` INT NOT NULL,`Filepath` varchar(50),`minimum_version` varchar(10),primary key(`DATA_ID`));
ALTER TABLE TrainingDetails_data AUTO_INCREMENT=1;
INSERT INTO TrainingDetails_data(`Training_index`,`Filepath`,`minimum_version`) Values (4,'DEF','2.0'),(2,'ABC','1.0'),(4,'JKL','1.0'),(3,'JKL','2.0'),(2,'JKL','3.0'),(3,'ABC','1.0'),(4,'JKL','1.0'),(3,'JKL','4.0'),(2,'JKL','4.0'),(3,'ABC','3.0'),(1,'ABC','4.0'),(2,'JKL','2.0'),(1,'GHI','1.0'),(1,'GHI','1.0'),(1,'JKL','1.0'),(1,'JKL','4.0'),(4,'DEF','4.0'),(4,'GHI','4.0'),(1,'JKL','4.0'),(1,'DEF','2.0'),(4,'GHI','1.0'),(3,'GHI','2.0'),(3,'JKL','1.0'),(1,'ABC','4.0'),(3,'ABC','2.0'),(4,'ABC','1.0'),(3,'ABC','2.0'),(1,'ABC','1.0'),(3,'ABC','1.0'),(3,'ABC','3.0');