/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `flowsheet_entries`;
CREATE TABLE IF NOT EXISTS `flowsheet_entries` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `VisitId` bigint(20) unsigned DEFAULT NULL,
  `FlowsheetId` bigint(20) unsigned NOT NULL,
  `EntryTime` datetime NOT NULL,
  `EnteredBy` bigint(20) unsigned DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FlowsheetEntries_PatientId` (`PatientId`),
  KEY `IX_FlowsheetEntries_VisitId` (`VisitId`),
  KEY `IX_FlowsheetEntries_FlowsheetId` (`FlowsheetId`),
  CONSTRAINT `FK_FlowsheetEntries_Flowsheets` FOREIGN KEY (`FlowsheetId`) REFERENCES `flowsheet_definitions` (`Id`),
  CONSTRAINT `FK_FlowsheetEntries_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_FlowsheetEntries_Visits` FOREIGN KEY (`VisitId`) REFERENCES `visits` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
