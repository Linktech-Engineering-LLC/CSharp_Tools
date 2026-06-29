/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ConditionTypesUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ConditionTypesUpdate`(
    IN  p_Id          BIGINT UNSIGNED,
    IN  p_ICD10       VARCHAR(10),
    IN  p_Name        VARCHAR(255),
    IN  p_Description TEXT,
    IN  p_Chronic     BOOLEAN,
    OUT p_Success     BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_exists BIGINT UNSIGNED;

    SET p_Success = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_ICD10 IS NULL OR p_ICD10 = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Name IS NULL OR p_Name = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Chronic IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT Id INTO v_exists
        FROM condition_types
        WHERE Id = p_Id AND Active = TRUE;

        IF v_exists IS NULL THEN
            SET v_is_valid = FALSE;
        END IF;
    END IF;

    IF v_is_valid THEN
        UPDATE condition_types
        SET
            ICD10       = p_ICD10,
            Name        = p_Name,
            Description = p_Description,
            Chronic     = p_Chronic
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
