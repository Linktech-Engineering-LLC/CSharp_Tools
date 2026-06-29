/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `referrals`;
CREATE TABLE IF NOT EXISTS `referrals` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `VisitId` bigint(20) unsigned DEFAULT NULL,
  `ReferredBy` bigint(20) unsigned DEFAULT NULL,
  `ReferralProviderId` bigint(20) unsigned DEFAULT NULL,
  `ReferralDate` datetime NOT NULL,
  `Reason` text NOT NULL,
  `Urgency` enum('Routine','Urgent','Stat') NOT NULL DEFAULT 'Routine',
  `Status` enum('Pending','Sent','Scheduled','Completed','Cancelled') NOT NULL DEFAULT 'Pending',
  `FollowUpDate` date DEFAULT NULL,
  `Notes` text DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Referrals_PatientId` (`PatientId`),
  KEY `IX_Referrals_VisitId` (`VisitId`),
  KEY `IX_Referrals_ReferralProviderId` (`ReferralProviderId`),
  CONSTRAINT `FK_Referrals_Patients` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_Referrals_ReferralProviders` FOREIGN KEY (`ReferralProviderId`) REFERENCES `referral_providers` (`Id`),
  CONSTRAINT `FK_Referrals_Visits` FOREIGN KEY (`VisitId`) REFERENCES `visits` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
