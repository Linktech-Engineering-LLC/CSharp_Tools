/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_summary_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_summary_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName,
    p.LastName,
    p.MiddleName,
    p.DOB,
    p.Sex,
    p.Phone,
    p.Email,
    p.Active AS PatientActive,

    
    a.Maildrop,
    a.Street,
    a.Suite,
    a.ZipCode,
    a.City,
    a.State,
    a.Country,

    
    v.Systolic,
    v.Diastolic,
    v.Pulse,
    v.Oxygen,
    v.Temperature,
    v.Weight,
    v.RecordedAt AS LatestVitalsRecordedAt,

    
    pi.PlanId,
    pi.PolicyNumber,
    pi.GroupNumber,
    pi.Relationship,
    pi.EffectiveDate,
    pi.EndDate,
    pi.Copay,
    pi.Deductible,
    pi.IsPrimary,

    
    pc.ConditionTypeId,
    pc.OnsetDate,
    pc.ResolvedDate,
    pc.Status AS ConditionStatus,
    pc.Severity AS ConditionSeverity

FROM patients p


LEFT JOIN addresses a
    ON a.Id = p.AddressId


LEFT JOIN vitals v
    ON v.PatientId = p.Id
    AND v.RecordedAt = (
        SELECT MAX(v2.RecordedAt)
        FROM vitals v2
        WHERE v2.PatientId = p.Id
    )


LEFT JOIN patient_insurance pi
    ON pi.PatientId = p.Id
    AND pi.IsPrimary = 1
    AND (pi.EndDate IS NULL OR pi.EndDate > CURRENT_DATE)


LEFT JOIN patient_conditions pc
    ON pc.PatientId = p.Id
    AND pc.Status = 'Active' 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
