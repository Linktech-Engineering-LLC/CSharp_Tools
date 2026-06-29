/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `lab_orders`;
CREATE TABLE IF NOT EXISTS `lab_orders` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `DoctorId` bigint(20) unsigned NOT NULL,
  `FacilityId` bigint(20) unsigned DEFAULT NULL,
  `LabTestId` bigint(20) unsigned NOT NULL,
  `OrderDate` datetime NOT NULL,
  `Status` enum('Ordered','Collected','Completed','Cancelled') NOT NULL DEFAULT 'Ordered',
  `Notes` text DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_LabOrders_PatientId` (`PatientId`),
  KEY `IX_LabOrders_DoctorId` (`DoctorId`),
  KEY `IX_LabOrders_FacilityId` (`FacilityId`),
  KEY `IX_LabOrders_LabTestId` (`LabTestId`),
  CONSTRAINT `FK_LabOrders_Doctors` FOREIGN KEY (`DoctorId`) REFERENCES `doctors` (`Id`),
  CONSTRAINT `FK_LabOrders_Facilities` FOREIGN KEY (`FacilityId`) REFERENCES `facilities` (`Id`),
  CONSTRAINT `FK_LabOrders_LabTests` FOREIGN KEY (`LabTestId`) REFERENCES `lab_tests` (`Id`),
  CONSTRAINT `FK_LabOrders_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
