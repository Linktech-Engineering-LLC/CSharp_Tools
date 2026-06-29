/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `register_standard_fhir_endpoints`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `register_standard_fhir_endpoints`(
    IN p_provider_id CHAR(36)
)
BEGIN
    INSERT INTO api_endpoints (endpoint_id, provider_id, resource_type, relative_path, description)
    VALUES
        (UUID(), p_provider_id, 'Patient', '/Patient/{id}', 'Get patient by ID'),
        (UUID(), p_provider_id, 'MedicationRequest', '/MedicationRequest?patient={patient_id}', 'Get prescriptions'),
        (UUID(), p_provider_id, 'Medication', '/Medication/{id}', 'Get medication details'),
        (UUID(), p_provider_id, 'MedicationStatement', '/MedicationStatement?patient={patient_id}', 'Get patient-reported meds'),
        (UUID(), p_provider_id, 'MedicationDispense', '/MedicationDispense?prescription={id}', 'Get pharmacy dispenses'),
        (UUID(), p_provider_id, 'Observation', '/Observation?patient={patient_id}&category=laboratory', 'Get lab results'),
        (UUID(), p_provider_id, 'DiagnosticReport', '/DiagnosticReport?patient={patient_id}', 'Get diagnostic reports'),
        (UUID(), p_provider_id, 'Encounter', '/Encounter?patient={patient_id}', 'Get encounters'),
        (UUID(), p_provider_id, 'DocumentReference', '/DocumentReference?patient={patient_id}', 'Get clinical documents'),
        (UUID(), p_provider_id, 'AllergyIntolerance', '/AllergyIntolerance?patient={patient_id}', 'Get allergies'),
        (UUID(), p_provider_id, 'AdverseEvent', '/AdverseEvent?subject={patient_id}', 'Get adverse reactions');
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
