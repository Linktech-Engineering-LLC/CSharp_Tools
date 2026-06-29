/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AddressesUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddressesUpdate`(
    IN p_Id       BIGINT UNSIGNED,
    IN p_Maildrop VARCHAR(50),
    IN p_Street   VARCHAR(50),
    IN p_Suite    VARCHAR(50),
    IN p_ZipCode  INT(5) UNSIGNED ZEROFILL,
    IN p_City     VARCHAR(50),
    IN p_State    CHAR(2),
    IN p_Country  VARCHAR(10)
)
BEGIN
    DECLARE v_exists INT;

    
    SELECT COUNT(*) INTO v_exists
    FROM addresses
    WHERE Id = p_Id;

    IF v_exists = 0 THEN
        SELECT 0 AS Success, 'Address not found' AS Message;
    ELSE

        
        IF p_ZipCode IS NOT NULL THEN
            SELECT COUNT(*) INTO v_exists
            FROM zip_codes
            WHERE ZipCode = p_ZipCode;

            IF v_exists = 0 THEN
                SELECT 0 AS Success, 'Invalid ZipCode' AS Message;
            ELSE
                UPDATE addresses
                SET
                    Maildrop = COALESCE(p_Maildrop, Maildrop),
                    Street   = COALESCE(p_Street, Street),
                    Suite    = COALESCE(p_Suite, Suite),
                    ZipCode  = COALESCE(p_ZipCode, ZipCode),
                    City     = COALESCE(p_City, City),
                    State    = COALESCE(p_State, State),
                    Country  = COALESCE(p_Country, Country)
                WHERE Id = p_Id;

                SELECT 1 AS Success, 'Address updated' AS Message;
            END IF;

        ELSE
            
            UPDATE addresses
            SET
                Maildrop = COALESCE(p_Maildrop, Maildrop),
                Street   = COALESCE(p_Street, Street),
                Suite    = COALESCE(p_Suite, Suite),
                ZipCode  = COALESCE(p_ZipCode, ZipCode),
                City     = COALESCE(p_City, City),
                State    = COALESCE(p_State, State),
                Country  = COALESCE(p_Country, Country)
            WHERE Id = p_Id;

            SELECT 1 AS Success, 'Address updated' AS Message;
        END IF;

    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
