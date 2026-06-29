/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `EmergencyContactsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `EmergencyContactsCreate`(
    IN  p_PatientId      BIGINT UNSIGNED,
    IN  p_FirstName      VARCHAR(100),
    IN  p_LastName       VARCHAR(100),
    IN  p_Relationship   VARCHAR(100),
    IN  p_Priority       INT UNSIGNED,
    IN  p_PhonePrimary   VARCHAR(50),
    IN  p_PhoneSecondary VARCHAR(50),
    IN  p_Email          VARCHAR(255),
    IN  p_AddressLine1   VARCHAR(255),
    IN  p_AddressLine2   VARCHAR(255),
    IN  p_City           VARCHAR(100),
    IN  p_State          VARCHAR(50),
    IN  p_PostalCode     VARCHAR(20),
    IN  p_Notes          TEXT,
    OUT p_Id             BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_FirstName IS NULL OR p_FirstName = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_LastName IS NULL OR p_LastName = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Relationship IS NULL OR p_Relationship = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_PhonePrimary IS NULL OR p_PhonePrimary = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO emergency_contacts (
            PatientId,
            FirstName,
            LastName,
            Relationship,
            Priority,
            PhonePrimary,
            PhoneSecondary,
            Email,
            AddressLine1,
            AddressLine2,
            City,
            State,
            PostalCode,
            Notes
        )
        VALUES (
            p_PatientId,
            p_FirstName,
            p_LastName,
            p_Relationship,
            p_Priority,
            p_PhonePrimary,
            p_PhoneSecondary,
            p_Email,
            p_AddressLine1,
            p_AddressLine2,
            p_City,
            p_State,
            p_PostalCode,
            p_Notes
        );

        SET p_Id = LAST_INSERT_ID();
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
