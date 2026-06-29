/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `visit_charges_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `visit_charges_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,

    
    v.Id AS VisitId,
    v.VisitDate,
    v.Reason AS VisitReason,

    
    c.Id AS ChargeId,
    c.ChargeDate,
    c.Amount AS ChargeAmount,
    c.Notes AS ChargeNotes,
    c.Active AS ChargeActive,

    
    bc.Id AS BillingCodeId,
    bc.Code AS BillingCode,
    bc.Description AS BillingDescription,
    bc.CodeType AS BillingCodeType,
    bc.DefaultAmount AS BillingDefaultAmount,
    bc.Active AS BillingCodeActive

FROM visits v
JOIN patients p
    ON p.Id = v.PatientId

LEFT JOIN charges c
    ON c.VisitId = v.Id
    AND c.Active = 1

LEFT JOIN billing_codes bc
    ON bc.Id = c.BillingCodeId

ORDER BY
    p.Id,
    v.VisitDate,
    c.ChargeDate 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
