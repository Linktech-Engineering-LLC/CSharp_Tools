/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DailyVitalsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DailyVitalsCreate`(
    IN  p_PatientId   BIGINT UNSIGNED,
    IN  p_RecordedAt  DATETIME,
    IN  p_Systolic    INT UNSIGNED,
    IN  p_Diastolic   INT UNSIGNED,
    IN  p_Pulse       INT UNSIGNED,
    IN  p_Oxygen      INT UNSIGNED,
    IN  p_Weight      DECIMAL(5,2),
    IN  p_Temperature DECIMAL(4,1),
    IN  p_Notes       TEXT,
    OUT p_Id          BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_RecordedAt IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO daily_vitals (
            PatientId,
            RecordedAt,
            Systolic,
            Diastolic,
            Pulse,
            Oxygen,
            Weight,
            Temperature,
            Notes
        )
        VALUES (
            p_PatientId,
            p_RecordedAt,
            p_Systolic,
            p_Diastolic,
            p_Pulse,
            p_Oxygen,
            p_Weight,
            p_Temperature,
            p_Notes
        );

        SET p_Id = LAST_INSERT_ID();
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
