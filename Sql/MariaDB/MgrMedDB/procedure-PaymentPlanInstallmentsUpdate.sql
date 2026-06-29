/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PaymentPlanInstallmentsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PaymentPlanInstallmentsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PaymentPlanId BIGINT UNSIGNED,
    IN p_DueDate DATE,
    IN p_AmountDue DECIMAL(10,2),
    IN p_AmountPaid DECIMAL(10,2),
    IN p_Status ENUM('Pending','Paid','Partial','Missed')
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PaymentPlanId IS NULL OR p_PaymentPlanId = 0) THEN
        SET v_Error = 'PaymentPlanId is required';
    END IF;

    IF v_Error IS NULL AND p_DueDate IS NULL THEN
        SET v_Error = 'DueDate is required';
    END IF;

    IF v_Error IS NULL AND p_AmountDue IS NULL THEN
        SET v_Error = 'AmountDue is required';
    END IF;

    IF v_Error IS NULL AND p_Status IS NULL THEN
        SET v_Error = 'Status is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE payment_plan_installments
        SET
            PaymentPlanId = p_PaymentPlanId,
            DueDate = p_DueDate,
            AmountDue = p_AmountDue,
            AmountPaid = p_AmountPaid,
            Status = p_Status
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
