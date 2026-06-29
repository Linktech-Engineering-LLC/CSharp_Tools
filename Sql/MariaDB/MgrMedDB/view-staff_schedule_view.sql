/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `staff_schedule_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `staff_schedule_view` AS SELECT
    s.Id AS StaffId,
    s.FirstName,
    s.LastName,
    s.Role,
    s.Specialty,

    a.Id AS NextAppointmentId,
    a.AppointmentDate AS NextAppointmentDate,
    a.PatientId AS NextAppointmentPatientId,
    a.Status AS NextAppointmentStatus

FROM staff s
LEFT JOIN appointments a
    ON a.Id = (
        SELECT a2.Id
        FROM appointments a2
        WHERE a2.DoctorId = s.Id
        AND a2.AppointmentDate > NOW()
        ORDER BY a2.AppointmentDate ASC
        LIMIT 1
    )

ORDER BY
    s.LastName,
    s.FirstName 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
