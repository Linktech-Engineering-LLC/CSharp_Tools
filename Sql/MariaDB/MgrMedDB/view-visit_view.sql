/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `visit_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `visit_view` AS SELECT
    
    v.Id AS VisitId,
    v.VisitDate,
    v.Reason,
    v.Notes,
    v.Weight,
    v.BloodPressure,
    v.Temperature,
    v.Created,
    v.Updated,

    
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

    
    addr.Maildrop AS FacilityMaildrop,
    addr.Street AS FacilityStreet,
    addr.Suite AS FacilitySuite,
    addr.ZipCode AS FacilityZipCode,
    addr.City AS FacilityCity,
    addr.State AS FacilityState,
    addr.Country AS FacilityCountry

FROM visits v

LEFT JOIN patients p
    ON p.Id = v.PatientId

LEFT JOIN staff s
    ON s.Id = v.DoctorId

LEFT JOIN facilities f
    ON f.Id = v.FacilityId

LEFT JOIN addresses addr
    ON addr.Id = f.AddressId

ORDER BY
    v.VisitDate DESC 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
