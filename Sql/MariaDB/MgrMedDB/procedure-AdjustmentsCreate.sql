/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP PROCEDURE IF EXISTS `AdjustmentsCreate`;
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `AdjustmentsCreate`(
    IN p_PatientId      BIGINT UNSIGNED,
    IN p_ChargeId       BIGINT UNSIGNED,
    IN p_VisitId        BIGINT UNSIGNED,
    IN p_AdjustmentDate DATETIME,
    IN p_Type           ENUM('Contractual','WriteOff','Correction','Refund','Goodwill','Other'),
    IN p_Amount         DECIMAL(10,2),
    IN p_Notes          TEXT
)
BEGIN
    DECLARE v_exists INT DEFAULT 0;
    DECLARE v_charge_patient BIGINT UNSIGNED;
    DECLARE v_charge_visit   BIGINT UNSIGNED;

    DECLARE v_success TINYINT UNSIGNED DEFAULT 1;
    DECLARE v_message VARCHAR(255) DEFAULT '';
    DECLARE v_new_id BIGINT UNSIGNED DEFAULT NULL;

    
    IF p_PatientId IS NULL THEN
        SET v_success = 0;
        SET v_message = 'PatientId is required';
    END IF;

    IF v_success = 1 AND p_AdjustmentDate IS NULL THEN
        SET v_success = 0;
        SET v_message = 'AdjustmentDate is required';
    END IF;

    IF v_success = 1 AND p_Type IS NULL THEN
        SET v_success = 0;
        SET v_message = 'Type is required';
    END IF;

    IF v_success = 1 AND p_Amount IS NULL THEN
        SET v_success = 0;
        SET v_message = 'Amount is required';
    END IF;

    
    IF v_success = 1 THEN
        SELECT COUNT(*) INTO v_exists
        FROM patients
        WHERE Id = p_PatientId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid PatientId';
        END IF;
    END IF;

    
    IF v_success = 1 AND p_ChargeId IS NOT NULL THEN

        
        SELECT COUNT(*) INTO v_exists
        FROM charges
        WHERE Id = p_ChargeId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid ChargeId';
        END IF;

        
        IF v_success = 1 THEN
            SELECT PatientId, VisitId
            INTO v_charge_patient, v_charge_visit
            FROM charges
            WHERE Id = p_ChargeId;

            IF v_charge_patient <> p_PatientId THEN
                SET v_success = 0;
                SET v_message = 'Charge does not belong to the specified PatientId';
            END IF;
        END IF;

        
        IF v_success = 1 AND p_VisitId IS NOT NULL AND v_charge_visit IS NOT NULL THEN
            IF p_VisitId <> v_charge_visit THEN
                SET v_success = 0;
                SET v_message = 'VisitId does not match Charge.VisitId';
            END IF;
        END IF;
    END IF;

    
    IF v_success = 1 AND p_VisitId IS NOT NULL THEN
        SELECT COUNT(*) INTO v_exists
        FROM visits
        WHERE Id = p_VisitId
          AND PatientId = p_PatientId;

        IF v_exists = 0 THEN
            SET v_success = 0;
            SET v_message = 'Invalid VisitId for this PatientId';
        END IF;
    END IF;

    
    IF v_success = 1 THEN
        INSERT INTO adjustments (
            PatientId, ChargeId, VisitId, AdjustmentDate, Type, Amount, Notes
        ) VALUES (
            p_PatientId, p_ChargeId, p_VisitId, p_AdjustmentDate, p_Type, p_Amount, p_Notes
        );

        SET v_new_id = LAST_INSERT_ID();
        SET v_message = 'Adjustment created';
    END IF;

    
    SELECT v_success AS Success,
           v_message AS Message,
           v_new_id AS Id;

END//
DELIMITER ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
