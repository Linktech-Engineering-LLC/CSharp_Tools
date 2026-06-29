/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_imaging_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_imaging_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    io.Id AS ImagingOrderId,
    io.VisitId AS ImagingVisitId,
    io.OrderedBy AS ImagingOrderedBy,
    io.OrderDate AS ImagingOrderDate,
    io.Modality AS ImagingOrderModality,
    io.BodyPart AS ImagingOrderBodyPart,
    io.Reason AS ImagingOrderReason,
    io.Status AS ImagingOrderStatus,

    
    isd.Id AS ImagingStudyId,
    isd.StudyDate AS ImagingStudyDate,
    isd.AccessionNumber AS ImagingAccessionNumber,
    isd.Modality AS ImagingStudyModality,
    isd.Status AS ImagingStudyStatus,
    isd.Notes AS ImagingStudyNotes,

    
    ise.Id AS ImagingSeriesId,
    ise.SeriesNumber AS ImagingSeriesNumber,
    ise.Description AS ImagingSeriesDescription,

    
    ifl.Id AS ImagingFileId,
    ifl.FilePath AS ImagingFilePath,
    ifl.FileType AS ImagingFileType,
    ifl.InstanceNumber AS ImagingInstanceNumber,
    ifl.UploadedAt AS ImagingFileUploadedAt,

    
    ir.Id AS ImagingReportId,
    ir.ReportedBy AS ImagingReportedBy,
    ir.ReportDate AS ImagingReportDate,
    ir.Impression AS ImagingImpression,
    ir.Findings AS ImagingFindings,
    ir.Recommendations AS ImagingRecommendations

FROM patients p


JOIN imaging_orders io
    ON io.PatientId = p.Id


LEFT JOIN imaging_studies isd
    ON isd.OrderId = io.Id


LEFT JOIN imaging_series ise
    ON ise.StudyId = isd.Id


LEFT JOIN imaging_files ifl
    ON ifl.StudyId = isd.Id
    AND (ifl.SeriesId = ise.Id OR ifl.SeriesId IS NULL)


LEFT JOIN imaging_reports ir
    ON ir.StudyId = isd.Id 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
