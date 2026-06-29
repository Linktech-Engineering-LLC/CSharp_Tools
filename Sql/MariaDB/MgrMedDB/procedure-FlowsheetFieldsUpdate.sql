/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `FlowsheetFieldsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `FlowsheetFieldsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_FlowsheetId BIGINT UNSIGNED,
    IN p_Name VARCHAR(255),
    IN p_Unit VARCHAR(50),
    IN p_FieldType ENUM('Numeric','Text','Enum','Boolean'),
    IN p_EnumOptions TEXT,
    IN p_SortOrder INT UNSIGNED
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_FlowsheetId IS NULL OR p_FlowsheetId = 0) THEN
        SET v_Error = 'FlowsheetId is required';
    END IF;

    IF v_Error IS NULL AND (p_Name IS NULL OR p_Name = '') THEN
        SET v_Error = 'Name is required';
    END IF;

    IF v_Error IS NULL AND p_FieldType IS NULL THEN
        SET v_Error = 'FieldType is required';
    END IF;

    IF v_Error IS NULL AND p_FieldType = 'Enum' AND (p_EnumOptions IS NULL OR p_EnumOptions = '') THEN
        SET v_Error = 'EnumOptions required for Enum FieldType';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE flowsheet_fields
        SET
            FlowsheetId = p_FlowsheetId,
            Name = p_Name,
            Unit = p_Unit,
            FieldType = p_FieldType,
            EnumOptions = p_EnumOptions,
            SortOrder = p_SortOrder
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
