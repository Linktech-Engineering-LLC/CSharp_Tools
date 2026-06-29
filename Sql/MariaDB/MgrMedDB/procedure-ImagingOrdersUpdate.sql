/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ImagingOrdersUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ImagingOrdersUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_VisitId BIGINT UNSIGNED,
    IN p_OrderedBy BIGINT UNSIGNED,
    IN p_OrderDate DATETIME,
    IN p_Modality ENUM('XR','CT','MRI','US','PET','NM','MAMMO','DEXA'),
    IN p_BodyPart VARCHAR(255),
    IN p_Reason TEXT,
    IN p_Status ENUM('Ordered','Scheduled','Completed','Cancelled')
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PatientId IS NULL OR p_PatientId = 0) THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND p_OrderDate IS NULL THEN
        SET v_Error = 'OrderDate is required';
    END IF;

    IF v_Error IS NULL AND p_Modality IS NULL THEN
        SET v_Error = 'Modality is required';
    END IF;

    IF v_Error IS NULL AND (p_BodyPart IS NULL OR p_BodyPart = '') THEN
        SET v_Error = 'BodyPart is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE imaging_orders
        SET
            PatientId = p_PatientId,
            VisitId = p_VisitId,
            OrderedBy = p_OrderedBy,
            OrderDate = p_OrderDate,
            Modality = p_Modality,
            BodyPart = p_BodyPart,
            Reason = p_Reason,
            Status = p_Status
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
