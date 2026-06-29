/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `LabResultsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `LabResultsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_LabOrderId BIGINT UNSIGNED,
    IN p_ComponentName VARCHAR(100),
    IN p_Value VARCHAR(50),
    IN p_Units VARCHAR(20),
    IN p_ReferenceRange VARCHAR(50),
    IN p_Flag ENUM('Normal','High','Low','Critical'),
    IN p_ResultDate DATETIME
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_LabOrderId IS NULL OR p_LabOrderId = 0) THEN
        SET v_Error = 'LabOrderId is required';
    END IF;

    IF v_Error IS NULL AND (p_ComponentName IS NULL OR p_ComponentName = '') THEN
        SET v_Error = 'ComponentName is required';
    END IF;

    IF v_Error IS NULL AND (p_Value IS NULL OR p_Value = '') THEN
        SET v_Error = 'Value is required';
    END IF;

    IF v_Error IS NULL AND p_ResultDate IS NULL THEN
        SET v_Error = 'ResultDate is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE lab_results
        SET
            LabOrderId = p_LabOrderId,
            ComponentName = p_ComponentName,
            Value = p_Value,
            Units = p_Units,
            ReferenceRange = p_ReferenceRange,
            Flag = p_Flag,
            ResultDate = p_ResultDate
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
