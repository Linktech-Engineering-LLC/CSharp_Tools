/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AuditLogCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AuditLogCreate`(
    IN p_UserId      BIGINT UNSIGNED,
    IN p_Action      VARCHAR(100),
    IN p_TargetTable VARCHAR(100),
    IN p_TargetId    BIGINT UNSIGNED,
    IN p_IpAddress   VARCHAR(45)
)
BEGIN
    DECLARE v_exists INT DEFAULT 0;
    DECLARE v_success TINYINT UNSIGNED DEFAULT 1;
    DECLARE v_message VARCHAR(255) DEFAULT '';
    DECLARE v_new_id BIGINT UNSIGNED DEFAULT NULL;

    
    IF p_Action IS NULL OR p_Action = '' THEN
        SET v_success = 0;
        SET v_message = 'Action is required';
    END IF;

    IF v_success = 1 AND (p_TargetTable IS NULL OR p_TargetTable = '') THEN
        SET v_success = 0;
        SET v_message = 'TargetTable is required';
    END IF;

    
    IF v_success = 1 AND p_UserId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists
        FROM portal_users
        WHERE Id = p_UserId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid UserId';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        INSERT INTO audit_log (
            UserId, Action, TargetTable, TargetId, IpAddress
        ) VALUES (
            p_UserId, p_Action, p_TargetTable, p_TargetId, p_IpAddress
        );

        SET v_new_id = LAST_INSERT_ID();
        SET v_message = 'Audit entry created';
    END IF;

    SELECT v_success AS Success,
           v_message AS Message,
           v_new_id AS Id;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
