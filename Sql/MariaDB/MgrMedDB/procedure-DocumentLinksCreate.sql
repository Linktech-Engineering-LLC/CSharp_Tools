/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `DocumentLinksCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `DocumentLinksCreate`(
    IN  p_DocumentId     BIGINT UNSIGNED,
    IN  p_PatientId      BIGINT UNSIGNED,
    IN  p_VisitId        BIGINT UNSIGNED,
    IN  p_ImagingStudyId BIGINT UNSIGNED,
    IN  p_LabResultId    BIGINT UNSIGNED,
    IN  p_ClaimId        BIGINT UNSIGNED,
    IN  p_Notes          TEXT,
    OUT p_Id             BIGINT,
    OUT p_Success        BOOLEAN,
    OUT p_Message        VARCHAR(255)
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;
    SET p_Success = FALSE;
    SET p_Message = '';

    
    IF p_DocumentId IS NULL OR p_DocumentId = 0 THEN
        SET v_is_valid = FALSE;
        SET p_Message = 'DocumentId is required';
    END IF;

    
    IF v_is_valid THEN
        IF (p_PatientId IS NULL OR p_PatientId = 0)
           AND (p_VisitId IS NULL OR p_VisitId = 0)
           AND (p_ImagingStudyId IS NULL OR p_ImagingStudyId = 0)
           AND (p_LabResultId IS NULL OR p_LabResultId = 0)
           AND (p_ClaimId IS NULL OR p_ClaimId = 0) THEN
            SET v_is_valid = FALSE;
            SET p_Message = 'At least one target Id must be provided';
        END IF;
    END IF;

    IF v_is_valid THEN
        INSERT INTO document_links (
            DocumentId,
            PatientId,
            VisitId,
            ImagingStudyId,
            LabResultId,
            ClaimId,
            Notes
        )
        VALUES (
            p_DocumentId,
            p_PatientId,
            p_VisitId,
            p_ImagingStudyId,
            p_LabResultId,
            p_ClaimId,
            p_Notes
        );

        SET p_Id = LAST_INSERT_ID();
        SET p_Success = TRUE;
        SET p_Message = 'Document link created';
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
