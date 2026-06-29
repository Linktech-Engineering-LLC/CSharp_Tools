/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PatientsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PatientsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_FirstName VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_MiddleName VARCHAR(50),
    IN p_DOB DATE,
    IN p_Sex ENUM('M','F','O'),
    IN p_Phone VARCHAR(20),
    IN p_Email VARCHAR(100),
    IN p_AddressId BIGINT UNSIGNED,
    IN p_Active TINYINT(1)
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_FirstName IS NULL OR p_FirstName = '') THEN
        SET v_Error = 'FirstName is required';
    END IF;

    IF v_Error IS NULL AND (p_LastName IS NULL OR p_LastName = '') THEN
        SET v_Error = 'LastName is required';
    END IF;

    IF v_Error IS NULL AND p_DOB IS NULL THEN
        SET v_Error = 'DOB is required';
    END IF;

    IF v_Error IS NULL AND p_Sex IS NULL THEN
        SET v_Error = 'Sex is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE patients
        SET
            FirstName = p_FirstName,
            LastName = p_LastName,
            MiddleName = p_MiddleName,
            DOB = p_DOB,
            Sex = p_Sex,
            Phone = p_Phone,
            Email = p_Email,
            AddressId = p_AddressId,
            Active = p_Active
        WHERE Id = p_Id;

        SELECT ROW_COUNT() AS RowsAffected;
    ELSE
        SELECT 0 AS RowsAffected, v_Error AS Error;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
