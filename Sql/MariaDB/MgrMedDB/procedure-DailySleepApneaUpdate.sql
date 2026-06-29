/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DailySleepApneaUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DailySleepApneaUpdate`(
    IN  p_Id          BIGINT UNSIGNED,
    IN  p_RecordedAt  DATETIME,
    IN  p_UsageHours  DECIMAL(4,2),
    IN  p_AHI         DECIMAL(4,2),
    IN  p_LeakRate    DECIMAL(6,2),
    IN  p_Pressure    DECIMAL(4,2),
    IN  p_IsCompliant TINYINT(1),
    IN  p_Notes       TEXT,
    OUT p_Success     BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_exists BIGINT UNSIGNED;

    SET p_Success = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_RecordedAt IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT Id INTO v_exists
        FROM daily_sleep_apnea
        WHERE Id = p_Id;

        IF v_exists IS NULL THEN
            SET v_is_valid = FALSE;
        END IF;
    END IF;

    IF v_is_valid THEN
        UPDATE daily_sleep_apnea
        SET
            RecordedAt  = p_RecordedAt,
            UsageHours  = p_UsageHours,
            AHI         = p_AHI,
            LeakRate    = p_LeakRate,
            Pressure    = p_Pressure,
            IsCompliant = p_IsCompliant,
            Notes       = p_Notes
        WHERE Id = p_Id;

        SET p_Success = TRUE;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
