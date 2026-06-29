/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `staff_insert`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `staff_insert`(
    IN p_FirstName VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_MiddleName VARCHAR(50),
    IN p_AddressId BIGINT UNSIGNED,
    IN p_Role VARCHAR(50),
    IN p_Specialty VARCHAR(100),
    IN p_Phone VARCHAR(50),
    IN p_Email VARCHAR(150),
    OUT p_NewId BIGINT UNSIGNED
)
BEGIN
    SET p_NewId = NULL;

    INSERT INTO staff (
        FirstName, LastName, MiddleName,
        AddressId, Role, Specialty,
        Phone, Email, Active
    )
    VALUES (
        p_FirstName, p_LastName, p_MiddleName,
        p_AddressId, p_Role, p_Specialty,
        p_Phone, p_Email, 1
    );

    SET p_NewId = LAST_INSERT_ID();
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
