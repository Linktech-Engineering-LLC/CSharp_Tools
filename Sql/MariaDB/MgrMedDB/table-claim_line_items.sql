/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `claim_line_items`;
CREATE TABLE IF NOT EXISTS `claim_line_items` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `ClaimId` bigint(20) unsigned NOT NULL,
  `BillingCodeId` bigint(20) unsigned NOT NULL,
  `DiagnosisPointer` varchar(10) DEFAULT NULL,
  `Quantity` int(10) unsigned NOT NULL DEFAULT 1,
  `UnitPrice` decimal(10,2) NOT NULL,
  `LineTotal` decimal(10,2) NOT NULL,
  `Active` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`Id`),
  KEY `IX_ClaimLineItems_ClaimId` (`ClaimId`),
  KEY `FK_ClaimLineItems_BillingCodes` (`BillingCodeId`),
  CONSTRAINT `FK_ClaimLineItems_BillingCodes` FOREIGN KEY (`BillingCodeId`) REFERENCES `billing_codes` (`Id`),
  CONSTRAINT `FK_ClaimLineItems_Claims` FOREIGN KEY (`ClaimId`) REFERENCES `claims` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
