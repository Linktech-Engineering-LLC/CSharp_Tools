/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_statement_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_statement_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,

    
    v.Id AS VisitId,
    v.VisitDate,
    v.Reason AS VisitReason,

    
    c.Id AS ChargeId,
    c.ChargeDate,
    c.Amount AS ChargeAmount,
    bc.Code AS BillingCode,
    bc.Description AS BillingCodeDescription,

    
    (
        SELECT GROUP_CONCAT(m.Modifier ORDER BY m.Modifier SEPARATOR ', ')
        FROM charge_modifiers cm
        JOIN modifiers m ON m.Id = cm.ModifierId
        WHERE cm.ChargeId = c.Id
    ) AS ChargeModifiers,

    
    (
        SELECT IFNULL(SUM(pay.Amount), 0)
        FROM payments pay
        WHERE pay.ChargeId = c.Id
    ) AS ChargePayments,

    
    (
        SELECT IFNULL(SUM(adj.Amount), 0)
        FROM adjustments adj
        WHERE adj.VisitId = v.Id
    ) AS VisitAdjustments,

    
    (
        c.Amount
        -
        (SELECT IFNULL(SUM(pay.Amount), 0)
         FROM payments pay
         WHERE pay.ChargeId = c.Id)
        -
        (SELECT IFNULL(SUM(adj.Amount), 0)
         FROM adjustments adj
         WHERE adj.VisitId = v.Id)
    ) AS LineItemBalance

FROM charges c
LEFT JOIN visits v ON v.Id = c.VisitId
LEFT JOIN patients p ON p.Id = c.PatientId
LEFT JOIN billing_codes bc ON bc.Id = c.BillingCodeId

ORDER BY p.LastName, p.FirstName, v.VisitDate, c.ChargeDate 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
