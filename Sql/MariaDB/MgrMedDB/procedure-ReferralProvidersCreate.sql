/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ReferralProvidersCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReferralProvidersCreate`(
    IN p_Name VARCHAR(255),
    IN p_Specialty VARCHAR(255),
    IN p_Organization VARCHAR(255),
    IN p_Phone VARCHAR(50),
    IN p_Fax VARCHAR(50),
    IN p_Email VARCHAR(255),
    IN p_AddressLine1 VARCHAR(255),
    IN p_AddressLine2 VARCHAR(255),
    IN p_City VARCHAR(100),
    IN p_State VARCHAR(50),
    IN p_PostalCode VARCHAR(20),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Name IS NULL OR p_Name = '' THEN
        SET v_Error = 'Name is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO referral_providers
        (Name, Specialty, Organization, Phone, Fax, Email,
         AddressLine1, AddressLine2, City, State, PostalCode, Notes)
        VALUES
        (p_Name, p_Specialty, p_Organization, p_Phone, p_Fax, p_Email,
         p_AddressLine1, p_AddressLine2, p_City, p_State, p_PostalCode, p_Notes);

        SELECT LAST_INSERT_ID() AS NewId;
    ELSE
        SELECT NULL AS NewId, v_Error AS Error;
    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
