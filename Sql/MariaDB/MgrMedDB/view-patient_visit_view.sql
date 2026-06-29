/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_visit_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_visit_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,
    p.Active AS PatientActive,

    
    v.Id AS VisitId,
    v.VisitDate,
    v.Reason AS VisitReason,
    v.Notes AS VisitNotes,
    v.Weight AS VisitWeight,
    v.BloodPressure AS VisitBloodPressure,
    v.Temperature AS VisitTemperature,
    v.Created AS VisitCreated,
    v.Updated AS VisitUpdated,

    
    d.Id AS DoctorId,
    d.FirstName AS DoctorFirstName,
    d.LastName AS DoctorLastName,
    d.MiddleName AS DoctorMiddleName,
    d.Credential AS DoctorCredential,
    d.Specialty AS DoctorSpecialty,
    d.Phone AS DoctorPhone,
    d.Fax AS DoctorFax,
    d.NPI AS DoctorNPI,
    d.Active AS DoctorActive,

    
    f.Id AS FacilityId,
    f.Name AS FacilityName,
    f.FacilityType AS FacilityType,
    f.Phone AS FacilityPhone,
    f.Fax AS FacilityFax,
    f.Active AS FacilityActive

FROM patients p
JOIN visits v
    ON v.PatientId = p.Id

LEFT JOIN doctors d
    ON d.Id = v.DoctorId

LEFT JOIN facilities f
    ON f.Id = v.FacilityId 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
