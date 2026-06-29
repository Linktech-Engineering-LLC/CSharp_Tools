/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `CarePlanReviewsGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CarePlanReviewsGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_CarePlanId BIGINT UNSIGNED,
    OUT p_ReviewDate DATETIME,
    OUT p_Reviewer   VARCHAR(255),
    OUT p_Summary    TEXT,
    OUT p_NextSteps  TEXT,
    OUT p_Active     BOOLEAN,
    OUT p_Found      BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_CarePlanId = NULL;
    SET p_ReviewDate = NULL;
    SET p_Reviewer   = NULL;
    SET p_Summary    = NULL;
    SET p_NextSteps  = NULL;
    SET p_Active     = NULL;
    SET p_Found      = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            CarePlanId,
            ReviewDate,
            Reviewer,
            Summary,
            NextSteps,
            Active
        INTO
            p_CarePlanId,
            p_ReviewDate,
            p_Reviewer,
            p_Summary,
            p_NextSteps,
            p_Active
        FROM care_plan_reviews
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
