/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `portal_user_profile_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `portal_user_profile_view` AS SELECT
    pu.Id AS PortalUserId,
    pu.Username AS PortalUsername,
    pu.Role AS PortalRole,
    pu.Active AS PortalActive,
    pu.LastLogin AS PortalLastLogin,
    pu.Created AS PortalCreated,
    pu.Updated AS PortalUpdated,

    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.MiddleName AS PatientMiddleName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,

    
    s.Id AS StaffId,
    s.FirstName AS StaffFirstName,
    s.LastName AS StaffLastName,
    s.Role AS StaffRole,
    s.Active AS StaffActive

FROM portal_users pu
LEFT JOIN patients p
    ON p.Id = pu.PatientId

LEFT JOIN staff s
    ON s.Id = pu.StaffId

ORDER BY
    pu.Username 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
