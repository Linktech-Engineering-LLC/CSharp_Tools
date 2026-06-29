/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `insurance_full_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `insurance_full_view` AS SELECT
    
    p.Id AS PatientId,
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    p.DOB AS PatientDOB,
    p.Sex AS PatientSex,
    p.Active AS PatientActive,

    
    pi.Id AS PatientInsuranceId,
    pi.PolicyNumber,
    pi.GroupNumber,
    pi.Relationship,
    pi.EffectiveDate,
    pi.EndDate,
    pi.Copay,
    pi.Deductible,
    pi.IsPrimary,

    
    ip.Id AS PlanId,
    ip.Name AS PlanName,
    ip.PlanType,
    ip.Active AS PlanActive,

    
    ic.Id AS CompanyId,
    ic.Name AS CompanyName,
    ic.Phone AS CompanyPhone,
    ic.Fax AS CompanyFax,
    ic.Active AS CompanyActive,

    
    addr.Street AS CompanyStreet,
    addr.Suite AS CompanySuite,
    addr.City AS CompanyCity,
    addr.State AS CompanyState,
    addr.ZipCode AS CompanyZipCode,
    addr.Country AS CompanyCountry

FROM patient_insurance pi
LEFT JOIN patients p
    ON p.Id = pi.PatientId
LEFT JOIN insurance_plans ip
    ON ip.Id = pi.PlanId
LEFT JOIN insurance_companies ic
    ON ic.Id = ip.CompanyId
LEFT JOIN addresses addr
    ON addr.Id = ic.AddressId

ORDER BY
    p.LastName,
    p.FirstName,
    pi.IsPrimary DESC 
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
