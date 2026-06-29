/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `medicare_part_d`;
CREATE TABLE IF NOT EXISTS `medicare_part_d` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `PatientId` bigint(20) unsigned NOT NULL,
  `PlanName` varchar(255) NOT NULL,
  `PlanId` varchar(50) NOT NULL,
  `MemberId` varchar(50) NOT NULL,
  `GroupNumber` varchar(50) DEFAULT NULL,
  `EffectiveDate` date NOT NULL,
  `TerminationDate` date DEFAULT NULL,
  `LISLevel` enum('None','Partial','Full') NOT NULL DEFAULT 'None',
  `CoveragePhase` enum('Deductible','Initial','Gap','Catastrophic') NOT NULL DEFAULT 'Initial',
  `Notes` text DEFAULT NULL,
  `Created` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
