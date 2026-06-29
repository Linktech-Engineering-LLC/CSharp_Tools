/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `statements_update`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `statements_update`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_StatementDate DATETIME,
    IN p_PeriodStart DATE,
    IN p_PeriodEnd DATE,
    IN p_BeginningBalance DECIMAL(10,2),
    IN p_NewCharges DECIMAL(10,2),
    IN p_Payments DECIMAL(10,2),
    IN p_Adjustments DECIMAL(10,2),
    IN p_EndingBalance DECIMAL(10,2),
    IN p_Notes TEXT,
    OUT p_Success TINYINT
)
BEGIN
    SET p_Success = 0;

    UPDATE statements
    SET
        PatientId = p_PatientId,
        StatementDate = p_StatementDate,
        PeriodStart = p_PeriodStart,
        PeriodEnd = p_PeriodEnd,
        BeginningBalance = p_BeginningBalance,
        NewCharges = p_NewCharges,
        Payments = p_Payments,
        Adjustments = p_Adjustments,
        EndingBalance = p_EndingBalance,
        Notes = p_Notes
    WHERE Id = p_Id;

    IF ROW_COUNT() > 0 THEN
        SET p_Success = 1;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
