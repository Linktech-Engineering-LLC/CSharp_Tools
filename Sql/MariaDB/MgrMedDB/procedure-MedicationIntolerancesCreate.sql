/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `MedicationIntolerancesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `MedicationIntolerancesCreate`(
    IN p_PatientId BIGINT UNSIGNED,
    IN p_MedicationId BIGINT UNSIGNED,
    IN p_Description VARCHAR(255),
    IN p_Severity ENUM('Mild','Moderate','Severe'),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND (p_Description IS NULL OR p_Description = '') THEN
        SET v_Error = 'Description is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO medication_intolerances
        (PatientId, MedicationId, Description, Severity, Notes)
        VALUES
        (p_PatientId, p_MedicationId, p_Description, p_Severity, p_Notes);

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
