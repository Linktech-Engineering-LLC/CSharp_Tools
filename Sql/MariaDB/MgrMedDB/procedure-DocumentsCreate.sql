/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DocumentsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DocumentsCreate`(
    IN  p_CategoryId  BIGINT UNSIGNED,
    IN  p_FileName    VARCHAR(255),
    IN  p_FileType    VARCHAR(50),
    IN  p_FileSize    BIGINT UNSIGNED,
    IN  p_StoragePath VARCHAR(500),
    IN  p_UploadedBy  BIGINT UNSIGNED,
    IN  p_Description TEXT,
    OUT p_Id          BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_FileName IS NULL OR p_FileName = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_FileType IS NULL OR p_FileType = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_StoragePath IS NULL OR p_StoragePath = '' THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO documents (
            CategoryId,
            FileName,
            FileType,
            FileSize,
            StoragePath,
            UploadedBy,
            Description
        )
        VALUES (
            p_CategoryId,
            p_FileName,
            p_FileType,
            p_FileSize,
            p_StoragePath,
            p_UploadedBy,
            p_Description
        );

        SET p_Id = LAST_INSERT_ID();
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
