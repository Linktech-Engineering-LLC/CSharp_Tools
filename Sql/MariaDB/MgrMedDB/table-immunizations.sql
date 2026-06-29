/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `immunizations`;
CREATE TABLE IF NOT EXISTS `immunizations` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `ImmunizationTypeId` bigint(20) unsigned NOT NULL,
  `DateGiven` date NOT NULL,
  `DoseNumber` int(10) unsigned DEFAULT NULL,
  `LotNumber` varchar(50) DEFAULT NULL,
  `ExpirationDate` date DEFAULT NULL,
  `Route` varchar(50) DEFAULT NULL,
  `Site` varchar(50) DEFAULT NULL,
  `AdministeredBy` varchar(100) DEFAULT NULL,
  `FacilityId` bigint(20) unsigned DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `IX_Immunizations_PatientId` (`PatientId`),
  KEY `IX_Immunizations_ImmunizationTypeId` (`ImmunizationTypeId`),
  KEY `IX_Immunizations_FacilityId` (`FacilityId`),
  CONSTRAINT `FK_Immunizations_Facilities` FOREIGN KEY (`FacilityId`) REFERENCES `facilities` (`Id`),
  CONSTRAINT `FK_Immunizations_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_Immunizations_Types` FOREIGN KEY (`ImmunizationTypeId`) REFERENCES `immunization_types` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
