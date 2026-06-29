/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `PrescriptionDiscountCardsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `PrescriptionDiscountCardsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_Name VARCHAR(100),
    IN p_PBM VARCHAR(100),
    IN p_NetworkId VARCHAR(50),
    IN p_Bin VARCHAR(10),
    IN p_Pcn VARCHAR(20),
    IN p_GroupNumber VARCHAR(20),
    IN p_Notes TEXT,
    IN p_Active TINYINT(1)
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_Name IS NULL OR p_Name = '') THEN
        SET v_Error = 'Name is required';
    END IF;

    IF v_Error IS NULL AND (p_PBM IS NULL OR p_PBM = '') THEN
        SET v_Error = 'PBM is required';
    END IF;

    IF v_Error IS NULL AND (p_Bin IS NULL OR p_Bin = '') THEN
        SET v_Error = 'Bin is required';
    END IF;

    IF v_Error IS NULL AND (p_Pcn IS NULL OR p_Pcn = '') THEN
        SET v_Error = 'Pcn is required';
    END IF;

    IF v_Error IS NULL AND p_Active IS NULL THEN
        SET v_Error = 'Active flag is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE prescription_discount_cards
        SET
            Name = p_Name,
            PBM = p_PBM,
            NetworkId = p_NetworkId,
            Bin = p_Bin,
            Pcn = p_Pcn,
            GroupNumber = p_GroupNumber,
            Notes = p_Notes,
            Active = p_Active
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
