/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ImagingFilesCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ImagingFilesCreate`(
    IN p_SeriesId BIGINT UNSIGNED,
    IN p_StudyId BIGINT UNSIGNED,
    IN p_FilePath VARCHAR(500),
    IN p_FileType ENUM('DICOM','JPEG','PNG','MP4'),
    IN p_InstanceNumber INT UNSIGNED
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_StudyId IS NULL OR p_StudyId = 0 THEN
        SET v_Error = 'StudyId is required';
    END IF;

    IF v_Error IS NULL AND (p_FilePath IS NULL OR p_FilePath = '') THEN
        SET v_Error = 'FilePath is required';
    END IF;

    IF v_Error IS NULL AND p_FileType IS NULL THEN
        SET v_Error = 'FileType is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO imaging_files
        (SeriesId, StudyId, FilePath, FileType, InstanceNumber)
        VALUES
        (p_SeriesId, p_StudyId, p_FilePath, p_FileType, p_InstanceNumber);

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
