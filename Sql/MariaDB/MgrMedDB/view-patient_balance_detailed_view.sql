/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_balance_detailed_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_balance_detailed_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,

    
    c.Id AS ChargeId,
    c.ChargeDate,
    c.Amount AS ChargeAmount,
    c.Notes AS ChargeNotes,
    c.Active AS ChargeActive,

    
    bc.Code AS BillingCode,
    bc.Description AS BillingDescription,
    bc.CodeType AS BillingCodeType,

    
    IFNULL(SUM(pay.Amount), 0) AS TotalPayments,

    
    (c.Amount - IFNULL(SUM(pay.Amount), 0)) AS ChargeBalance

FROM charges c
JOIN patients p
    ON p.Id = c.PatientId

LEFT JOIN billing_codes bc
    ON bc.Id = c.BillingCodeId

LEFT JOIN payments pay
    ON pay.ChargeId = c.Id

GROUP BY
    p.Id, p.FirstName, p.LastName,
    c.Id, c.ChargeDate, c.Amount, c.Notes, c.Active,
    bc.Code, bc.Description, bc.CodeType 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
