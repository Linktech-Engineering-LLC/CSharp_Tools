/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `CarePlanCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CarePlanCreate`(
    IN  p_PatientId       BIGINT UNSIGNED,
    IN  p_ConditionTypeId BIGINT UNSIGNED,
    IN  p_Title           VARCHAR(255),
    IN  p_Goal            TEXT,
    IN  p_StartDate       DATE,
    IN  p_EndDate         DATE,
    IN  p_Status          ENUM('Active','Completed','Cancelled'),
    IN  p_Notes           TEXT,
    OUT p_Id              BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_final_end DATE;

    SET p_Id = 0;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Title IS NULL OR p_Title = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Goal IS NULL OR p_Goal = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_StartDate IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Status IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Status = 'Active' THEN
        SET v_final_end = NULL;
    ELSE
        IF p_EndDate IS NULL THEN
            SET v_final_end = CURRENT_DATE;
        ELSE
            SET v_final_end = p_EndDate;
        END IF;
    END IF;

    IF v_is_valid THEN
        INSERT INTO care_plans (
            PatientId,
            ConditionTypeId,
            Title,
            Goal,
            StartDate,
            EndDate,
            Status,
            Notes,
            Active
        )
        VALUES (
            p_PatientId,
            p_ConditionTypeId,
            p_Title,
            p_Goal,
            p_StartDate,
            v_final_end,
            p_Status,
            p_Notes,
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
