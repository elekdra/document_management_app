DROP TABLE `document_management`.`trainingdetails_header`;
USE `DOCUMENT_MANAGEMENT`;
Create table TrainingDetails_data (`DATA_ID` INT NOT NULL auto_increment,`Training_index` INT NOT NULL,`Filepath` varchar(50),`minimum_version` varchar(10),primary key(`DATA_ID`));
ALTER TABLE TrainingDetails_data AUTO_INCREMENT=1;
INSERT INTO TrainingDetails_data(`Training_index`,`Filepath`,`minimum_version`) Values(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(1,'ertuifghj','1.0'),(2,'ertuifghj','1.0'),(2,'ertuifghj','1.0'),(3,'ertuifghj','1.0'),(3,'ertuifghj','1.0'),(4,'ertuifghj','1.0'),(4,'ertuifghj','1.0'),(5,'ertuifghj','1.0'),(5,'ertuifghj','1.0'),(6,'ertuifghj','1.0'),(6,'ertuifghj','1.0'),(6,'ertuifghj','1.0'),(7,'ertuifghj','1.0'),(7,'ertuifghj','1.0'),(7,'ertuifghj','1.0');


