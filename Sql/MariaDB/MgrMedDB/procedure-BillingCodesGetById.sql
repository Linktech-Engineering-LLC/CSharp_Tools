/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `BillingCodesGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `BillingCodesGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_Code VARCHAR(20),
    OUT p_Description VARCHAR(255),
    OUT p_CodeType ENUM('CPT','HCPCS','ICD10','Custom'),
    OUT p_DefaultAmount DECIMAL(10,2),
    OUT p_Active BOOLEAN,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Code = NULL;
    SET p_Description = NULL;
    SET p_CodeType = NULL;
    SET p_DefaultAmount = NULL;
    SET p_Active = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            Code,
            Description,
            CodeType,
            DefaultAmount,
            Active
        INTO
            p_Code,
            p_Description,
            p_CodeType,
            p_DefaultAmount,
            p_Active
        FROM billing_codes
        WHERE Id = p_Id;

        IF p_Code IS NOT NULL THEN
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
