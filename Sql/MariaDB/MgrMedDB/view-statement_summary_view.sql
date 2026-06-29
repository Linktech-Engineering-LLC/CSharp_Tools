/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `statement_summary_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `statement_summary_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    COUNT(DISTINCT v.Id) AS TotalVisits,
    MIN(v.VisitDate) AS FirstVisitDate,
    MAX(v.VisitDate) AS LastVisitDate,

    
    COUNT(DISTINCT c.Id) AS TotalCharges,
    IFNULL(SUM(c.Amount), 0) AS TotalChargeAmount,
    MIN(c.ChargeDate) AS FirstChargeDate,
    MAX(c.ChargeDate) AS LastChargeDate,

    
    COUNT(DISTINCT pay.Id) AS TotalPayments,
    IFNULL(SUM(pay.Amount), 0) AS TotalPaymentAmount,
    MIN(pay.PaymentDate) AS FirstPaymentDate,
    MAX(pay.PaymentDate) AS LastPaymentDate,

    
    (IFNULL(SUM(c.Amount), 0) - IFNULL(SUM(pay.Amount), 0)) AS RemainingBalance

FROM patients p

LEFT JOIN visits v
    ON v.PatientId = p.Id

LEFT JOIN charges c
    ON c.PatientId = p.Id
    AND c.Active = 1

LEFT JOIN payments pay
    ON pay.PatientId = p.Id

GROUP BY
    p.Id,
    p.FirstName,
    p.LastName,
    p.MiddleName,
    p.DOB,
    p.Sex 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
