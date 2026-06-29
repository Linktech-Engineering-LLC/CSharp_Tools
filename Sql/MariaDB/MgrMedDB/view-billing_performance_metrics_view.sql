/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `billing_performance_metrics_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `billing_performance_metrics_view` AS SELECT
    
    v.Id AS VisitId,
    v.VisitDate,
    v.PatientId,

    
    p.FirstName,
    p.LastName,

    
    (SELECT IFNULL(SUM(c.Amount), 0)
     FROM charges c WHERE c.VisitId = v.Id) AS TotalCharges,

    
    (SELECT IFNULL(SUM(pay.Amount), 0)
     FROM payments pay
     JOIN charges c2 ON c2.Id = pay.ChargeId
     WHERE c2.VisitId = v.Id) AS TotalPayments,

    
    (SELECT IFNULL(SUM(adj.Amount), 0)
     FROM adjustments adj WHERE adj.VisitId = v.Id) AS TotalAdjustments,

    
    CASE
        WHEN (SELECT IFNULL(SUM(c.Amount), 0)
              FROM charges c WHERE c.VisitId = v.Id) = 0
        THEN 0
        ELSE
            (
                (SELECT IFNULL(SUM(pay.Amount), 0)
                 FROM payments pay JOIN charges c2 ON c2.Id = pay.ChargeId
                 WHERE c2.VisitId = v.Id)
                /
                (SELECT IFNULL(SUM(c.Amount), 0)
                 FROM charges c WHERE c.VisitId = v.Id)
            )
    END AS CollectionRate,

    
    CASE
        WHEN (SELECT IFNULL(SUM(c.Amount), 0)
              FROM charges c WHERE c.VisitId = v.Id) = 0
        THEN 0
        ELSE
            (
                (SELECT IFNULL(SUM(adj.Amount), 0)
                 FROM adjustments adj WHERE adj.VisitId = v.Id)
                /
                (SELECT IFNULL(SUM(c.Amount), 0)
                 FROM charges c WHERE c.VisitId = v.Id)
            )
    END AS AdjustmentRate,

    
    DATEDIFF(CURRENT_DATE(), v.VisitDate) AS DaysOutstanding,

    
    CASE
        WHEN DATEDIFF(CURRENT_DATE(), v.VisitDate) <= 30 THEN '0-30'
        WHEN DATEDIFF(CURRENT_DATE(), v.VisitDate) <= 60 THEN '31-60'
        WHEN DATEDIFF(CURRENT_DATE(), v.VisitDate) <= 90 THEN '61-90'
        ELSE '90+'
    END AS AgingBucket

FROM visits v
LEFT JOIN patients p ON p.Id = v.PatientId

ORDER BY v.VisitDate DESC, v.Id 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
