/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `patient_contact_info_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `patient_contact_info_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName,
    p.LastName,
    p.MiddleName,
    p.Phone AS PatientPhone,
    p.Email AS PatientEmail,
    p.Active AS PatientActive,

    
    a.Id AS AddressId,
    a.Maildrop,
    a.Street,
    a.Suite,
    a.ZipCode,
    a.City,
    a.State,
    a.Country,

    
    ec.Id AS EmergencyContactId,
    ec.FirstName AS EmergencyFirstName,
    ec.LastName AS EmergencyLastName,
    ec.Relationship AS EmergencyRelationship,
    ec.Priority AS EmergencyPriority,
    ec.PhonePrimary AS EmergencyPhonePrimary,
    ec.PhoneSecondary AS EmergencyPhoneSecondary,
    ec.Email AS EmergencyEmail,
    ec.AddressLine1 AS EmergencyAddressLine1,
    ec.AddressLine2 AS EmergencyAddressLine2,
    ec.City AS EmergencyCity,
    ec.State AS EmergencyState,
    ec.PostalCode AS EmergencyPostalCode,
    ec.Notes AS EmergencyNotes

FROM patients p


LEFT JOIN addresses a
    ON a.Id = p.AddressId


LEFT JOIN emergency_contacts ec
    ON ec.PatientId = p.Id 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
