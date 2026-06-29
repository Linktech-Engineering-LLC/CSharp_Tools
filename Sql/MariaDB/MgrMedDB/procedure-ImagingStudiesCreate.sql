/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ImagingStudiesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ImagingStudiesCreate`(
    IN p_OrderId BIGINT UNSIGNED,
    IN p_StudyDate DATETIME,
    IN p_AccessionNumber VARCHAR(100),
    IN p_Modality ENUM('XR','CT','MRI','US','PET','NM','MAMMO','DEXA'),
    IN p_Status ENUM('InProgress','Completed','Finalized'),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_OrderId IS NULL OR p_OrderId = 0 THEN
        SET v_Error = 'OrderId is required';
    END IF;

    IF v_Error IS NULL AND p_StudyDate IS NULL THEN
        SET v_Error = 'StudyDate is required';
    END IF;

    IF v_Error IS NULL AND p_Modality IS NULL THEN
        SET v_Error = 'Modality is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO imaging_studies
        (OrderId, StudyDate, AccessionNumber, Modality, Status, Notes)
        VALUES
        (p_OrderId, p_StudyDate, p_AccessionNumber, p_Modality, p_Status, p_Notes);

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
