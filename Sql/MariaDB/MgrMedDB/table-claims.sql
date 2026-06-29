/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `claims`;
CREATE TABLE IF NOT EXISTS `claims` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `VisitId` bigint(20) unsigned NOT NULL,
  `PatientId` bigint(20) unsigned NOT NULL,
  `PrimaryInsuranceId` bigint(20) unsigned DEFAULT NULL,
  `SecondaryInsuranceId` bigint(20) unsigned DEFAULT NULL,
  `ClaimDate` date NOT NULL,
  `Status` enum('Open','Submitted','Rejected','Paid','Denied','Closed') NOT NULL DEFAULT 'Open',
  `TotalCharge` decimal(10,2) DEFAULT 0.00,
  `TotalPaid` decimal(10,2) DEFAULT 0.00,
  `TotalAdjusted` decimal(10,2) DEFAULT 0.00,
  `PatientResponsibility` decimal(10,2) DEFAULT 0.00,
  `Notes` text DEFAULT NULL,
  `Active` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`Id`),
  KEY `IX_Claims_VisitId` (`VisitId`),
  KEY `IX_Claims_PatientId` (`PatientId`),
  CONSTRAINT `FK_Claims_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_Claims_Visits` FOREIGN KEY (`VisitId`) REFERENCES `visits` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
