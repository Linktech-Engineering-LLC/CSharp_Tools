/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_primary_emergency_contact_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_primary_emergency_contact_view` AS SELECT 
    ec.Id AS Id,
    ec.PatientId AS PatientId,
    ec.FirstName AS FirstName,
    ec.LastName AS LastName,
    ec.Relationship AS Relationship,
    ec.Priority AS Priority,
    ec.PhonePrimary AS PhonePrimary,
    ec.PhoneSecondary AS PhoneSecondary,
    ec.Email AS Email,
    ec.AddressLine1 AS AddressLine1,
    ec.AddressLine2 AS AddressLine2,
    ec.City AS City,
    ec.State AS State,
    ec.PostalCode AS PostalCode,
    ec.Notes AS Notes
FROM emergency_contacts ec
JOIN (
    SELECT 
        emergency_contacts.PatientId AS PatientId,
        MIN(emergency_contacts.Priority) AS TopPriority
    FROM emergency_contacts
    GROUP BY emergency_contacts.PatientId
) x 
    ON x.PatientId = ec.PatientId 
   AND x.TopPriority = ec.Priority 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
