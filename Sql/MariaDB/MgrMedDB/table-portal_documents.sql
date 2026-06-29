/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `portal_documents`;
CREATE TABLE IF NOT EXISTS `portal_documents` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PortalUserId` bigint(20) unsigned NOT NULL,
  `PatientId` bigint(20) unsigned DEFAULT NULL,
  `CategoryId` bigint(20) unsigned DEFAULT NULL,
  `FileName` varchar(255) NOT NULL,
  `FilePath` varchar(500) NOT NULL,
  `MimeType` varchar(100) NOT NULL,
  `FileSize` bigint(20) unsigned NOT NULL,
  `Active` tinyint(1) NOT NULL DEFAULT 1,
  `Notes` text DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `IX_PortalDocuments_PortalUserId` (`PortalUserId`),
  KEY `IX_PortalDocuments_PatientId` (`PatientId`),
  KEY `IX_PortalDocuments_CategoryId` (`CategoryId`),
  CONSTRAINT `FK_PortalDocuments_Category` FOREIGN KEY (`CategoryId`) REFERENCES `document_categories` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_PortalDocuments_Patient` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_PortalDocuments_PortalUser` FOREIGN KEY (`PortalUserId`) REFERENCES `portal_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
