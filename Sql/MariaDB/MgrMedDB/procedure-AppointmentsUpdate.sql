/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AppointmentsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AppointmentsUpdate`(
    IN p_Id             BIGINT UNSIGNED,
    IN p_PatientId      BIGINT UNSIGNED,
    IN p_DoctorId       BIGINT UNSIGNED,
    IN p_FacilityId     BIGINT UNSIGNED,
    IN p_AppointmentDate DATETIME,
    IN p_DurationMinutes INT UNSIGNED,
    IN p_Reason         VARCHAR(255),
    IN p_Notes          TEXT,
    IN p_Status         ENUM('Scheduled','CheckedIn','Cancelled','NoShow','Completed'),
    IN p_VisitId        BIGINT UNSIGNED
)
BEGIN
    DECLARE v_exists INT DEFAULT 0;
    DECLARE v_success TINYINT UNSIGNED DEFAULT 1;
    DECLARE v_message VARCHAR(255) DEFAULT '';

    
    SELECT COUNT(*) INTO v_exists FROM appointments WHERE Id = p_Id;
    IF v_exists = 0 THEN
        SET v_success = 0;
        SET v_message = 'Appointment not found';
    END IF;

    
    IF v_success = 1 AND p_PatientId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists FROM patients WHERE Id = p_PatientId;
        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid PatientId';
        END IF;
    END IF;

    
    IF v_success = 1 AND p_DoctorId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists FROM doctors WHERE Id = p_DoctorId;
        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid DoctorId';
        END IF;
    END IF;

    
    IF v_success = 1 AND p_FacilityId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists FROM facilities WHERE Id = p_FacilityId;
        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid FacilityId';
        END IF;
    END IF;

    
    IF v_success = 1 AND p_DoctorId IS NOT NULL AND p_FacilityId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists
        FROM doctorfacilities
        WHERE DoctorId = p_DoctorId
          AND FacilityId = p_FacilityId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Doctor is not assigned to this Facility';
        END IF;
    END IF;

    
    IF v_success = 1 AND p_VisitId IS NOT NULL AND p_PatientId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists
        FROM visits
        WHERE Id = p_VisitId
          AND PatientId = p_PatientId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'VisitId does not belong to this Patient';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        UPDATE appointments
        SET
            PatientId       = COALESCE(p_PatientId, PatientId),
            DoctorId        = COALESCE(p_DoctorId, DoctorId),
            FacilityId      = COALESCE(p_FacilityId, FacilityId),
            AppointmentDate = COALESCE(p_AppointmentDate, AppointmentDate),
            DurationMinutes = COALESCE(p_DurationMinutes, DurationMinutes),
            Reason          = COALESCE(p_Reason, Reason),
            Notes           = COALESCE(p_Notes, Notes),
            Status          = COALESCE(p_Status, Status),
            VisitId         = COALESCE(p_VisitId, VisitId)
        WHERE Id = p_Id;

        SET v_message = 'Appointment updated';
    END IF;

    SELECT v_success AS Success,
           v_message AS Message;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
