/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PrescriptionCoverageUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PrescriptionCoverageUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_MedicarePartDId BIGINT UNSIGNED,
    IN p_DrugName VARCHAR(255),
    IN p_NDC VARCHAR(20),
    IN p_Tier ENUM('1','2','3','4','Specialty'),
    IN p_PriorAuth TINYINT(1),
    IN p_StepTherapy TINYINT(1),
    IN p_QuantityLimit VARCHAR(50),
    IN p_CopayAmount DECIMAL(10,2),
    IN p_CoinsurancePct DECIMAL(5,2),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_MedicarePartDId IS NULL OR p_MedicarePartDId = 0) THEN
        SET v_Error = 'MedicarePartDId is required';
    END IF;

    IF v_Error IS NULL AND (p_DrugName IS NULL OR p_DrugName = '') THEN
        SET v_Error = 'DrugName is required';
    END IF;

    IF v_Error IS NULL AND p_Tier IS NULL THEN
        SET v_Error = 'Tier is required';
    END IF;

    IF v_Error IS NULL AND p_PriorAuth IS NULL THEN
        SET v_Error = 'PriorAuth flag is required';
    END IF;

    IF v_Error IS NULL AND p_StepTherapy IS NULL THEN
        SET v_Error = 'StepTherapy flag is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE prescription_coverage
        SET
            MedicarePartDId = p_MedicarePartDId,
            DrugName = p_DrugName,
            NDC = p_NDC,
            Tier = p_Tier,
            PriorAuth = p_PriorAuth,
            StepTherapy = p_StepTherapy,
            QuantityLimit = p_QuantityLimit,
            CopayAmount = p_CopayAmount,
            CoinsurancePct = p_CoinsurancePct,
            Notes = p_Notes
        WHERE Id = p_Id;

        SELECT ROW_COUNT() AS RowsAffected;
    ELSE
        SELECT 0 AS RowsAffected, v_Error AS Error;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
