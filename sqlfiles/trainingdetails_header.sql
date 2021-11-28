
USE `DOCUMENT_MANAGEMENT`;
Create table TrainingDetails_header (`Training_index` INT NOT NULL auto_increment,`Company_ID` INT NOT NULL,`Version` varchar(50),`Training_ID` INT NOT NULL,primary key(`Training_index`));

INSERT INTO TrainingDetails_header(`Company_ID`,`Version`,`Training_ID`) Values(101,'1.0',1001),(102,'1.0',1001),(103,'1.0',1001),(104,'1.0',1001),(105,'1.0',1001),(101,'1.0',1002),(102,'1.0',1002),(103,'1.0',1002),(104,'1.0',1002),(105,'1.0',1002),(101,'1.0',1003),(102,'1.0',1003),(103,'1.0',1003),(104,'1.0',1003),(105,'1.0',1003),(101,'1.0',1004);

