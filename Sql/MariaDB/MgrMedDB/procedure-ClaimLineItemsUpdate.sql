/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ClaimLineItemsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ClaimLineItemsUpdate`(
    IN  p_Id               BIGINT UNSIGNED,
    IN  p_BillingCodeId    BIGINT UNSIGNED,
    IN  p_DiagnosisPointer VARCHAR(10),
    IN  p_Quantity         INT UNSIGNED,
    IN  p_UnitPrice        DECIMAL(10,2),
    IN  p_LineTotal        DECIMAL(10,2),
    OUT p_Success          BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_exists BIGINT UNSIGNED;

    SET p_Success = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_BillingCodeId IS NULL OR p_BillingCodeId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Quantity IS NULL OR p_Quantity = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_UnitPrice IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_LineTotal IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT Id INTO v_exists
        FROM claim_line_items
        WHERE Id = p_Id AND Active = TRUE;

        IF v_exists IS NULL THEN
            SET v_is_valid = FALSE;
        END IF;
    END IF;

    IF v_is_valid THEN
        UPDATE claim_line_items
        SET
            BillingCodeId    = p_BillingCodeId,
            DiagnosisPointer = p_DiagnosisPointer,
            Quantity         = p_Quantity,
            UnitPrice        = p_UnitPrice,
            LineTotal        = p_LineTotal
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
