/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DailySleepApneaBulkImport`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DailySleepApneaBulkImport`(
    IN p_PatientId   BIGINT UNSIGNED,
    IN p_RecordedAt  DATETIME,
    IN p_UsageHHMM   VARCHAR(10),
    IN p_AHI         DECIMAL(4,2),
    IN p_LeakRate    DECIMAL(6,2),
    IN p_Pressure    DECIMAL(4,2),
    IN p_Notes       TEXT,
    OUT p_Id         BIGINT,
    OUT p_Success    BOOLEAN,
    OUT p_Message    VARCHAR(255)
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_usage DECIMAL(4,2);

    SET p_Id = 0;
    SET p_Success = FALSE;
    SET p_Message = '';

    
    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_is_valid = FALSE;
        SET p_Message = 'Invalid PatientId';
    END IF;

    IF p_RecordedAt IS NULL THEN
        SET v_is_valid = FALSE;
        SET p_Message = 'RecordedAt is required';
    END IF;

    IF p_UsageHHMM IS NULL OR p_UsageHHMM = '' THEN
        SET v_is_valid = FALSE;
        SET p_Message = 'Usage time (HH:MM) is required';
    END IF;

    
    IF v_is_valid THEN
        SET v_usage = HHMMToDecimal(p_UsageHHMM);

        IF v_usage IS NULL THEN
            SET v_is_valid = FALSE;
            SET p_Message = 'Invalid HH:MM format';
        END IF;
    END IF;

    
    IF v_is_valid THEN
        INSERT INTO daily_sleep_apnea (
            PatientId,
            RecordedAt,
            UsageHours,
            AHI,
            LeakRate,
            Pressure,
            IsCompliant,
            Notes
        )
        VALUES (
            p_PatientId,
            p_RecordedAt,
            v_usage,
            p_AHI,
            p_LeakRate,
            p_Pressure,
            CASE WHEN v_usage >= 4.00 THEN 1 ELSE 0 END,
            p_Notes
        );

        SET p_Id = LAST_INSERT_ID();
        SET p_Success = TRUE;
        SET p_Message = 'Sleep apnea log imported';
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
