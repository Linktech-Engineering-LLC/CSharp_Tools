/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `portal_user_activity_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `portal_user_activity_view` AS SELECT
    pu.Id AS PortalUserId,
    pu.Username AS PortalUsername,
    pu.Role AS PortalRole,
    pu.Active AS PortalActive,

    pm.Id AS MessageId,
    pm.Subject AS MessageSubject,
    pm.ReadFlag AS MessageReadFlag,
    pm.Created AS MessageCreated,

    CASE
        WHEN pm.SenderId = pu.Id THEN 'Sent'
        WHEN pm.ReceiverId = pu.Id THEN 'Received'
        ELSE 'Unknown'
    END AS MessageDirection

FROM portal_users pu
LEFT JOIN portal_messages pm
    ON pm.SenderId = pu.Id
    OR pm.ReceiverId = pu.Id

ORDER BY
    pu.Id,
    pm.Created DESC 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
