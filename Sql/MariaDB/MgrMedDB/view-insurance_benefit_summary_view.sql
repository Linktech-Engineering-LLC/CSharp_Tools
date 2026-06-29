/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `insurance_benefit_summary_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `insurance_benefit_summary_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName,
    p.LastName,
    p.DOB,
    p.Sex,

    
    pi.Id AS PatientInsuranceId,
    pi.PolicyNumber,
    pi.GroupNumber,
    pi.Relationship,
    pi.EffectiveDate,
    pi.EndDate,
    pi.IsPrimary,

    
    ip.Id AS PlanId,
    ip.Name AS PlanName,
    ip.PlanType,

    
    ic.Id AS CompanyId,
    ic.Name AS CompanyName,
    ic.Phone AS CompanyPhone,
    ic.Fax AS CompanyFax,

    
    (
        SELECT IFNULL(SUM(c.Amount), 0)
        FROM charges c
        JOIN visits v ON v.Id = c.VisitId
        WHERE v.PatientId = p.Id
          AND v.VisitDate BETWEEN pi.EffectiveDate AND IFNULL(pi.EndDate, v.VisitDate)
    ) AS TotalCharges,

    
    (
        SELECT IFNULL(SUM(pay.Amount), 0)
        FROM payments pay
        JOIN charges c2 ON c2.Id = pay.ChargeId
        JOIN visits v2 ON v2.Id = c2.VisitId
        WHERE v2.PatientId = p.Id
          AND v2.VisitDate BETWEEN pi.EffectiveDate AND IFNULL(pi.EndDate, v2.VisitDate)
    ) AS TotalPayments,

    
    (
        SELECT IFNULL(SUM(adj.Amount), 0)
        FROM adjustments adj
        JOIN visits v3 ON v3.Id = adj.VisitId
        WHERE v3.PatientId = p.Id
          AND v3.VisitDate BETWEEN pi.EffectiveDate AND IFNULL(pi.EndDate, v3.VisitDate)
    ) AS TotalAdjustments

FROM patients p
LEFT JOIN patient_insurance pi
    ON pi.PatientId = p.Id AND pi.IsPrimary = 1
LEFT JOIN insurance_plans ip ON ip.Id = pi.PlanId
LEFT JOIN insurance_companies ic ON ic.Id = ip.CompanyId

ORDER BY p.LastName, p.FirstName 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
