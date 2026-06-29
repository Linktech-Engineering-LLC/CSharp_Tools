/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `billing_detailed_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `billing_detailed_view` AS SELECT
    
    c.Id AS ChargeId,
    c.ChargeDate,
    c.Amount AS ChargeAmount,
    c.Notes AS ChargeNotes,
    c.Active AS ChargeActive,

    
    v.Id AS VisitId,
    v.VisitDate,
    v.Reason AS VisitReason,

    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    s.Id AS DoctorId,
    s.FirstName AS DoctorFirstName,
    s.LastName AS DoctorLastName,
    s.Specialty AS DoctorSpecialty,

    
    f.Id AS FacilityId,
    f.Name AS FacilityName,
    f.FacilityType AS FacilityType,

    
    bc.Id AS BillingCodeId,
    bc.Code AS BillingCode,
    bc.Description AS BillingCodeDescription,

    
    (
        SELECT GROUP_CONCAT(m.Modifier ORDER BY m.Modifier SEPARATOR ', ')
        FROM charge_modifiers cm
        JOIN modifiers m ON m.Id = cm.ModifierId
        WHERE cm.ChargeId = c.Id
    ) AS Modifiers,

    
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
    ) AS ChargeBalance

FROM charges c

LEFT JOIN visits v
    ON v.Id = c.VisitId

LEFT JOIN patients p
    ON p.Id = c.PatientId

LEFT JOIN staff s
    ON s.Id = v.DoctorId

LEFT JOIN facilities f
    ON f.Id = v.FacilityId

LEFT JOIN billing_codes bc
    ON bc.Id = c.BillingCodeId

ORDER BY
    v.VisitDate DESC,
    c.ChargeDate DESC,
    c.Id 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
