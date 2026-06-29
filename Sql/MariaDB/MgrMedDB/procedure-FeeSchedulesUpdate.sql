/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `FeeSchedulesUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `FeeSchedulesUpdate`(
    IN  p_Id            BIGINT UNSIGNED,
    IN  p_BillingCodeId BIGINT UNSIGNED,
    IN  p_Payer         VARCHAR(100),
    IN  p_AllowedAmount DECIMAL(10,2),
    IN  p_EffectiveDate DATE,
    IN  p_EndDate       DATE,
    OUT p_Success       BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_exists BIGINT UNSIGNED;

    SET p_Success = FALSE;

    
    SELECT Id INTO v_exists
    FROM fee_schedules
    WHERE Id = p_Id;

    IF v_exists IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    
    SELECT Id INTO v_exists
    FROM billing_codes
    WHERE Id = p_BillingCodeId AND Active = TRUE;

    IF v_exists IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    
    IF p_Payer IS NULL OR p_Payer = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    
    IF p_AllowedAmount <= 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    
    IF p_EndDate IS NOT NULL AND p_EndDate < p_EffectiveDate THEN
        SET v_is_valid = FALSE;
    END IF;

    
    IF v_is_valid THEN
        SELECT Id INTO v_exists
        FROM fee_schedules
        WHERE BillingCodeId = p_BillingCodeId
          AND Payer = p_Payer
          AND Id <> p_Id
          AND (
                (p_EndDate IS NULL AND EffectiveDate <= p_EffectiveDate)
                OR
                (EndDate IS NULL AND p_EffectiveDate <= EffectiveDate)
                OR
                (p_EndDate IS NOT NULL AND EndDate IS NOT NULL AND p_EffectiveDate <= EndDate AND p_EndDate >= EffectiveDate)
              )
        LIMIT 1;

        IF v_exists IS NOT NULL THEN
            SET v_is_valid = FALSE;
        END IF;
    END IF;

    IF v_is_valid THEN
        UPDATE fee_schedules
        SET
            BillingCodeId = p_BillingCodeId,
            Payer         = p_Payer,
            AllowedAmount = p_AllowedAmount,
            EffectiveDate = p_EffectiveDate,
            EndDate       = p_EndDate
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
