/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PortalUsersCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PortalUsersCreate`(
    IN p_PatientId BIGINT UNSIGNED,
    IN p_StaffId BIGINT UNSIGNED,
    IN p_Username VARCHAR(100),
    IN p_PasswordHash VARCHAR(255),
    IN p_Role VARCHAR(50)
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Username IS NULL OR p_Username = '' THEN
        SET v_Error = 'Username is required';
    END IF;

    IF v_Error IS NULL AND (p_PasswordHash IS NULL OR p_PasswordHash = '') THEN
        SET v_Error = 'PasswordHash is required';
    END IF;

    IF v_Error IS NULL AND (p_Role IS NULL OR p_Role = '') THEN
        SET v_Error = 'Role is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO portal_users
        (PatientId, StaffId, Username, PasswordHash, Role)
        VALUES
        (p_PatientId, p_StaffId, p_Username, p_PasswordHash, p_Role);

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
