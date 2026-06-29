/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AppointmentsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AppointmentsCreate`(
    IN p_PatientId       BIGINT UNSIGNED,
    IN p_DoctorId        BIGINT UNSIGNED,
    IN p_FacilityId      BIGINT UNSIGNED,
    IN p_AppointmentDate DATETIME,
    IN p_DurationMinutes INT UNSIGNED,
    IN p_Reason          VARCHAR(255),
    IN p_Notes           TEXT,
    IN p_Status          ENUM('Scheduled','CheckedIn','Cancelled','NoShow','Completed'),
    IN p_VisitId         BIGINT UNSIGNED
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

    IF v_success = 1 AND p_DoctorId IS NULL THEN
        SET v_success = 0;
        SET v_message = 'DoctorId is required';
    END IF;

    IF v_success = 1 AND p_FacilityId IS NULL THEN
        SET v_success = 0;
        SET v_message = 'FacilityId is required';
    END IF;

    IF v_success = 1 AND p_AppointmentDate IS NULL THEN
        SET v_success = 0;
        SET v_message = 'AppointmentDate is required';
    END IF;

    
    IF v_success = 1 THEN
        SELECT COUNT(*) INTO v_exists FROM patients WHERE Id = p_PatientId;
        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid PatientId';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        SELECT COUNT(*) INTO v_exists FROM doctors WHERE Id = p_DoctorId;
        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid DoctorId';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        SELECT COUNT(*) INTO v_exists FROM facilities WHERE Id = p_FacilityId;
        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid FacilityId';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        SELECT COUNT(*) INTO v_exists
        FROM doctorfacilities
        WHERE DoctorId = p_DoctorId
          AND FacilityId = p_FacilityId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Doctor is not assigned to this Facility';
        END IF;
    END IF;

    
    IF v_success = 1 AND p_VisitId IS NOT NULL THEN
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
        INSERT INTO appointments (
            PatientId, DoctorId, FacilityId, AppointmentDate,
            DurationMinutes, Reason, Notes, Status, VisitId
        ) VALUES (
            p_PatientId, p_DoctorId, p_FacilityId, p_AppointmentDate,
            COALESCE(p_DurationMinutes, 15), p_Reason, p_Notes,
            COALESCE(p_Status, 'Scheduled'), p_VisitId
        );

        SET v_new_id = LAST_INSERT_ID();
        SET v_message = 'Appointment created';
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
