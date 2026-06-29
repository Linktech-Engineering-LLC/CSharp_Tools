/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PacemakerDailyDataCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PacemakerDailyDataCreate`(
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

    IF p_PacemakerId IS NULL OR p_PacemakerId = 0 THEN
        SET v_Error = 'PacemakerId is required';
    END IF;

    IF v_Error IS NULL AND p_RecordedAt IS NULL THEN
        SET v_Error = 'RecordedAt is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO pacemaker_daily_data
        (PacemakerId, RecordedAt, BatteryPercent, PacingPercent, AtrialEvents, VentricularEvents, AFibEpisodes, PVCs, LeadImpedance, Notes)
        VALUES
        (p_PacemakerId, p_RecordedAt, p_BatteryPercent, p_PacingPercent, p_AtrialEvents, p_VentricularEvents, p_AFibEpisodes, p_PVCs, p_LeadImpedance, p_Notes);

        SELECT LAST_INSERT_ID() AS NewId;
    ELSE
        SELECT NULL AS NewId, v_Error AS Error;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
