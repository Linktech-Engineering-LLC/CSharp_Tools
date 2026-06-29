/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `portal_activity_log`;
CREATE TABLE IF NOT EXISTS `portal_activity_log` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PortalUserId` bigint(20) unsigned NOT NULL,
  `ActivityType` varchar(100) NOT NULL,
  `ActivityDetail` text DEFAULT NULL,
  `IpAddress` varchar(45) DEFAULT NULL,
  `UserAgent` varchar(255) DEFAULT NULL,
  `PerformedBy` bigint(20) unsigned DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `IX_PortalActivityLog_PortalUserId` (`PortalUserId`),
  KEY `IX_PortalActivityLog_PerformedBy` (`PerformedBy`),
  CONSTRAINT `FK_PortalActivityLog_PerformedBy` FOREIGN KEY (`PerformedBy`) REFERENCES `users` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_PortalActivityLog_PortalUser` FOREIGN KEY (`PortalUserId`) REFERENCES `portal_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
