/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DailySleepApneaGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DailySleepApneaGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_PatientId BIGINT UNSIGNED,
    OUT p_RecordedAt DATETIME,
    OUT p_UsageHours DECIMAL(4,2),
    OUT p_AHI DECIMAL(4,2),
    OUT p_LeakRate DECIMAL(6,2),
    OUT p_Pressure DECIMAL(4,2),
    OUT p_IsCompliant TINYINT(1),
    OUT p_Notes TEXT,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_PatientId = NULL;
    SET p_RecordedAt = NULL;
    SET p_UsageHours = NULL;
    SET p_AHI = NULL;
    SET p_LeakRate = NULL;
    SET p_Pressure = NULL;
    SET p_IsCompliant = NULL;
    SET p_Notes = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            PatientId,
            RecordedAt,
            UsageHours,
            AHI,
            LeakRate,
            Pressure,
            IsCompliant,
            Notes
        INTO
            p_PatientId,
            p_RecordedAt,
            p_UsageHours,
            p_AHI,
            p_LeakRate,
            p_Pressure,
            p_IsCompliant,
            p_Notes
        FROM daily_sleep_apnea
        WHERE Id = p_Id;

        IF p_PatientId IS NOT NULL THEN
            SET p_Found = TRUE;
        END IF;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
