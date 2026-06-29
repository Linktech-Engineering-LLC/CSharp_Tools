/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `aging_detail_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `aging_detail_view` AS SELECT
    
    v.Id AS VisitId,
    v.VisitDate,
    v.PatientId,

    
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,

    
    DATEDIFF(CURRENT_DATE(), v.VisitDate) AS DaysOutstanding,

    
    CASE
        WHEN DATEDIFF(CURRENT_DATE(), v.VisitDate) <= 30 THEN '0-30'
        WHEN DATEDIFF(CURRENT_DATE(), v.VisitDate) <= 60 THEN '31-60'
        WHEN DATEDIFF(CURRENT_DATE(), v.VisitDate) <= 90 THEN '61-90'
        ELSE '90+'
    END AS AgingBucket,

    
    (SELECT IFNULL(SUM(c.Amount), 0)
     FROM charges c
     WHERE c.VisitId = v.Id) AS TotalCharges,

    
    (SELECT IFNULL(SUM(pay.Amount), 0)
     FROM payments pay
     JOIN charges c2 ON c2.Id = pay.ChargeId
     WHERE c2.VisitId = v.Id) AS TotalPayments,

    
    (SELECT IFNULL(SUM(adj.Amount), 0)
     FROM adjustments adj
     WHERE adj.VisitId = v.Id) AS TotalAdjustments,

    
    (
        (SELECT IFNULL(SUM(c.Amount), 0)
         FROM charges c WHERE c.VisitId = v.Id)
        -
        (SELECT IFNULL(SUM(pay.Amount), 0)
         FROM payments pay JOIN charges c2 ON c2.Id = pay.ChargeId
         WHERE c2.VisitId = v.Id)
        -
        (SELECT IFNULL(SUM(adj.Amount), 0)
         FROM adjustments adj WHERE adj.VisitId = v.Id)
    ) AS VisitBalance

FROM visits v
LEFT JOIN patients p ON p.Id = v.PatientId

ORDER BY DaysOutstanding DESC, v.VisitDate 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
