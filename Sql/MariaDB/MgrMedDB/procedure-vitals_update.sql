/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `vitals_update`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `vitals_update`(
    IN p_Id BIGINT UNSIGNED,
    IN p_VisitId BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_RecordedAt DATETIME,
    IN p_Systolic INT UNSIGNED,
    IN p_Diastolic INT UNSIGNED,
    IN p_Pulse INT UNSIGNED,
    IN p_Oxygen INT UNSIGNED,
    IN p_Temperature DECIMAL(4,1),
    IN p_Weight DECIMAL(5,2),
    IN p_Notes TEXT,
    OUT p_Success TINYINT
)
BEGIN
    SET p_Success = 0;

    UPDATE vitals
    SET
        VisitId = p_VisitId,
        PatientId = p_PatientId,
        RecordedAt = p_RecordedAt,
        Systolic = p_Systolic,
        Diastolic = p_Diastolic,
        Pulse = p_Pulse,
        Oxygen = p_Oxygen,
        Temperature = p_Temperature,
        Weight = p_Weight,
        Notes = p_Notes
    WHERE Id = p_Id;

    IF ROW_COUNT() > 0 THEN
        SET p_Success = 1;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
