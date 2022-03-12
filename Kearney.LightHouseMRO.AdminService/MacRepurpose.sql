-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema lighthousemrodev
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema lighthousemrodev
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `lighthousemrodev` DEFAULT CHARACTER SET utf8 ;
USE `lighthousemrodev` ;

-- -----------------------------------------------------
-- Table `lighthousemrodev`.`clientinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`clientinfo` (
  `ClientID` INT(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `DomainName` VARCHAR(50) NULL DEFAULT NULL,
  `ADSecurityGroup` VARCHAR(25) NULL DEFAULT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT NULL,
  `ClientName` VARCHAR(255) NOT NULL,
  `AzKeyName` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`ClientID`))
ENGINE = InnoDB
AUTO_INCREMENT = 22
DEFAULT CHARACTER SET = utf8
COMMENT = 'THis table will hold all the master list of clients created for MRO';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`clientcontactinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`clientcontactinfo` (
  `ContactId` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ClientID` INT(10) UNSIGNED NOT NULL,
  `ContactName` VARCHAR(100) NULL DEFAULT NULL,
  `ContactEmail` VARCHAR(100) NULL DEFAULT NULL,
  `ContactNumber` VARCHAR(20) NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT b'1',
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`ContactId`),
  INDEX `Client_ID_idx` (`ClientID` ASC) VISIBLE,
  CONSTRAINT `FK_ContactClient_ID`
    FOREIGN KEY (`ClientID`)
    REFERENCES `lighthousemrodev`.`clientinfo` (`ClientID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold contact informationfor the clients';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`clientadditionalcontactinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`clientadditionalcontactinfo` (
  `ContactDetailID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ContactId` INT(10) UNSIGNED NOT NULL,
  `ContactType` VARCHAR(50) NULL DEFAULT NULL,
  `ContactValue` VARCHAR(100) NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT b'1',
  PRIMARY KEY (`ContactDetailID`),
  INDEX `FK_Contact_ID_idx` (`ContactId` ASC) VISIBLE,
  CONSTRAINT `FK_Contact_ID`
    FOREIGN KEY (`ContactId`)
    REFERENCES `lighthousemrodev`.`clientcontactinfo` (`ContactId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 11
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold additional phone n email address for contact informationfor the clients';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`clientbusinessunit`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`clientbusinessunit` (
  `BUID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `BUName` VARCHAR(50) NOT NULL,
  `ClientID` INT(10) UNSIGNED NOT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`BUID`),
  INDEX `FK_BUClient_ID` (`ClientID` ASC) VISIBLE,
  CONSTRAINT `FK_BUClient_ID`
    FOREIGN KEY (`ClientID`)
    REFERENCES `lighthousemrodev`.`clientinfo` (`ClientID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 36
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold master list of BUs per project';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`locationinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`locationinfo` (
  `LocationID` INT(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NULL DEFAULT NULL,
  `ParentID` INT(11) UNSIGNED NULL DEFAULT NULL,
  `ValidTill` DATE NULL DEFAULT NULL,
  `ClientID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`LocationID`),
  UNIQUE INDEX `ClientID_UNIQUE` (`ClientID` ASC) VISIBLE,
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'THis table will hold all the master list of locations created for each client';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`clientlocationmapping`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`clientlocationmapping` (
  `MappingId` INT(10) UNSIGNED NOT NULL,
  `ClientID` INT(10) UNSIGNED NOT NULL,
  `LocationID` INT(10) UNSIGNED NOT NULL,
  `IsActive` BIT(1) NULL DEFAULT b'1',
  PRIMARY KEY (`MappingId`),
  INDEX `Client_ID_idx` (`ClientID` ASC) VISIBLE,
  INDEX `FK_LocationId_idx` (`LocationID` ASC) VISIBLE,
  CONSTRAINT `FK_Client_ID`
    FOREIGN KEY (`ClientID`)
    REFERENCES `lighthousemrodev`.`clientinfo` (`ClientID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_LocationId`
    FOREIGN KEY (`LocationID`)
    REFERENCES `lighthousemrodev`.`locationinfo` (`LocationID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will map clients to the locations';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`entityinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`entityinfo` (
  `EntityID` INT(10) UNSIGNED NOT NULL,
  `EntityName` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`EntityID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold master list of all MRO application entities';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`entityoperationmapping`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`entityoperationmapping` (
  `OperationID` INT(10) UNSIGNED NOT NULL,
  `EntityID` INT(10) UNSIGNED NOT NULL,
  `OperationName` VARCHAR(50) NOT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`OperationID`),
  INDEX `FK_Entity_ID` (`EntityID` ASC) VISIBLE,
  CONSTRAINT `FK_Entity_ID`
    FOREIGN KEY (`EntityID`)
    REFERENCES `lighthousemrodev`.`entityinfo` (`EntityID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold mapping for entities and operations that can be performed on the entity';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`plantregionmapping`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`plantregionmapping` (
  `PlantIDMapping` INT(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ParentID` INT(11) NULL DEFAULT NULL,
  `Name` VARCHAR(100) NULL DEFAULT NULL,
  `ValidTill` DATE NULL DEFAULT NULL,
  `PlantID` VARCHAR(100) NULL DEFAULT NULL,
  PRIMARY KEY (`PlantIDMapping`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'THis table will hold all the plants for all regions';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`projectmoduleinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`projectmoduleinfo` (
  `ModuleID` INT(10) UNSIGNED NOT NULL,
  `ModuleName` VARCHAR(50) NOT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`ModuleID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold master list of all MRO Project statuses';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`roleinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`roleinfo` (
  `RoleID` INT(10) UNSIGNED NOT NULL,
  `RoleName` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`RoleID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold master list of all MRO application roles';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`roleoperationmapping`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`roleoperationmapping` (
  `RoleOpMappingID` INT(10) UNSIGNED NOT NULL,
  `OperationID` INT(10) UNSIGNED NOT NULL,
  `RoleID` INT(10) UNSIGNED NOT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`RoleOpMappingID`),
  INDEX `FK_RoleOperation_OpId` (`OperationID` ASC) VISIBLE,
  INDEX `FK_RoleOp_ID` (`RoleID` ASC) VISIBLE,
  CONSTRAINT `FK_RoleOp_ID`
    FOREIGN KEY (`RoleID`)
    REFERENCES `lighthousemrodev`.`roleinfo` (`RoleID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_RoleOperation_OpId`
    FOREIGN KEY (`OperationID`)
    REFERENCES `lighthousemrodev`.`entityoperationmapping` (`OperationID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'Mapping operations to entities';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`statusinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`statusinfo` (
  `StatusID` INT(10) UNSIGNED NOT NULL,
  `StatusName` VARCHAR(50) NOT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`StatusID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will hold master list of all MRO Project statuses';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`userinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`userinfo` (
  `UserID` INT(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `EmailID` VARCHAR(100) NULL DEFAULT NULL,
  `IsInternalUser` TINYINT(4) NULL DEFAULT NULL,
  `Name` VARCHAR(255) NULL DEFAULT NULL,
  `CreatedOn` DATETIME NULL DEFAULT NULL,
  `CreatedBy` VARCHAR(45) CHARACTER SET 'latin1' NULL DEFAULT NULL,
  `IsActive` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`UserID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'THis table will hold all the user related information along with thier client mapping';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`userclientmapping`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`userclientmapping` (
  `UserMappingId` INT(10) UNSIGNED NOT NULL,
  `ClientID` INT(10) UNSIGNED NOT NULL,
  `UserID` INT(10) UNSIGNED NOT NULL,
  `IsActive` BIT(1) NULL DEFAULT b'1',
  PRIMARY KEY (`UserMappingId`),
  INDEX `Client_ID_idx` (`ClientID` ASC) VISIBLE,
  INDEX `User_ID_idx` (`UserID` ASC) VISIBLE,
  CONSTRAINT `FK_User_Client_ID`
    FOREIGN KEY (`ClientID`)
    REFERENCES `lighthousemrodev`.`clientinfo` (`ClientID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_User_ID`
    FOREIGN KEY (`UserID`)
    REFERENCES `lighthousemrodev`.`userinfo` (`UserID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will map users to the clients';


-- -----------------------------------------------------
-- Table `lighthousemrodev`.`userrolemapping`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `lighthousemrodev`.`userrolemapping` (
  `RoleMappingId` INT(10) UNSIGNED NOT NULL,
  `RoleID` INT(10) UNSIGNED NOT NULL,
  `UserID` INT(10) UNSIGNED NOT NULL,
  `IsActive` BIT(1) NULL DEFAULT b'1',
  PRIMARY KEY (`RoleMappingId`),
  INDEX `FK_User_Role_ID` (`RoleID` ASC) VISIBLE,
  INDEX `FK_RoleUser_ID` (`UserID` ASC) VISIBLE,
  CONSTRAINT `FK_RoleUser_ID`
    FOREIGN KEY (`UserID`)
    REFERENCES `lighthousemrodev`.`userinfo` (`UserID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_User_Role_ID`
    FOREIGN KEY (`RoleID`)
    REFERENCES `lighthousemrodev`.`roleinfo` (`RoleID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table will map roles to users';

USE `lighthousemrodev` ;

-- -----------------------------------------------------
-- procedure CreateClient
-- -----------------------------------------------------

DELIMITER $$
USE `lighthousemrodev`$$
CREATE DEFINER=`mromysqldevadmin`@`%` PROCEDURE `CreateClient`(IN clientData JSON, IN userId varchar(100))
BEGIN
DECLARE clientName VARCHAR(255);
DECLARE domainName VARCHAR(50);
DECLARE adsecgroup VARCHAR(50);
DECLARE azkeyname VARCHAR(50);
DECLARE last_inserted_client_id  INT(10) UNSIGNED;
DECLARE last_inserted_contact_id INT(10) UNSIGNED;
DECLARE i INT;
DECLARE k,j INT;
DECLARE n INT;

SET clientName =  JSON_EXTRACT(clientData,'$.basicInfo.clientName');
SET domainName = JSON_EXTRACT(clientData,'$.basicInfo.domainName');

INSERT INTO clientinfo(clientName,domainName,IsActive, createdOn, createdBy) 
VALUES(clientName, domainName,1, CURRENT_TIMESTAMP(), userid);
SET last_inserted_client_id = LAST_INSERT_ID();

SET adsecgroup = "MRO_" + SUBSTRING(clientName, 0, 8);
SET azkeyname ="AKMRO" + SUBSTRING(clientName, 0, 8);

SELECT JSON_EXTRACT(clientData, "$.businessUnits") INTO @businessUnits;

SET i=0;
WHILE i < JSON_LENGTH(@businessUnits) DO
   SELECT JSON_EXTRACT(@businessUnits,CONCAT('$[',i,']')) INTO @BUName;
   INSERT INTO clientbusinessunit(BUName, ClientID,IsActive, CreatedOn, CreatedBy)
   VALUES(@BUName, last_inserted_client_id, 1, CURRENT_TIMESTAMP(), userid);
SELECT i + 1 INTO i;
END WHILE;

SELECT "BU DONE", @businessUnits;

SELECT JSON_EXTRACT(clientData, "$.basicInfo.contacts") INTO @contacts;

SELECT "Contacts DONE", @contacts;

SET i=0;
WHILE  i < JSON_LENGTH(@contacts) DO
SELECT "INSIDE LOOP";

SELECT i as counter;

SET last_inserted_contact_id = 0;



SELECT JSON_EXTRACT(@contacts, CONCAT('$[', i, ']')) INTO @contact;


SELECT JSON_EXTRACT(@contact, "$.contactEmail") INTO @contactEmails;
SELECT JSON_EXTRACT(@contact, "$.contactNumber") INTO @contactNumbers;



SELECT  JSON_EXTRACT(@contact,"$.contactName")  INTO @CName;
SELECT  JSON_EXTRACT(@contactEmails,'$[0]' ) INTO @CEMail ;
SELECT JSON_EXTRACT(@contactNumbers,'$[0]' ) INTO @CNumber;
SELECT last_inserted_client_id AS ClientID;
  
   INSERT INTO clientcontactinfo(ContactName, ClientID,  ContactEmail, ContactNumber, IsActive, CreatedOn, CreatedBy)
    VALUES (@CName, last_inserted_client_id, @CEMail , @CNumber, 1, CURRENT_TIMESTAMP(), userid);
    
    SET last_inserted_contact_id = LAST_INSERT_ID();
    
     -- Loop through all additional emails for a given contact and add
	SET n=1;
    WHILE n < JSON_LENGTH(@contactEmails)  DO    
    SELECT JSON_EXTRACT(@contactEmails,CONCAT('$[',n,']')) INTO @email;
    INSERT INTO clientadditionalcontactinfo(ContactID, ContactType,ContactValue, IsActive)
    VALUES(last_inserted_contact_id,'EMAIL',@email,1)  ;
     SELECT n + 1 INTO n;
    END WHILE;
    
	  -- Loop through all additional phonenumbers for a given contact and add
     SET k=1;
    WHILE k < JSON_LENGTH(@contactNumbers)  DO  
     SELECT JSON_EXTRACT(@contactNumbers,CONCAT('$[',k,']')) INTO @CNo;
    INSERT INTO clientadditionalcontactinfo(ContactID, ContactType,ContactValue, IsActive)
    VALUES(last_inserted_contact_id,'PhoneNo', @CNo,1)     ;
    SELECT k + 1 INTO k;
    END WHILE; 
   
    
    
SELECT i + 1 INTO i;
END WHILE;

END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
