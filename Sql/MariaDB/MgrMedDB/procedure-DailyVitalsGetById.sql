/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DailyVitalsGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DailyVitalsGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_PatientId BIGINT UNSIGNED,
    OUT p_RecordedAt DATETIME,
    OUT p_Systolic INT UNSIGNED,
    OUT p_Diastolic INT UNSIGNED,
    OUT p_Pulse INT UNSIGNED,
    OUT p_Oxygen INT UNSIGNED,
    OUT p_Weight DECIMAL(5,2),
    OUT p_Temperature DECIMAL(4,1),
    OUT p_Notes TEXT,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_PatientId = NULL;
    SET p_RecordedAt = NULL;
    SET p_Systolic = NULL;
    SET p_Diastolic = NULL;
    SET p_Pulse = NULL;
    SET p_Oxygen = NULL;
    SET p_Weight = NULL;
    SET p_Temperature = NULL;
    SET p_Notes = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            PatientId,
            RecordedAt,
            Systolic,
            Diastolic,
            Pulse,
            Oxygen,
            Weight,
            Temperature,
            Notes
        INTO
            p_PatientId,
            p_RecordedAt,
            p_Systolic,
            p_Diastolic,
            p_Pulse,
            p_Oxygen,
            p_Weight,
            p_Temperature,
            p_Notes
        FROM daily_vitals
        WHERE Id = p_Id;

        IF p_PatientId IS NOT NULL THEN
            SET p_Found = TRUE;
        END IF;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
