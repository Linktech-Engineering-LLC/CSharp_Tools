/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DoctorsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DoctorsCreate`(
    IN  p_FirstName  VARCHAR(50),
    IN  p_LastName   VARCHAR(50),
    IN  p_MiddleName VARCHAR(50),
    IN  p_Credential VARCHAR(20),
    IN  p_Specialty  VARCHAR(100),
    IN  p_AddressId  BIGINT UNSIGNED,
    IN  p_Phone      VARCHAR(20),
    IN  p_Fax        VARCHAR(20),
    IN  p_NPI        VARCHAR(20),
    OUT p_Id         BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_FirstName IS NULL OR p_FirstName = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_LastName IS NULL OR p_LastName = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Credential IS NULL OR p_Credential = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO doctors (
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
        )
        VALUES (
            p_FirstName,
            p_LastName,
            p_MiddleName,
            p_Credential,
            p_Specialty,
            p_AddressId,
            p_Phone,
            p_Fax,
            p_NPI,
            TRUE
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
