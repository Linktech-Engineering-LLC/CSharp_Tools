/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `MedicationDetailsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `MedicationDetailsUpdate`(
    IN p_MedicationId BIGINT UNSIGNED,
    IN p_Warnings TEXT,
    IN p_Interactions TEXT,
    IN p_SideEffects TEXT,
    IN p_MissedDose TEXT,
    IN p_Overdose TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_MedicationId IS NULL OR p_MedicationId = 0 THEN
        SET v_Error = 'Invalid MedicationId';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE medication_details
        SET
            Warnings = p_Warnings,
            Interactions = p_Interactions,
            SideEffects = p_SideEffects,
            MissedDose = p_MissedDose,
            Overdose = p_Overdose
        WHERE MedicationId = p_MedicationId;

        SELECT ROW_COUNT() AS RowsAffected;
    ELSE
        SELECT 0 AS RowsAffected, v_Error AS Error;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
