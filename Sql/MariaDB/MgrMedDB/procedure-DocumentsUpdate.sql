/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DocumentsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DocumentsUpdate`(
    IN  p_Id          BIGINT UNSIGNED,
    IN  p_CategoryId  BIGINT UNSIGNED,
    IN  p_FileName    VARCHAR(255),
    IN  p_FileType    VARCHAR(50),
    IN  p_FileSize    BIGINT UNSIGNED,
    IN  p_StoragePath VARCHAR(500),
    IN  p_Description TEXT,
    OUT p_Success     BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;
    DECLARE v_exists BIGINT UNSIGNED;

    SET p_Success = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

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
        SELECT Id INTO v_exists
        FROM documents
        WHERE Id = p_Id;

        IF v_exists IS NULL THEN
            SET v_is_valid = FALSE;
        END IF;
    END IF;

    IF v_is_valid THEN
        UPDATE documents
        SET
            CategoryId  = p_CategoryId,
            FileName    = p_FileName,
            FileType    = p_FileType,
            FileSize    = p_FileSize,
            StoragePath = p_StoragePath,
            Description = p_Description
        WHERE Id = p_Id;

        SET p_Success = TRUE;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
