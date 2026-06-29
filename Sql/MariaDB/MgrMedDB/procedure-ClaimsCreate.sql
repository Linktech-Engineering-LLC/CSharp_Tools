/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ClaimsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ClaimsCreate`(
    IN  p_VisitId               BIGINT UNSIGNED,
    IN  p_PatientId             BIGINT UNSIGNED,
    IN  p_PrimaryInsuranceId    BIGINT UNSIGNED,
    IN  p_SecondaryInsuranceId  BIGINT UNSIGNED,
    IN  p_ClaimDate             DATE,
    IN  p_Status                ENUM('Open','Submitted','Rejected','Paid','Denied','Closed'),
    IN  p_TotalCharge           DECIMAL(10,2),
    IN  p_TotalPaid             DECIMAL(10,2),
    IN  p_TotalAdjusted         DECIMAL(10,2),
    IN  p_PatientResponsibility DECIMAL(10,2),
    IN  p_Notes                 TEXT,
    OUT p_Id                    BIGINT
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_Id = 0;

    IF p_VisitId IS NULL OR p_VisitId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_PatientId IS NULL OR p_PatientId = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_ClaimDate IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF p_Status IS NULL THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        INSERT INTO claims (
            VisitId,
            PatientId,
            PrimaryInsuranceId,
            SecondaryInsuranceId,
            ClaimDate,
            Status,
            TotalCharge,
            TotalPaid,
            TotalAdjusted,
            PatientResponsibility,
            Notes,
            Active
        )
        VALUES (
            p_VisitId,
            p_PatientId,
            p_PrimaryInsuranceId,
            p_SecondaryInsuranceId,
            p_ClaimDate,
            p_Status,
            p_TotalCharge,
            p_TotalPaid,
            p_TotalAdjusted,
            p_PatientResponsibility,
            p_Notes,
            TRUE
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
