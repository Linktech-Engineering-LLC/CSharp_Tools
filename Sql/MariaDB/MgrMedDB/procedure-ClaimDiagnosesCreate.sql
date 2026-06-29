/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ClaimDiagnosesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ClaimDiagnosesCreate`(
    IN  p_ClaimId  BIGINT UNSIGNED,
    IN  p_ICD10    VARCHAR(10),
    IN  p_Sequence INT UNSIGNED,
    OUT p_Id       BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_ClaimId IS NULL OR p_ClaimId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_ICD10 IS NULL OR p_ICD10 = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Sequence IS NULL OR p_Sequence = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO claim_diagnoses (
            ClaimId,
            ICD10,
            Sequence,
            Active
        )
        VALUES (
            p_ClaimId,
            p_ICD10,
            p_Sequence,
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
