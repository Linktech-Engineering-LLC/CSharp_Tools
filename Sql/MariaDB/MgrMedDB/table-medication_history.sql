/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `medication_history`;
CREATE TABLE IF NOT EXISTS `medication_history` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `MedicationId` bigint(20) unsigned NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date DEFAULT NULL,
  `Active` tinyint(1) NOT NULL DEFAULT 1,
  `Source` varchar(50) NOT NULL DEFAULT 'Provider',
  `EnteredBy` bigint(20) unsigned NOT NULL,
  `DiscontinuedBy` bigint(20) unsigned DEFAULT NULL,
  `DiscontinueReason` varchar(255) DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `IX_MedHistory_PatientId` (`PatientId`),
  KEY `IX_MedHistory_MedicationId` (`MedicationId`),
  KEY `IX_MedHistory_EnteredBy` (`EnteredBy`),
  KEY `IX_MedHistory_DiscontinuedBy` (`DiscontinuedBy`),
  CONSTRAINT `FK_MedHistory_DiscontinuedBy` FOREIGN KEY (`DiscontinuedBy`) REFERENCES `users` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_MedHistory_EnteredBy` FOREIGN KEY (`EnteredBy`) REFERENCES `users` (`Id`) ON UPDATE CASCADE,
  CONSTRAINT `FK_MedHistory_Medication` FOREIGN KEY (`MedicationId`) REFERENCES `medications` (`Id`) ON UPDATE CASCADE,
  CONSTRAINT `FK_MedHistory_Patient` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
