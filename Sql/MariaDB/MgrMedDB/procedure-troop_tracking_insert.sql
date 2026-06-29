/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `troop_tracking_insert`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `troop_tracking_insert`(
    IN p_PatientId BIGINT UNSIGNED,
    IN p_ClaimId BIGINT UNSIGNED,
    IN p_AmountApplied DECIMAL(10,2),
    IN p_Phase ENUM('Deductible','Initial','Gap','Catastrophic'),
    IN p_AppliedDate DATETIME,
    IN p_Notes TEXT,
    OUT p_NewId BIGINT UNSIGNED
)
BEGIN
    SET p_NewId = NULL;

    INSERT INTO troop_tracking (
        PatientId, ClaimId, AmountApplied,
        Phase, AppliedDate, Notes
    )
    VALUES (
        p_PatientId, p_ClaimId, p_AmountApplied,
        p_Phase, p_AppliedDate, p_Notes
    );

    SET p_NewId = LAST_INSERT_ID();
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
