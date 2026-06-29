/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `replace_placeholders`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `replace_placeholders`(
    IN p_template VARCHAR(1000),
    IN p_patient_id VARCHAR(100),
    IN p_generic_id VARCHAR(100),
    IN p_prescription_id VARCHAR(100),
    OUT p_result VARCHAR(1000)
)
BEGIN
    SET p_result = p_template;

    IF p_patient_id IS NOT NULL THEN
        SET p_result = REPLACE(p_result, '{patient_id}', p_patient_id);
    END IF;

    IF p_generic_id IS NOT NULL THEN
        SET p_result = REPLACE(p_result, '{id}', p_generic_id);
    END IF;

    IF p_prescription_id IS NOT NULL THEN
        SET p_result = REPLACE(p_result, '{prescription_id}', p_prescription_id);
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
