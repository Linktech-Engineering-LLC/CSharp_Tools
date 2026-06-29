/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_revenue_summary_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_revenue_summary_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName,
    p.LastName,

    
    (SELECT IFNULL(SUM(c.Amount), 0)
     FROM charges c WHERE c.PatientId = p.Id) AS TotalCharges,

    
    (SELECT IFNULL(SUM(pay.Amount), 0)
     FROM payments pay
     JOIN charges c2 ON c2.Id = pay.ChargeId
     WHERE c2.PatientId = p.Id) AS TotalPayments,

    
    (SELECT IFNULL(SUM(adj.Amount), 0)
     FROM adjustments adj
     JOIN visits v2 ON v2.Id = adj.VisitId
     WHERE v2.PatientId = p.Id) AS TotalAdjustments,

    
    (
        (SELECT IFNULL(SUM(c.Amount), 0)
         FROM charges c WHERE c.PatientId = p.Id)
        -
        (SELECT IFNULL(SUM(adj.Amount), 0)
         FROM adjustments adj JOIN visits v2 ON v2.Id = adj.VisitId
         WHERE v2.PatientId = p.Id)
    ) AS NetRevenue

FROM patients p

ORDER BY NetRevenue DESC, p.LastName, p.FirstName 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
