/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PortalActivityLogCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PortalActivityLogCreate`(
    IN p_PortalUserId BIGINT UNSIGNED,
    IN p_ActivityType VARCHAR(100),
    IN p_ActivityDetail TEXT,
    IN p_IpAddress VARCHAR(45),
    IN p_UserAgent VARCHAR(255),
    IN p_PerformedBy BIGINT UNSIGNED
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_PortalUserId IS NULL OR p_PortalUserId = 0 THEN
        SET v_Error = 'PortalUserId is required';
    END IF;

    IF v_Error IS NULL AND (p_ActivityType IS NULL OR p_ActivityType = '') THEN
        SET v_Error = 'ActivityType is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO portal_activity_log
        (PortalUserId, ActivityType, ActivityDetail, IpAddress, UserAgent, PerformedBy)
        VALUES
        (p_PortalUserId, p_ActivityType, p_ActivityDetail, p_IpAddress, p_UserAgent, p_PerformedBy);

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
