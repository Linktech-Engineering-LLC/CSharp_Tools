/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PaymentPlanPaymentsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PaymentPlanPaymentsCreate`(
    IN p_InstallmentId BIGINT UNSIGNED,
    IN p_PaymentId BIGINT UNSIGNED,
    IN p_AppliedAmount DECIMAL(10,2),
    IN p_AppliedDate DATETIME
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_InstallmentId IS NULL OR p_InstallmentId = 0 THEN
        SET v_Error = 'InstallmentId is required';
    END IF;

    IF v_Error IS NULL AND (p_PaymentId IS NULL OR p_PaymentId = 0) THEN
        SET v_Error = 'PaymentId is required';
    END IF;

    IF v_Error IS NULL AND p_AppliedAmount IS NULL THEN
        SET v_Error = 'AppliedAmount is required';
    END IF;

    IF v_Error IS NULL AND p_AppliedDate IS NULL THEN
        SET v_Error = 'AppliedDate is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO payment_plan_payments
        (InstallmentId, PaymentId, AppliedAmount, AppliedDate)
        VALUES
        (p_InstallmentId, p_PaymentId, p_AppliedAmount, p_AppliedDate);

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
