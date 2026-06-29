/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_conditions`;
CREATE TABLE IF NOT EXISTS `patient_conditions` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `ConditionTypeId` bigint(20) unsigned NOT NULL,
  `OnsetDate` date DEFAULT NULL,
  `ResolvedDate` date DEFAULT NULL,
  `Status` enum('Active','Resolved','Inactive','Remission') NOT NULL DEFAULT 'Active',
  `Severity` enum('Mild','Moderate','Severe','Critical') DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_PatientConditions_PatientId` (`PatientId`),
  KEY `IX_PatientConditions_ConditionTypeId` (`ConditionTypeId`),
  CONSTRAINT `FK_PatientConditions_ConditionType` FOREIGN KEY (`ConditionTypeId`) REFERENCES `condition_types` (`Id`) ON UPDATE CASCADE,
  CONSTRAINT `FK_PatientConditions_Patient` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
