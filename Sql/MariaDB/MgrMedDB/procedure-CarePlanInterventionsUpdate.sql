/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `CarePlanInterventionsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CarePlanInterventionsUpdate`(
    IN  p_Id            BIGINT UNSIGNED,
    IN  p_Description   TEXT,
    IN  p_Frequency     VARCHAR(100),
    IN  p_TargetDate    DATE,
    IN  p_CompletedDate DATE,
    IN  p_Status        ENUM('Pending','InProgress','Completed','Cancelled'),
    OUT p_Success       BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_old_CarePlanId BIGINT UNSIGNED;
    DECLARE v_old_Status ENUM('Pending','InProgress','Completed','Cancelled');
    DECLARE v_final_completed DATE;

    SET p_Success = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Description IS NULL OR p_Description = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Status IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT CarePlanId, Status
        INTO v_old_CarePlanId, v_old_Status
        FROM care_plan_interventions
        WHERE Id = p_Id AND Active = TRUE;

        IF v_old_CarePlanId IS NULL THEN
            SET v_is_valid = FALSE;
        END IF;
    END IF;

    IF v_is_valid THEN
        IF p_Status = 'Completed' THEN
            IF p_CompletedDate IS NULL THEN
                SET v_final_completed = CURRENT_DATE;
            ELSE
                SET v_final_completed = p_CompletedDate;
            END IF;
        ELSE
            SET v_final_completed = NULL;
        END IF;
    END IF;

    IF v_is_valid THEN
        UPDATE care_plan_interventions
        SET
            Description   = p_Description,
            Frequency     = p_Frequency,
            TargetDate    = p_TargetDate,
            CompletedDate = v_final_completed,
            Status        = p_Status
        WHERE Id = p_Id;

        SET p_Success = TRUE;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
