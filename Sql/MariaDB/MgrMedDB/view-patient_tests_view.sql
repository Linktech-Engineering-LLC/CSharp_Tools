/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_tests_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_tests_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    pt.Id AS PatientTestId,
    pt.OrderedDate,
    pt.PerformedDate,
    pt.ResultSummary,
    pt.ResultDetails,
    pt.Notes AS TestNotes,
    pt.Created AS TestCreated,
    pt.Updated AS TestUpdated,

    
    tt.Id AS TestTypeId,
    tt.Name AS TestTypeName,
    tt.Category AS TestCategory,
    tt.Description AS TestDescription,

    
    pr.Id AS ProviderId,
    pr.FirstName AS ProviderFirstName,
    pr.LastName AS ProviderLastName,
    pr.MiddleName AS ProviderMiddleName,
    pr.Credential AS ProviderCredential,
    pr.Specialty AS ProviderSpecialty,
    pr.Phone AS ProviderPhone,
    pr.Fax AS ProviderFax,
    pr.NPI AS ProviderNPI,
    pr.Active AS ProviderActive,

    
    f.Id AS FacilityId,
    f.Name AS FacilityName,
    f.FacilityType AS FacilityType,
    f.Phone AS FacilityPhone,
    f.Fax AS FacilityFax,
    f.Active AS FacilityActive

FROM patients p
JOIN patient_tests pt
    ON pt.PatientId = p.Id

LEFT JOIN test_types tt
    ON tt.Id = pt.TestTypeId

LEFT JOIN providers pr
    ON pr.Id = pt.OrderingProviderId

LEFT JOIN facilities f
    ON f.Id = pt.PerformingFacilityId 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
