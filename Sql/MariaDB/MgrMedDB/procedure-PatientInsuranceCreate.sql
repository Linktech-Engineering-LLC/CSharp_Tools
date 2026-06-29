/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PatientInsuranceCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PatientInsuranceCreate`(
    IN p_PatientId BIGINT UNSIGNED,
    IN p_PlanId BIGINT UNSIGNED,
    IN p_PolicyNumber VARCHAR(50),
    IN p_GroupNumber VARCHAR(50),
    IN p_Relationship ENUM('Self','Spouse','Child','Other'),
    IN p_EffectiveDate DATE,
    IN p_EndDate DATE,
    IN p_Copay DECIMAL(10,2),
    IN p_Deductible DECIMAL(10,2),
    IN p_IsPrimary TINYINT(1)
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND (p_PlanId IS NULL OR p_PlanId = 0) THEN
        SET v_Error = 'PlanId is required';
    END IF;

    IF v_Error IS NULL AND (p_PolicyNumber IS NULL OR p_PolicyNumber = '') THEN
        SET v_Error = 'PolicyNumber is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO patient_insurance
        (PatientId, PlanId, PolicyNumber, GroupNumber, Relationship,
         EffectiveDate, EndDate, Copay, Deductible, IsPrimary)
        VALUES
        (p_PatientId, p_PlanId, p_PolicyNumber, p_GroupNumber, p_Relationship,
         p_EffectiveDate, p_EndDate, p_Copay, p_Deductible, p_IsPrimary);

        SELECT LAST_INSERT_ID() AS NewId;
    ELSE
        SELECT NULL AS NewId, v_Error AS Error;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
