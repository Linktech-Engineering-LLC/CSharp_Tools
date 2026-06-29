/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PharmacyDiscountClaimsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PharmacyDiscountClaimsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_PrescriptionId BIGINT UNSIGNED,
    IN p_DiscountCardId BIGINT UNSIGNED,
    IN p_NDC VARCHAR(20),
    IN p_DrugName VARCHAR(255),
    IN p_Quantity DECIMAL(10,2),
    IN p_DaysSupply INT UNSIGNED,
    IN p_ClaimDate DATETIME,
    IN p_DiscountPrice DECIMAL(10,2),
    IN p_PharmacyReimbursement DECIMAL(10,2),
    IN p_PBMFee DECIMAL(10,2),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PatientId IS NULL OR p_PatientId = 0) THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND (p_DiscountCardId IS NULL OR p_DiscountCardId = 0) THEN
        SET v_Error = 'DiscountCardId is required';
    END IF;

    IF v_Error IS NULL AND (p_NDC IS NULL OR p_NDC = '') THEN
        SET v_Error = 'NDC is required';
    END IF;

    IF v_Error IS NULL AND (p_DrugName IS NULL OR p_DrugName = '') THEN
        SET v_Error = 'DrugName is required';
    END IF;

    IF v_Error IS NULL AND p_Quantity IS NULL THEN
        SET v_Error = 'Quantity is required';
    END IF;

    IF v_Error IS NULL AND p_DaysSupply IS NULL THEN
        SET v_Error = 'DaysSupply is required';
    END IF;

    IF v_Error IS NULL AND p_ClaimDate IS NULL THEN
        SET v_Error = 'ClaimDate is required';
    END IF;

    IF v_Error IS NULL AND p_DiscountPrice IS NULL THEN
        SET v_Error = 'DiscountPrice is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE pharmacy_discount_claims
        SET
            PatientId = p_PatientId,
            PrescriptionId = p_PrescriptionId,
            DiscountCardId = p_DiscountCardId,
            NDC = p_NDC,
            DrugName = p_DrugName,
            Quantity = p_Quantity,
            DaysSupply = p_DaysSupply,
            ClaimDate = p_ClaimDate,
            DiscountPrice = p_DiscountPrice,
            PharmacyReimbursement = p_PharmacyReimbursement,
            PBMFee = p_PBMFee,
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
