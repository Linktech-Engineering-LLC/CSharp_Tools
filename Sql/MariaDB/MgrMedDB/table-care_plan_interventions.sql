/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `care_plan_interventions`;
CREATE TABLE IF NOT EXISTS `care_plan_interventions` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `CarePlanId` bigint(20) unsigned NOT NULL,
  `Description` text NOT NULL,
  `Frequency` varchar(100) DEFAULT NULL,
  `TargetDate` date DEFAULT NULL,
  `CompletedDate` date DEFAULT NULL,
  `Status` enum('Pending','InProgress','Completed','Cancelled') NOT NULL DEFAULT 'Pending',
  `Active` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`Id`),
  KEY `IX_CarePlanInterventions_CarePlanId` (`CarePlanId`),
  CONSTRAINT `FK_CarePlanInterventions_CarePlans` FOREIGN KEY (`CarePlanId`) REFERENCES `care_plans` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
