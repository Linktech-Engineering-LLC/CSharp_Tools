/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `staff_update`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `staff_update`(
    IN p_Id BIGINT UNSIGNED,
    IN p_FirstName VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_MiddleName VARCHAR(50),
    IN p_AddressId BIGINT UNSIGNED,
    IN p_Role VARCHAR(50),
    IN p_Specialty VARCHAR(100),
    IN p_Phone VARCHAR(50),
    IN p_Email VARCHAR(150),
    OUT p_Success TINYINT
)
BEGIN
    SET p_Success = 0;

    UPDATE staff
    SET
        FirstName = p_FirstName,
        LastName = p_LastName,
        MiddleName = p_MiddleName,
        AddressId = p_AddressId,
        Role = p_Role,
        Specialty = p_Specialty,
        Phone = p_Phone,
        Email = p_Email
    WHERE Id = p_Id;

    IF ROW_COUNT() > 0 THEN
        SET p_Success = 1;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
