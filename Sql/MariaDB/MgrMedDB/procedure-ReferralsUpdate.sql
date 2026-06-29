/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ReferralsUpdate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReferralsUpdate`(
    IN p_Id BIGINT UNSIGNED,
    IN p_PatientId BIGINT UNSIGNED,
    IN p_VisitId BIGINT UNSIGNED,
    IN p_ReferredBy BIGINT UNSIGNED,
    IN p_ReferralProviderId BIGINT UNSIGNED,
    IN p_ReferralDate DATETIME,
    IN p_Reason TEXT,
    IN p_Urgency ENUM('Routine','Urgent','Stat'),
    IN p_Status ENUM('Pending','Sent','Scheduled','Completed','Cancelled'),
    IN p_FollowUpDate DATE,
    IN p_Notes TEXT
)
BEGIN
    DECLARE v_Error TEXT DEFAULT NULL;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_Error = 'Invalid Id';
    END IF;

    IF v_Error IS NULL AND (p_PatientId IS NULL OR p_PatientId = 0) THEN
        SET v_Error = 'PatientId is required';
    END IF;

    IF v_Error IS NULL AND p_ReferralDate IS NULL THEN
        SET v_Error = 'ReferralDate is required';
    END IF;

    IF v_Error IS NULL AND (p_Reason IS NULL OR p_Reason = '') THEN
        SET v_Error = 'Reason is required';
    END IF;

    IF v_Error IS NULL THEN
        UPDATE referrals
        SET
            PatientId = p_PatientId,
            VisitId = p_VisitId,
            ReferredBy = p_ReferredBy,
            ReferralProviderId = p_ReferralProviderId,
            ReferralDate = p_ReferralDate,
            Reason = p_Reason,
            Urgency = p_Urgency,
            Status = p_Status,
            FollowUpDate = p_FollowUpDate,
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
