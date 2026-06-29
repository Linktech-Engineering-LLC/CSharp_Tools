/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `ClaimsGetById`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `ClaimsGetById`(
    IN  p_Id BIGINT UNSIGNED,
    OUT p_VisitId BIGINT UNSIGNED,
    OUT p_PatientId BIGINT UNSIGNED,
    OUT p_PrimaryInsuranceId BIGINT UNSIGNED,
    OUT p_SecondaryInsuranceId BIGINT UNSIGNED,
    OUT p_ClaimDate DATE,
    OUT p_Status ENUM('Open','Submitted','Rejected','Paid','Denied','Closed'),
    OUT p_TotalCharge DECIMAL(10,2),
    OUT p_TotalPaid DECIMAL(10,2),
    OUT p_TotalAdjusted DECIMAL(10,2),
    OUT p_PatientResponsibility DECIMAL(10,2),
    OUT p_Notes TEXT,
    OUT p_Active BOOLEAN,
    OUT p_Found BOOLEAN
)
BEGIN
    DECLARE v_is_valid BOOLEAN DEFAULT TRUE;

    SET p_VisitId = NULL;
    SET p_PatientId = NULL;
    SET p_PrimaryInsuranceId = NULL;
    SET p_SecondaryInsuranceId = NULL;
    SET p_ClaimDate = NULL;
    SET p_Status = NULL;
    SET p_TotalCharge = NULL;
    SET p_TotalPaid = NULL;
    SET p_TotalAdjusted = NULL;
    SET p_PatientResponsibility = NULL;
    SET p_Notes = NULL;
    SET p_Active = NULL;
    SET p_Found = FALSE;

    IF p_Id IS NULL OR p_Id = 0 THEN
        SET v_is_valid = FALSE;
    END IF;

    IF v_is_valid THEN
        SELECT
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
        INTO
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
            p_Active
        FROM claims
        WHERE Id = p_Id;

        IF p_VisitId IS NOT NULL THEN
            SET p_Found = TRUE;
        END IF;
    END IF;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
