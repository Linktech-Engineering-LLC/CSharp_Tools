/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `statement_line_items`;
CREATE TABLE IF NOT EXISTS `statement_line_items` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `StatementId` bigint(20) unsigned NOT NULL,
  `LedgerEntryId` bigint(20) unsigned NOT NULL,
  `LineDate` datetime NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_StatementLineItems_StatementId` (`StatementId`),
  KEY `IX_StatementLineItems_LedgerEntryId` (`LedgerEntryId`),
  CONSTRAINT `FK_StatementLineItems_LedgerEntries` FOREIGN KEY (`LedgerEntryId`) REFERENCES `ledger_entries` (`Id`),
  CONSTRAINT `FK_StatementLineItems_Statements` FOREIGN KEY (`StatementId`) REFERENCES `statements` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
