/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `statement_by_visit_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `statement_by_visit_view` AS SELECT
    
    v.Id AS VisitId,
    v.VisitDate,
    v.Reason AS VisitReason,

    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,

    
    s.Id AS StatementId,
    s.StatementDate,
    s.PeriodStart,
    s.PeriodEnd,
    s.BeginningBalance,
    s.NewCharges,
    s.Payments AS StatementPayments,
    s.Adjustments,
    s.EndingBalance,
    s.Notes AS StatementNotes

FROM statements s
JOIN patients p
    ON p.Id = s.PatientId

LEFT JOIN visits v
    ON v.Id = s.VisitId

ORDER BY
    p.Id,
    v.VisitDate,
    s.StatementDate 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
