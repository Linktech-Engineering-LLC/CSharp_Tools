/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DoctorFacilitiesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DoctorFacilitiesCreate`(
    IN  p_DoctorId   BIGINT UNSIGNED,
    IN  p_FacilityId BIGINT UNSIGNED,
    IN  p_IsPrimary  TINYINT(1),
    IN  p_Outreach   TINYINT(1),
    IN  p_Notes      VARCHAR(255),
    OUT p_Success    BOOLEAN,
    OUT p_Message    VARCHAR(255)
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_exists BIGINT UNSIGNED DEFAULT 0;

    SET p_Success = FALSE;
    SET p_Message = '';

    
    IF p_DoctorId IS NULL OR p_DoctorId = 0 THEN
        SET v_is_valid = FALSE;
        SET p_Message = 'Invalid DoctorId';
    END IF;

    IF p_FacilityId IS NULL OR p_FacilityId = 0 THEN
        SET v_is_valid = FALSE;
        SET p_Message = 'Invalid FacilityId';
    END IF;

    
    IF v_is_valid THEN
        SELECT COUNT(*) INTO v_exists
        FROM doctor_facilities
        WHERE DoctorId = p_DoctorId
          AND FacilityId = p_FacilityId;

        IF v_exists > 0 THEN
            SET v_is_valid = FALSE;
            SET p_Message = 'Relationship already exists';
        END IF;
    END IF;

    
    IF v_is_valid THEN
        INSERT INTO doctor_facilities (
            DoctorId,
            FacilityId,
            IsPrimary,
            Outreach,
            Notes
        )
        VALUES (
            p_DoctorId,
            p_FacilityId,
            p_IsPrimary,
            p_Outreach,
            p_Notes
        );

        SET p_Success = TRUE;
        SET p_Message = 'Relationship created';
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
