DROP DATABASE `document_management` ;
CREATE DATABASE `DOCUMENT_MANAGEMENT`;
USE `DOCUMENT_MANAGEMENT`;
Create table Company (`COMPANY_ID` INT NOT NULL auto_increment,`COMPANY_NAME` VARCHAR(40) NOT NULL,primary key(`COMPANY_ID`));
ALTER TABLE Company AUTO_INCREMENT=100;
INSERT INTO Company(`COMPANY_NAME`) Values('HPCL'),('IOCL'),('BPL'),('GRK');