/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PacemakerDailyDataUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PacemakerDailyDataUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PacemakerId BIGINT UNSIGNED,
    IN p_RecordedAt DATETIME,
    IN p_BatteryPercent DECIMAL(5,2),
    IN p_PacingPercent DECIMAL(5,2),
    IN p_AtrialEvents INT UNSIGNED,
    IN p_VentricularEvents INT UNSIGNED,
    IN p_AFibEpisodes INT UNSIGNED,
    IN p_PVCs INT UNSIGNED,
    IN p_LeadImpedance DECIMAL(6,2),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PacemakerId IS NULL OR p_PacemakerId = 0) THEN
        SET v_Error = 'PacemakerId is required';
    END IF;

    IF v_Error IS NULL AND p_RecordedAt IS NULL THEN
        SET v_Error = 'RecordedAt is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE pacemaker_daily_data
        SET
            PacemakerId = p_PacemakerId,
            RecordedAt = p_RecordedAt,
            BatteryPercent = p_BatteryPercent,
            PacingPercent = p_PacingPercent,
            AtrialEvents = p_AtrialEvents,
            VentricularEvents = p_VentricularEvents,
            AFibEpisodes = p_AFibEpisodes,
            PVCs = p_PVCs,
            LeadImpedance = p_LeadImpedance,
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
