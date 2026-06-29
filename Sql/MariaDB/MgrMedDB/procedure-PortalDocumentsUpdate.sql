/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PortalDocumentsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PortalDocumentsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PortalUserId BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_CategoryId BIGINT UNSIGNED,
    IN p_FileName VARCHAR(255),
    IN p_FilePath VARCHAR(500),
    IN p_MimeType VARCHAR(100),
    IN p_FileSize BIGINT UNSIGNED,
    IN p_Active TINYINT(1),
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PortalUserId IS NULL OR p_PortalUserId = 0) THEN
        SET v_Error = 'PortalUserId is required';
    END IF;

    IF v_Error IS NULL AND (p_FileName IS NULL OR p_FileName = '') THEN
        SET v_Error = 'FileName is required';
    END IF;

    IF v_Error IS NULL AND (p_FilePath IS NULL OR p_FilePath = '') THEN
        SET v_Error = 'FilePath is required';
    END IF;

    IF v_Error IS NULL AND (p_MimeType IS NULL OR p_MimeType = '') THEN
        SET v_Error = 'MimeType is required';
    END IF;

    IF v_Error IS NULL AND (p_FileSize IS NULL OR p_FileSize = 0) THEN
        SET v_Error = 'FileSize is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE portal_documents
        SET
            PortalUserId = p_PortalUserId,
            PatientId = p_PatientId,
            CategoryId = p_CategoryId,
            FileName = p_FileName,
            FilePath = p_FilePath,
            MimeType = p_MimeType,
            FileSize = p_FileSize,
            Active = p_Active,
            Notes = p_Notes
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
