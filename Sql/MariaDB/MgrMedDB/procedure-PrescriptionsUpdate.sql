/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PrescriptionsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PrescriptionsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_DoctorId BIGINT UNSIGNED,
    IN p_MedicationId BIGINT UNSIGNED,
    IN p_PharmacyId BIGINT UNSIGNED,
    IN p_VisitId BIGINT UNSIGNED,
    IN p_DatePrescribed DATETIME,
    IN p_Dosage VARCHAR(255),
    IN p_Quantity INT UNSIGNED,
    IN p_Refills INT UNSIGNED,
    IN p_Instructions TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PatientId IS NULL OR p_PatientId = 0) THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND (p_DoctorId IS NULL OR p_DoctorId = 0) THEN
        SET v_Error = 'DoctorId is required';
    END IF;

    IF v_Error IS NULL AND (p_MedicationId IS NULL OR p_MedicationId = 0) THEN
        SET v_Error = 'MedicationId is required';
    END IF;

    IF v_Error IS NULL AND p_DatePrescribed IS NULL THEN
        SET v_Error = 'DatePrescribed is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE prescriptions
        SET
            PatientId = p_PatientId,
            DoctorId = p_DoctorId,
            MedicationId = p_MedicationId,
            PharmacyId = p_PharmacyId,
            VisitId = p_VisitId,
            DatePrescribed = p_DatePrescribed,
            Dosage = p_Dosage,
            Quantity = p_Quantity,
            Refills = p_Refills,
            Instructions = p_Instructions
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
