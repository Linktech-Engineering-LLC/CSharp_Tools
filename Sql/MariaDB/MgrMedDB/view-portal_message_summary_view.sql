/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `portal_message_summary_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `portal_message_summary_view` AS SELECT
    
    pm.Id AS MessageId,
    pm.Subject AS MessageSubject,
    pm.MessageBody AS MessageBody,
    pm.ReadFlag AS MessageReadFlag,
    pm.Created AS MessageCreated,

    
    s.Id AS SenderUserId,
    s.Username AS SenderUsername,
    s.PatientId AS SenderPatientId,
    s.StaffId AS SenderStaffId,
    s.Role AS SenderRole,
    s.LastLogin AS SenderLastLogin,
    s.Active AS SenderActive,

    
    r.Id AS ReceiverUserId,
    r.Username AS ReceiverUsername,
    r.PatientId AS ReceiverPatientId,
    r.StaffId AS ReceiverStaffId,
    r.Role AS ReceiverRole,
    r.LastLogin AS ReceiverLastLogin,
    r.Active AS ReceiverActive

FROM portal_messages pm
JOIN portal_users s
    ON s.Id = pm.SenderId

JOIN portal_users r
    ON r.Id = pm.ReceiverId

ORDER BY
    pm.Created DESC,
    pm.Id DESC 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
