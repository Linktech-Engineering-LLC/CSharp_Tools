/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ImagingReportsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ImagingReportsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_StudyId BIGINT UNSIGNED,
    IN p_ReportedBy BIGINT UNSIGNED,
    IN p_ReportDate DATETIME,
    IN p_Impression TEXT,
    IN p_Findings TEXT,
    IN p_Recommendations TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_StudyId IS NULL OR p_StudyId = 0) THEN
        SET v_Error = 'StudyId is required';
    END IF;

    IF v_Error IS NULL AND p_ReportDate IS NULL THEN
        SET v_Error = 'ReportDate is required';
    END IF;

    IF v_Error IS NULL AND (p_Impression IS NULL OR p_Impression = '') THEN
        SET v_Error = 'Impression is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE imaging_reports
        SET
            StudyId = p_StudyId,
            ReportedBy = p_ReportedBy,
            ReportDate = p_ReportDate,
            Impression = p_Impression,
            Findings = p_Findings,
            Recommendations = p_Recommendations
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
