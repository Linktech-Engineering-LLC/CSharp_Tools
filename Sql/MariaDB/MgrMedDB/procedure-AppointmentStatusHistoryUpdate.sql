/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AppointmentStatusHistoryUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AppointmentStatusHistoryUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_AppointmentId BIGINT UNSIGNED,
    IN p_Status VARCHAR(50),
    IN p_ChangedBy BIGINT UNSIGNED,
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_AppointmentId IS NULL OR p_AppointmentId = 0) THEN
        SET v_Error = 'AppointmentId is required';
    END IF;

    IF v_Error IS NULL AND (p_Status IS NULL OR p_Status = '') THEN
        SET v_Error = 'Status is required';
    END IF;

    IF v_Error IS NULL AND (p_ChangedBy IS NULL OR p_ChangedBy = 0) THEN
        SET v_Error = 'ChangedBy is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE appointment_status_history
        SET
            AppointmentId = p_AppointmentId,
            Status = p_Status,
            ChangedBy = p_ChangedBy,
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
