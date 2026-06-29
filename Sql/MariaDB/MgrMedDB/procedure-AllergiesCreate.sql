/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AllergiesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AllergiesCreate`(
    IN p_PatientId    BIGINT UNSIGNED,
    IN p_Allergen     VARCHAR(255),
    IN p_AllergenType ENUM('Medication','Food','Environmental','Other'),
    IN p_Reaction     VARCHAR(255),
    IN p_Severity     ENUM('Mild','Moderate','Severe','Anaphylaxis'),
    IN p_Notes        TEXT
)
BEGIN
    DECLARE v_exists INT DEFAULT 0;
    DECLARE v_success TINYINT UNSIGNED DEFAULT 1;
    DECLARE v_message VARCHAR(255) DEFAULT '';
    DECLARE v_new_id BIGINT UNSIGNED DEFAULT NULL;

    
    IF p_PatientId IS NULL THEN
        SET v_success = 0;
        SET v_message = 'PatientId is required';
    END IF;

    IF v_success = 1 AND (p_Allergen IS NULL OR p_Allergen = '') THEN
        SET v_success = 0;
        SET v_message = 'Allergen is required';
    END IF;

    IF v_success = 1 AND p_AllergenType IS NULL THEN
        SET v_success = 0;
        SET v_message = 'AllergenType is required';
    END IF;

    
    IF v_success = 1 THEN
        SELECT COUNT(*) INTO v_exists
        FROM patients
        WHERE Id = p_PatientId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid PatientId';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        INSERT INTO allergies (
            PatientId, Allergen, AllergenType, Reaction, Severity, Notes
        ) VALUES (
            p_PatientId, p_Allergen, p_AllergenType,
            p_Reaction, COALESCE(p_Severity, 'Moderate'), p_Notes
        );

        SET v_new_id = LAST_INSERT_ID();
        SET v_message = 'Allergy created';
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
