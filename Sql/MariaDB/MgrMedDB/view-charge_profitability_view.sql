/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `charge_profitability_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `charge_profitability_view` AS SELECT
    c.Id AS ChargeId,
    c.PatientId,
    c.VisitId,
    c.ChargeDate,
    c.Amount AS ChargeAmount,

    
    (
        SELECT IFNULL(SUM(pay.Amount), 0)
        FROM payments pay
        WHERE pay.ChargeId = c.Id
    ) AS ChargePayments,

    
    (
        SELECT IFNULL(SUM(adj.Amount), 0)
        FROM adjustments adj
        WHERE adj.VisitId = c.VisitId
    ) AS VisitAdjustments,

    
    (
        SELECT IFNULL(SUM(pay.Amount), 0)
        FROM payments pay
        WHERE pay.ChargeId = c.Id
    ) AS NetRevenue,

    
    CASE
        WHEN c.Amount = 0 THEN 0
        ELSE (
            (SELECT IFNULL(SUM(pay.Amount), 0)
             FROM payments pay WHERE pay.ChargeId = c.Id)
            / c.Amount
        )
    END AS ProfitabilityRatio

FROM charges c

ORDER BY c.ChargeDate DESC, c.Id 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
