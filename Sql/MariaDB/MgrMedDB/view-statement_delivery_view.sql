/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `statement_delivery_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `statement_delivery_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    v.Id AS VisitId,
    v.VisitDate AS VisitDate,
    v.Reason AS VisitReason,

    
    s.Id AS StatementId,
    s.StatementDate AS StatementDate,
    s.PeriodStart AS PeriodStart,
    s.PeriodEnd AS PeriodEnd,
    s.BeginningBalance AS BeginningBalance,
    s.NewCharges AS NewCharges,
    s.Payments AS StatementPayments,
    s.Adjustments AS Adjustments,
    s.EndingBalance AS EndingBalance,
    s.Notes AS StatementNotes,

    
    sdl.Id AS DeliveryId,
    sdl.DeliveryDate AS DeliveryDate,
    sdl.Method AS DeliveryMethod,
    sdl.Destination AS DeliveryDestination,
    sdl.Notes AS DeliveryNotes

FROM statement_delivery_log sdl
JOIN statements s
    ON s.Id = sdl.StatementId

JOIN patients p
    ON p.Id = s.PatientId

LEFT JOIN visits v
    ON v.Id = s.VisitId

ORDER BY
    p.Id,
    s.StatementDate,
    sdl.DeliveryDate 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
