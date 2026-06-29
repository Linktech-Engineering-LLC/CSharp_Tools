/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `MedicationAdministrationsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `MedicationAdministrationsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_MedicationId BIGINT UNSIGNED,
    IN p_AdministeredBy BIGINT UNSIGNED,
    IN p_Dose VARCHAR(50),
    IN p_Route VARCHAR(50),
    IN p_Units VARCHAR(20),
    IN p_TimeGiven DATETIME,
    IN p_PRNReason VARCHAR(255),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PatientId IS NULL OR p_PatientId = 0) THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND (p_MedicationId IS NULL OR p_MedicationId = 0) THEN
        SET v_Error = 'MedicationId is required';
    END IF;

    IF v_Error IS NULL AND (p_AdministeredBy IS NULL OR p_AdministeredBy = 0) THEN
        SET v_Error = 'AdministeredBy is required';
    END IF;

    IF v_Error IS NULL AND (p_Dose IS NULL OR p_Dose = '') THEN
        SET v_Error = 'Dose is required';
    END IF;

    IF v_Error IS NULL AND (p_Route IS NULL OR p_Route = '') THEN
        SET v_Error = 'Route is required';
    END IF;

    IF v_Error IS NULL AND p_TimeGiven IS NULL THEN
        SET v_Error = 'TimeGiven is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE medication_administrations
        SET
            PatientId = p_PatientId,
            MedicationId = p_MedicationId,
            AdministeredBy = p_AdministeredBy,
            Dose = p_Dose,
            Route = p_Route,
            Units = p_Units,
            TimeGiven = p_TimeGiven,
            PRNReason = p_PRNReason,
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
