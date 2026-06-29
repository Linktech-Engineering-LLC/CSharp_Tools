/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `procedure_documents`;
CREATE TABLE IF NOT EXISTS `procedure_documents` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `ProcedureId` bigint(20) unsigned NOT NULL,
  `DocumentId` bigint(20) unsigned NOT NULL,
  `LinkedAt` datetime NOT NULL DEFAULT current_timestamp(),
  `Notes` text DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ProcedureDocuments_ProcedureId` (`ProcedureId`),
  KEY `IX_ProcedureDocuments_DocumentId` (`DocumentId`),
  CONSTRAINT `FK_ProcedureDocuments_Documents` FOREIGN KEY (`DocumentId`) REFERENCES `documents` (`Id`),
  CONSTRAINT `FK_ProcedureDocuments_Procedures` FOREIGN KEY (`ProcedureId`) REFERENCES `procedures` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
