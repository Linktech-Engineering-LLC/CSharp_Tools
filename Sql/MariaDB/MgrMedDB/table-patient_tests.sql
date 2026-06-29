/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_tests`;
CREATE TABLE IF NOT EXISTS `patient_tests` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `TestTypeId` bigint(20) unsigned NOT NULL,
  `OrderedDate` date DEFAULT NULL,
  `PerformedDate` date DEFAULT NULL,
  `ResultSummary` varchar(500) DEFAULT NULL,
  `ResultDetails` text DEFAULT NULL,
  `OrderingProviderId` bigint(20) unsigned DEFAULT NULL,
  `PerformingFacilityId` bigint(20) unsigned DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `IX_PatientTests_PatientId` (`PatientId`),
  KEY `IX_PatientTests_TestTypeId` (`TestTypeId`),
  KEY `FK_patient_tests_ordering_provider` (`OrderingProviderId`),
  KEY `FK_PatientTests_PerformingFacility` (`PerformingFacilityId`),
  CONSTRAINT `FK_PatientTests_Patient` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_PatientTests_PerformingFacility` FOREIGN KEY (`PerformingFacilityId`) REFERENCES `facilities` (`Id`) ON UPDATE CASCADE,
  CONSTRAINT `FK_PatientTests_TestType` FOREIGN KEY (`TestTypeId`) REFERENCES `test_types` (`Id`) ON UPDATE CASCADE,
  CONSTRAINT `FK_patient_tests_ordering_provider` FOREIGN KEY (`OrderingProviderId`) REFERENCES `providers` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
