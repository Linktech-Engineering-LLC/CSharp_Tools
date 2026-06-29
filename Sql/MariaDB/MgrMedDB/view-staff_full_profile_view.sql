/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `staff_full_profile_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `staff_full_profile_view` AS SELECT
    
    s.Id AS StaffId,
    s.FirstName,
    s.LastName,
    s.MiddleName,
    s.Role,
    s.Specialty,
    s.Phone,
    s.Email,
    s.Active,
    s.Created,
    s.Updated,

    
    addr.Maildrop AS AddressMaildrop,
    addr.Street AS AddressStreet,
    addr.Suite AS AddressSuite,
    addr.ZipCode AS AddressZipCode,
    addr.City AS AddressCity,
    addr.State AS AddressState,
    addr.Country AS AddressCountry,

    
    pu.Id AS PortalUserId,
    pu.Username AS PortalUsername,
    pu.Role AS PortalUserRole,
    pu.Active AS PortalUserActive,
    pu.LastLogin AS PortalUserLastLogin,

    
    (SELECT COUNT(*) FROM visits v WHERE v.DoctorId = s.Id) AS TotalVisits,
    (SELECT COUNT(*) FROM appointments a WHERE a.DoctorId = s.Id) AS TotalAppointments,
    (SELECT COUNT(*) FROM documents d WHERE d.UploadedBy = s.Id) AS TotalDocuments,

    
    (SELECT v2.VisitDate
     FROM visits v2
     WHERE v2.DoctorId = s.Id
     ORDER BY v2.VisitDate DESC
     LIMIT 1) AS LatestVisitDate,

    
    (SELECT a2.AppointmentDate
     FROM appointments a2
     WHERE a2.DoctorId = s.Id
     ORDER BY a2.AppointmentDate DESC
     LIMIT 1) AS LatestAppointmentDate

FROM staff s

LEFT JOIN addresses addr
    ON addr.Id = s.AddressId

LEFT JOIN portal_users pu
    ON pu.StaffId = s.Id

ORDER BY
    s.LastName,
    s.FirstName 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
