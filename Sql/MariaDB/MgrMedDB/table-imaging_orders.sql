/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `imaging_orders`;
CREATE TABLE IF NOT EXISTS `imaging_orders` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `VisitId` bigint(20) unsigned DEFAULT NULL,
  `OrderedBy` bigint(20) unsigned DEFAULT NULL,
  `OrderDate` datetime NOT NULL,
  `Modality` enum('XR','CT','MRI','US','PET','NM','MAMMO','DEXA') NOT NULL,
  `BodyPart` varchar(255) NOT NULL,
  `Reason` text DEFAULT NULL,
  `Status` enum('Ordered','Scheduled','Completed','Cancelled') NOT NULL DEFAULT 'Ordered',
  PRIMARY KEY (`Id`),
  KEY `IX_ImagingOrders_PatientId` (`PatientId`),
  KEY `IX_ImagingOrders_VisitId` (`VisitId`),
  CONSTRAINT `FK_ImagingOrders_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_ImagingOrders_Visits` FOREIGN KEY (`VisitId`) REFERENCES `visits` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
