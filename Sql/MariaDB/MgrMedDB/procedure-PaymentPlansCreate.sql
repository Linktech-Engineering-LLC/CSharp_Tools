/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PaymentPlansCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PaymentPlansCreate`(
    IN p_PatientId BIGINT UNSIGNED,
    IN p_PlanStartDate DATE,
    IN p_PlanEndDate DATE,
    IN p_OriginalBalance DECIMAL(10,2),
    IN p_RemainingBalance DECIMAL(10,2),
    IN p_InstallmentAmount DECIMAL(10,2),
    IN p_Frequency ENUM('Weekly','BiWeekly','Monthly'),
    IN p_Status ENUM('Active','Completed','Defaulted','Cancelled'),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND p_PlanStartDate IS NULL THEN
        SET v_Error = 'PlanStartDate is required';
    END IF;

    IF v_Error IS NULL AND p_OriginalBalance IS NULL THEN
        SET v_Error = 'OriginalBalance is required';
    END IF;

    IF v_Error IS NULL AND p_RemainingBalance IS NULL THEN
        SET v_Error = 'RemainingBalance is required';
    END IF;

    IF v_Error IS NULL AND p_InstallmentAmount IS NULL THEN
        SET v_Error = 'InstallmentAmount is required';
    END IF;

    IF v_Error IS NULL AND p_Frequency IS NULL THEN
        SET v_Error = 'Frequency is required';
    END IF;

    IF v_Error IS NULL AND p_Status IS NULL THEN
        SET v_Error = 'Status is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO payment_plans
        (PatientId, PlanStartDate, PlanEndDate, OriginalBalance, RemainingBalance,
         InstallmentAmount, Frequency, Status, Notes)
        VALUES
        (p_PatientId, p_PlanStartDate, p_PlanEndDate, p_OriginalBalance, p_RemainingBalance,
         p_InstallmentAmount, p_Frequency, p_Status, p_Notes);

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
