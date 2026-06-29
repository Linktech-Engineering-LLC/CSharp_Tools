/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_lab_results_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_lab_results_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    lo.Id AS LabOrderId,
    lo.OrderDate AS LabOrderDate,
    lo.Status AS LabOrderStatus,
    lo.Notes AS LabOrderNotes,
    lo.DoctorId AS OrderingDoctorId,
    lo.FacilityId AS OrderingFacilityId,

    
    lt.Id AS LabTestId,
    lt.Name AS LabTestName,
    lt.Description AS LabTestDescription,
    lt.LOINC AS LabTestLOINC,
    lt.Active AS LabTestActive,

    
    lr.Id AS LabResultId,
    lr.ComponentName,
    lr.Value,
    lr.Units,
    lr.ReferenceRange,
    lr.Flag AS ResultFlag,
    lr.ResultDate AS ResultDate

FROM patients p
JOIN lab_orders lo
    ON lo.PatientId = p.Id

JOIN lab_tests lt
    ON lt.Id = lo.LabTestId

JOIN lab_results lr
    ON lr.LabOrderId = lo.Id 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
