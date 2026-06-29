/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `CarePlanInterventionsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CarePlanInterventionsCreate`(
    IN  p_CarePlanId    BIGINT UNSIGNED,
    IN  p_Description   TEXT,
    IN  p_Frequency     VARCHAR(100),
    IN  p_TargetDate    DATE,
    IN  p_CompletedDate DATE,
    IN  p_Status        ENUM('Pending','InProgress','Completed','Cancelled'),
    OUT p_Id            BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_final_completed DATE;

    SET p_Id = 0;

    IF p_CarePlanId IS NULL OR p_CarePlanId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Description IS NULL OR p_Description = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Status IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Status = 'Completed' THEN
        IF p_CompletedDate IS NULL THEN
            SET v_final_completed = CURRENT_DATE;
        ELSE
            SET v_final_completed = p_CompletedDate;
        END IF;
    ELSE
        SET v_final_completed = NULL;
    END IF;

    IF v_is_valid THEN
        INSERT INTO care_plan_interventions (
            CarePlanId,
            Description,
            Frequency,
            TargetDate,
            CompletedDate,
            Status,
            Active
        )
        VALUES (
            p_CarePlanId,
            p_Description,
            p_Frequency,
            p_TargetDate,
            v_final_completed,
            p_Status,
            TRUE
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
