/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_documents_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_documents_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    d.Id AS DocumentId,
    d.CategoryId AS DocumentCategoryId,
    dc.Name AS DocumentCategoryName,
    dc.Description AS DocumentCategoryDescription,
    dc.Active AS DocumentCategoryActive,

    d.FileName AS DocumentFileName,
    d.FileType AS DocumentFileType,
    d.FileSize AS DocumentFileSize,
    d.StoragePath AS DocumentStoragePath,
    d.UploadedBy AS DocumentUploadedBy,
    d.UploadedAt AS DocumentUploadedAt,
    d.Description AS DocumentDescription,

    
    dl.Id AS LinkId,
    dl.LinkedAt AS LinkedAt,
    dl.Notes AS LinkNotes,

    
    v.Id AS VisitId,
    v.VisitDate AS VisitDate,
    v.Reason AS VisitReason,

    
    istd.Id AS ImagingStudyId,
    istd.StudyDate AS ImagingStudyDate,
    istd.AccessionNumber AS ImagingAccessionNumber,
    istd.Modality AS ImagingModality,
    istd.Status AS ImagingStatus,
    istd.Notes AS ImagingNotes,

    
    lab.Id AS LabResultId,
    lab.ResultDate AS LabResultDate,
    lab.ComponentName AS LabComponentName,
    lab.Value AS LabResultValue,
    lab.Units AS LabUnits,
    lab.ReferenceRange AS LabReferenceRange,
    lab.Flag AS LabFlag,

    
    c.Id AS ClaimId,
    c.ClaimDate AS ClaimDate,
    c.Status AS ClaimStatus,
    c.TotalCharge AS ClaimTotalCharge,
    c.TotalPaid AS ClaimTotalPaid,
    c.TotalAdjusted AS ClaimTotalAdjusted,
    c.PatientResponsibility AS ClaimPatientResponsibility,
    c.Active AS ClaimActive

FROM documents d
JOIN document_links dl
    ON dl.DocumentId = d.Id

JOIN patients p
    ON p.Id = dl.PatientId

LEFT JOIN document_categories dc
    ON dc.Id = d.CategoryId

LEFT JOIN visits v
    ON v.Id = dl.VisitId

LEFT JOIN imaging_studies istd
    ON istd.Id = dl.ImagingStudyId

LEFT JOIN lab_results lab
    ON lab.Id = dl.LabResultId

LEFT JOIN claims c
    ON c.Id = dl.ClaimId

ORDER BY
    p.Id,
    d.UploadedAt,
    dl.LinkedAt 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
