/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ConditionTypesGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ConditionTypesGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_ICD10 VARCHAR(10),
    OUT p_Name VARCHAR(255),
    OUT p_Description TEXT,
    OUT p_Chronic BOOLEAN,
    OUT p_Active BOOLEAN,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_ICD10 = NULL;
    SET p_Name = NULL;
    SET p_Description = NULL;
    SET p_Chronic = NULL;
    SET p_Active = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            ICD10,
            Name,
            Description,
            Chronic,
            Active
        INTO
            p_ICD10,
            p_Name,
            p_Description,
            p_Chronic,
            p_Active
        FROM condition_types
        WHERE Id = p_Id;

        IF p_ICD10 IS NOT NULL THEN
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
