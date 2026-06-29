/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `appointments`;
CREATE TABLE IF NOT EXISTS `appointments` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `DoctorId` bigint(20) unsigned NOT NULL,
  `FacilityId` bigint(20) unsigned NOT NULL,
  `AppointmentDate` datetime NOT NULL,
  `DurationMinutes` int(10) unsigned DEFAULT 15,
  `Reason` varchar(255) DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  `Status` enum('Scheduled','CheckedIn','Cancelled','NoShow','Completed') NOT NULL DEFAULT 'Scheduled',
  `VisitId` bigint(20) unsigned DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `IX_Appointments_PatientId` (`PatientId`),
  KEY `IX_Appointments_DoctorId` (`DoctorId`),
  KEY `IX_Appointments_FacilityId` (`FacilityId`),
  KEY `IX_Appointments_VisitId` (`VisitId`),
  CONSTRAINT `FK_Appointments_Doctors` FOREIGN KEY (`DoctorId`) REFERENCES `doctors` (`Id`),
  CONSTRAINT `FK_Appointments_Facilities` FOREIGN KEY (`FacilityId`) REFERENCES `facilities` (`Id`),
  CONSTRAINT `FK_Appointments_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_Appointments_Visits` FOREIGN KEY (`VisitId`) REFERENCES `visits` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
