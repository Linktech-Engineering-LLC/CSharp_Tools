/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `get_fhir_url`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_fhir_url`(
    IN p_provider_id CHAR(36),
    IN p_resource_type VARCHAR(100),
    OUT p_full_url VARCHAR(1000)
)
BEGIN
    DECLARE v_base VARCHAR(500);
    DECLARE v_path VARCHAR(500);

    SELECT base_url INTO v_base
    FROM api_providers
    WHERE provider_id = p_provider_id AND enabled = 1;

    SELECT relative_path INTO v_path
    FROM api_endpoints
    WHERE provider_id = p_provider_id
      AND resource_type = p_resource_type
      AND enabled = 1
    LIMIT 1;

    SET p_full_url = CONCAT(v_base, v_path);
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
