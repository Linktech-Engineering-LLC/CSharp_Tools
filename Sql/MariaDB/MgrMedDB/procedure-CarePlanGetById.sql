/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `CarePlanGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CarePlanGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_PatientId BIGINT UNSIGNED,
    OUT p_ConditionTypeId BIGINT UNSIGNED,
    OUT p_Title VARCHAR(255),
    OUT p_Goal TEXT,
    OUT p_StartDate DATE,
    OUT p_EndDate DATE,
    OUT p_Status ENUM('Active','Completed','Cancelled'),
    OUT p_Notes TEXT,
    OUT p_Active BOOLEAN,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_PatientId = NULL;
    SET p_ConditionTypeId = NULL;
    SET p_Title = NULL;
    SET p_Goal = NULL;
    SET p_StartDate = NULL;
    SET p_EndDate = NULL;
    SET p_Status = NULL;
    SET p_Notes = NULL;
    SET p_Active = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            PatientId,
            ConditionTypeId,
            Title,
            Goal,
            StartDate,
            EndDate,
            Status,
            Notes,
            Active
        INTO
            p_PatientId,
            p_ConditionTypeId,
            p_Title,
            p_Goal,
            p_StartDate,
            p_EndDate,
            p_Status,
            p_Notes,
            p_Active
        FROM care_plans
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
