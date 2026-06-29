/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ReferralDocumentsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReferralDocumentsCreate`(
    IN p_ReferralId BIGINT UNSIGNED,
    IN p_DocumentId BIGINT UNSIGNED,
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_ReferralId IS NULL OR p_ReferralId = 0 THEN
        SET v_Error = 'ReferralId is required';
    END IF;

    IF v_Error IS NULL AND (p_DocumentId IS NULL OR p_DocumentId = 0) THEN
        SET v_Error = 'DocumentId is required';
    END IF;

    IF v_Error IS NULL THEN
        INSERT INTO referral_documents
        (ReferralId, DocumentId, Notes)
        VALUES
        (p_ReferralId, p_DocumentId, p_Notes);

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
