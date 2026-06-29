/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `documents_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `documents_view` AS SELECT
    
    d.Id AS DocumentId,
    d.CategoryId AS DocumentCategoryId,
    dc.Name AS DocumentCategoryName,
    dc.Description AS DocumentCategoryDescription,
    dc.Active AS DocumentCategoryActive,

    
    d.FileName AS DocumentFileName,
    d.FileType AS DocumentFileType,
    d.FileSize AS DocumentFileSize,
    d.StoragePath AS DocumentStoragePath,
    d.Description AS DocumentDescription,

    
    d.UploadedBy AS UploadedByUserId,
    pu.Username AS UploadedByUsername,
    pu.Role AS UploadedByRole,
    pu.Active AS UploadedByActive,
    d.UploadedAt AS DocumentUploadedAt

FROM documents d
LEFT JOIN document_categories dc
    ON dc.Id = d.CategoryId

LEFT JOIN portal_users pu
    ON pu.Id = d.UploadedBy

ORDER BY
    d.UploadedAt DESC,
    d.Id DESC 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
