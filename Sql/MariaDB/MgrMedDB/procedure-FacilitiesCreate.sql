/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `FacilitiesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `FacilitiesCreate`(
    IN  p_Name         VARCHAR(100),
    IN  p_FacilityType ENUM('Clinic','Hospital','LongTermCare'),
    IN  p_AddressId    BIGINT UNSIGNED,
    IN  p_Phone        VARCHAR(20),
    IN  p_Fax          VARCHAR(20),
    OUT p_Id           BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_Name IS NULL OR p_Name = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_FacilityType IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_AddressId IS NULL OR p_AddressId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO facilities (
            Name,
            FacilityType,
            AddressId,
            Phone,
            Fax,
            Active
        )
        VALUES (
            p_Name,
            p_FacilityType,
            p_AddressId,
            p_Phone,
            p_Fax,
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
