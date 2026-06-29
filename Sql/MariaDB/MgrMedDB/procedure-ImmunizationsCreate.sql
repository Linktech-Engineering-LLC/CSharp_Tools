/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ImmunizationsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ImmunizationsCreate`(
    IN p_PatientId BIGINT UNSIGNED,
    IN p_ImmunizationTypeId BIGINT UNSIGNED,
    IN p_DateGiven DATE,
    IN p_DoseNumber INT UNSIGNED,
    IN p_LotNumber VARCHAR(50),
    IN p_ExpirationDate DATE,
    IN p_Route VARCHAR(50),
    IN p_Site VARCHAR(50),
    IN p_AdministeredBy VARCHAR(100),
    IN p_FacilityId BIGINT UNSIGNED,
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND (p_ImmunizationTypeId IS NULL OR p_ImmunizationTypeId = 0) THEN
        SET v_Error = 'ImmunizationTypeId is required';
    END IF;

    IF v_Error IS NULL AND p_DateGiven IS NULL THEN
        SET v_Error = 'DateGiven is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO immunizations
        (PatientId, ImmunizationTypeId, DateGiven, DoseNumber, LotNumber,
         ExpirationDate, Route, Site, AdministeredBy, FacilityId, Notes)
        VALUES
        (p_PatientId, p_ImmunizationTypeId, p_DateGiven, p_DoseNumber, p_LotNumber,
         p_ExpirationDate, p_Route, p_Site, p_AdministeredBy, p_FacilityId, p_Notes);

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
