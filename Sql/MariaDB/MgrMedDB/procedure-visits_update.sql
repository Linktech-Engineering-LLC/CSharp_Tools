/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `visits_update`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `visits_update`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_DoctorId BIGINT UNSIGNED,
    IN p_FacilityId BIGINT UNSIGNED,
    IN p_VisitDate DATETIME,
    IN p_Reason VARCHAR(255),
    IN p_Notes TEXT,
    IN p_Weight DECIMAL(5,2),
    IN p_BloodPressure VARCHAR(20),
    IN p_Temperature DECIMAL(4,1),
    OUT p_Success TINYINT
)
BEGIN
    SET p_Success = 0;

    UPDATE visits
    SET
        PatientId = p_PatientId,
        DoctorId = p_DoctorId,
        FacilityId = p_FacilityId,
        VisitDate = p_VisitDate,
        Reason = p_Reason,
        Notes = p_Notes,
        Weight = p_Weight,
        BloodPressure = p_BloodPressure,
        Temperature = p_Temperature
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
