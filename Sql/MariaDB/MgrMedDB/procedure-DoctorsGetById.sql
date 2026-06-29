/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DoctorsGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DoctorsGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_FirstName VARCHAR(50),
    OUT p_LastName VARCHAR(50),
    OUT p_MiddleName VARCHAR(50),
    OUT p_Credential VARCHAR(20),
    OUT p_Specialty VARCHAR(100),
    OUT p_AddressId BIGINT UNSIGNED,
    OUT p_Phone VARCHAR(20),
    OUT p_Fax VARCHAR(20),
    OUT p_NPI VARCHAR(20),
    OUT p_Active BOOLEAN,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_FirstName = NULL;
    SET p_LastName = NULL;
    SET p_MiddleName = NULL;
    SET p_Credential = NULL;
    SET p_Specialty = NULL;
    SET p_AddressId = NULL;
    SET p_Phone = NULL;
    SET p_Fax = NULL;
    SET p_NPI = NULL;
    SET p_Active = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
            FirstName,
            LastName,
            MiddleName,
            Credential,
            Specialty,
            AddressId,
            Phone,
            Fax,
            NPI,
            Active
        INTO
            p_FirstName,
            p_LastName,
            p_MiddleName,
            p_Credential,
            p_Specialty,
            p_AddressId,
            p_Phone,
            p_Fax,
            p_NPI,
            p_Active
        FROM doctors
        WHERE Id = p_Id;

        IF p_FirstName IS NOT NULL THEN
            SET p_Found = TRUE;
        END IF;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
