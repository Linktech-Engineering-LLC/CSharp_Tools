/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_conditions_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_conditions_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName,
    p.LastName,
    p.MiddleName,
    p.DOB,
    p.Sex,
    p.Active AS PatientActive,

    
    pc.Id AS ConditionId,
    pc.ConditionTypeId,
    pc.OnsetDate,
    pc.ResolvedDate,
    pc.Status AS ConditionStatus,
    pc.Severity AS ConditionSeverity,
    pc.Notes AS ConditionNotes,

    
    ct.ICD10 AS ConditionICD10,
    ct.Name AS ConditionName,
    ct.Description AS ConditionDescription,
    ct.Chronic AS ConditionIsChronic,
    ct.Active AS ConditionTypeActive

FROM patients p
LEFT JOIN patient_conditions pc
    ON pc.PatientId = p.Id
LEFT JOIN condition_types ct
    ON ct.Id = pc.ConditionTypeId 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
