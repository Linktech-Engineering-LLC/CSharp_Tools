/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PrescriptionReactionsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PrescriptionReactionsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PrescriptionId BIGINT UNSIGNED,
    IN p_Reaction VARCHAR(255),
    IN p_Severity ENUM('Mild','Moderate','Severe'),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PrescriptionId IS NULL OR p_PrescriptionId = 0) THEN
        SET v_Error = 'PrescriptionId is required';
    END IF;

    IF v_Error IS NULL AND (p_Reaction IS NULL OR p_Reaction = '') THEN
        SET v_Error = 'Reaction is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE prescription_reactions
        SET
            PrescriptionId = p_PrescriptionId,
            Reaction = p_Reaction,
            Severity = p_Severity,
            Notes = p_Notes
        WHERE Id = p_Id;

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
