/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AddressesResolveId`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddressesResolveId`(
    IN  p_maildrop   VARCHAR(64),
    IN  p_street     VARCHAR(128),
    IN  p_suite      VARCHAR(64),
    IN  p_city       VARCHAR(64),
    IN  p_state      VARCHAR(32),
    IN  p_zipcode    VARCHAR(16),
    IN  p_country    VARCHAR(64),
    OUT p_id         INT
)
BEGIN
    DECLARE v_maildrop VARCHAR(64);
    DECLARE v_street   VARCHAR(128);
    DECLARE v_suite    VARCHAR(64);
    DECLARE v_city     VARCHAR(64);
    DECLARE v_state    VARCHAR(32);
    DECLARE v_zipcode  VARCHAR(16);
    DECLARE v_country  VARCHAR(64);
    DECLARE v_hash     CHAR(64);
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    
    SET p_id = 0;

    
    IF p_city IS NULL OR p_city = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_state IS NULL OR p_state = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_zipcode IS NULL OR p_zipcode = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_country IS NULL OR p_country = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    
    IF (p_maildrop IS NULL OR p_maildrop = '')
       AND (p_street IS NULL OR p_street = '') THEN
        SET v_is_valid = FALSE;
    END IF;

    
    IF v_is_valid THEN

        
        SET v_maildrop = NormalizeAddressLine(p_maildrop);
        SET v_street   = NormalizeAddressLine(p_street);
        SET v_suite    = NormalizeAddressLine(p_suite);
        SET v_city     = NormalizeCity(p_city);
        SET v_state    = NormalizeState(p_state);
        SET v_zipcode  = NormalizeZip(p_zipcode);
        SET v_country  = NormalizeCountry(p_country);

        
        SET v_hash = ComputeAddressHash(
            v_maildrop,
            v_street,
            v_suite,
            v_city,
            v_state,
            v_zipcode,
            v_country
        );

        
        SELECT ID INTO p_id
        FROM addresses
        WHERE AddressHash = v_hash
        LIMIT 1;

    END IF;
END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
