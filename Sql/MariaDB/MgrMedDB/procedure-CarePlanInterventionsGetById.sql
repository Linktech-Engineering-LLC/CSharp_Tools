/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `CarePlanInterventionsGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CarePlanInterventionsGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_CarePlanId BIGINT UNSIGNED,
    OUT p_Description TEXT,
    OUT p_Frequency VARCHAR(100),
    OUT p_TargetDate DATE,
    OUT p_CompletedDate DATE,
    OUT p_Status ENUM('Pending','InProgress','Completed','Cancelled'),
    OUT p_Active BOOLEAN,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_CarePlanId = NULL;
    SET p_Description = NULL;
    SET p_Frequency = NULL;
    SET p_TargetDate = NULL;
    SET p_CompletedDate = NULL;
    SET p_Status = NULL;
    SET p_Active = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            CarePlanId,
            Description,
            Frequency,
            TargetDate,
            CompletedDate,
            Status,
            Active
        INTO
            p_CarePlanId,
            p_Description,
            p_Frequency,
            p_TargetDate,
            p_CompletedDate,
            p_Status,
            p_Active
        FROM care_plan_interventions
        WHERE Id = p_Id;

        IF p_CarePlanId IS NOT NULL THEN
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
