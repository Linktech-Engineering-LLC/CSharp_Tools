/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ClaimLineItemsListByClaim`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ClaimLineItemsListByClaim`(
    IN p_ClaimId BIGINT UNSIGNED
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    IF p_ClaimId IS NULL OR p_ClaimId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            Id,
            ClaimId,
            BillingCodeId,
            DiagnosisPointer,
            Quantity,
            UnitPrice,
            LineTotal,
            Active
        FROM claim_line_items
        WHERE ClaimId = p_ClaimId
          AND Active = TRUE
        ORDER BY Id;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
