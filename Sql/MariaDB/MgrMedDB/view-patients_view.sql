/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patients_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patients_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName,
    p.LastName,
    p.MiddleName,
    p.DOB,
    p.Sex,

    
    ec.FirstName AS EmergencyFirstName,
    ec.LastName AS EmergencyLastName,
    ec.Relationship AS EmergencyRelationship,
    ec.PhonePrimary AS EmergencyPhonePrimary,
    ec.PhoneSecondary AS EmergencyPhoneSecondary,
    ec.Email AS EmergencyEmail,

    
    pi.Id AS PrimaryInsuranceId,
    pi.PolicyNumber AS PrimaryPolicyNumber,
    ip.Name AS PrimaryInsurancePlanName,
    ic.Name AS PrimaryInsuranceCompanyName,

    
    si.Id AS SecondaryInsuranceId,
    si.PolicyNumber AS SecondaryPolicyNumber,
    isp.Name AS SecondaryInsurancePlanName,
    isc.Name AS SecondaryInsuranceCompanyName,

    
    pu.Id AS PortalUserId,
    pu.Username AS PortalUsername,
    pu.Role AS PortalRole,
    pu.Active AS PortalActive,
    pu.LastLogin AS PortalLastLogin,

    
    lv.Id AS LatestVisitId,
    lv.VisitDate AS LatestVisitDate,
    lv.Reason AS LatestVisitReason,

    
    vit.Id AS LatestVitalsId,
    vit.RecordedAt AS LatestVitalsRecordedAt,
    vit.Systolic AS LatestSystolic,
    vit.Diastolic AS LatestDiastolic,
    vit.Pulse AS LatestPulse,
    vit.Oxygen AS LatestOxygen,
    vit.Temperature AS LatestTemperature,
    vit.Weight AS LatestWeight,
    vit.Notes AS LatestVitalsNotes,

    
    (SELECT COUNT(*) FROM visits v WHERE v.PatientId = p.Id) AS TotalVisits,
    (SELECT COUNT(*) FROM patient_conditions cond WHERE cond.PatientId = p.Id) AS TotalConditions,
    (SELECT COUNT(*) FROM document_links dl WHERE dl.PatientId = p.Id) AS TotalDocuments,
    (SELECT COUNT(*) FROM claims cl WHERE cl.PatientId = p.Id) AS TotalClaims,

    
    (SELECT MAX(cl.ClaimDate) FROM claims cl WHERE cl.PatientId = p.Id) AS LatestClaimDate

FROM patients p

LEFT JOIN emergency_contacts ec
    ON ec.PatientId = p.Id AND ec.Priority = 1


LEFT JOIN patient_insurance pi
    ON pi.PatientId = p.Id AND pi.IsPrimary = 1
LEFT JOIN insurance_plans ip
    ON ip.Id = pi.PlanId
LEFT JOIN insurance_companies ic
    ON ic.Id = ip.CompanyId


LEFT JOIN patient_insurance si
    ON si.PatientId = p.Id AND si.IsPrimary = 0
LEFT JOIN insurance_plans isp
    ON isp.Id = si.PlanId
LEFT JOIN insurance_companies isc
    ON isc.Id = isp.CompanyId

LEFT JOIN portal_users pu
    ON pu.PatientId = p.Id

LEFT JOIN visits lv
    ON lv.Id = (
        SELECT v2.Id
        FROM visits v2
        WHERE v2.PatientId = p.Id
        ORDER BY v2.VisitDate DESC
        LIMIT 1
    )

LEFT JOIN vitals vit
    ON vit.Id = (
        SELECT vt.Id
        FROM vitals vt
        WHERE vt.PatientId = p.Id
        ORDER BY vt.RecordedAt DESC
        LIMIT 1
    )

ORDER BY
    p.LastName,
    p.FirstName 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
