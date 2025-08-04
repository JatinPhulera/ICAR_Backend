// namespace ICAR.Scanner.Models.DTOs;
// public class TreeRequest
// {
//     public string SensorType { get; set; }             // Can be null, so string? if nullable reference types enabled
//     public string SearchTerm { get; set; }
//     public string TreeState { get; set; }              // Can be null
// }

// public class TreeResponse
// {
//     public string Status { get; set; }
//     public int DataCount { get; set; }
//     public List<Tree> Data { get; set; }
// }
// public class TreeDetail
// {
//     public string CommonName { get; set; }
//     public string ScientificName { get; set; }

//     // Use string to support string or number interchangeably
//     public string AccessionNo { get; set; }

//     public string SensorId { get; set; }
//     public string Location { get; set; }
//     public DateTime LastUpdated { get; set; }        // Assume ISO date string mapped to DateTime

//     public string Status { get; set; }                // 'healthy', 'warning', 'critical'

//     public string CultiverName { get; set; }
//     public string DonorOrganization { get; set; }
//     public string Importance { get; set; }
//     public string PlaceOfOrigin { get; set; }

//     public string Age { get; set; }                   // string or number, keep string for flexibility
//     public string Latitude { get; set; }
//     public string Longitude { get; set; }
//     public string PlantationYear { get; set; }
//     public string SensorType { get; set; }

//     public string OperatorFirstName { get; set; }
//     public string OperatorLastName { get; set; }
//     public string OperatorPhone { get; set; }
//     public string OperatorState { get; set; }
//     public string ImageUrl { get; set; }

//     public List<FileDetail> Files { get; set; }
// }

// public class FileDetail
// {
//     public string Filename { get; set; }
//     public string Filetype { get; set; }
// }
// public class Tree
// {
//     public string Id { get; set; }
//     public string DisplayId { get; set; }
//     public string AssetType { get; set; }
//     public string Location { get; set; }
//     public string Alerts { get; set; }
//     public string AddedBy { get; set; }
//     public string SensorId { get; set; }
//     public string AccessionNumber { get; set; }
//     public string AssetId { get; set; }
//     public string RfidTagCreatedOn { get; set; }
//     public string LastAuditTime { get; set; }
//     public string AssetSubType { get; set; }
//     public string SensorType { get; set; }
//     public string OperatorId { get; set; }
//     public string AddedByName { get; set; }
//     public string OperatorName { get; set; }
//     public string Age { get; set; }
//     public string AgeUnits { get; set; }
//     public string Lat { get; set; }
//     public string Long { get; set; }
//     public string BotanicalName { get; set; }
//     public string ExpiryDate { get; set; }
//     public string InstallationDate { get; set; }
//     public string Origin { get; set; }
//     public string UniqueImportance { get; set; }
//     public double? Value { get; set; }
//     public string AccessionOrigin { get; set; }
//     public Dictionary<string, object> AdditionalParams { get; set; }
// }

// public class AddTreeRequest
// {
//     public AddTreeData Data { get; set; }
// }

// public class AddTreeData
// {
//     public string DisplayId { get; set; }
//     public string AssetType { get; set; }
//     public string Alerts { get; set; }
//     public string UniqueImportance { get; set; }
//     public DateTime? InstallationDate { get; set; }
//     public DateTime? ExpiryDate { get; set; }
//     public string AccessionNumber { get; set; }
//     public string BotanicalName { get; set; }
//     public string Donor { get; set; }
//     public string Location { get; set; }
//     public string Origin { get; set; }
//     public string AgeUnits { get; set; }
//     public string OperatorId { get; set; }
//     public string SensorId { get; set; }
//     public string SensorType { get; set; }
// }

// public class AddTreeResponse
// {
//     public string Status { get; set; }
//     public string Message { get; set; }
// }

// public class AuditTree
// {
//     public string Id { get; set; }                    // _id mapped to Id
//     public string Name { get; set; }
//     public string AssetId { get; set; }
//     public string DisplayId { get; set; }
//     public string AuditId { get; set; }
//     public long AuditDate { get; set; }               // Unix timestamp (milliseconds)
//     public double Girth { get; set; }
//     public double Height { get; set; }
//     public string Disease { get; set; }
//     public string Pest { get; set; }
//     public string PhysicalDamage { get; set; }
//     public string Remarks { get; set; }
//     public string AddedBy { get; set; }
//     public DateTime LastUpdate { get; set; }
//     public string State { get; set; }
//     public int Level { get; set; }
//     public int V { get; set; }                         // __v mapped to V
//     public string ReviewedBy { get; set; }
//     public long ReviewedOn { get; set; }
//     public string AccessionNumber { get; set; }
//     public bool Deletable { get; set; }
//     public bool Editable { get; set; }
//     public bool Acceptable { get; set; }
// }




// public class AuditRequest
// {
//     public string DisplayID { get; set; }
//     public string AssetID { get; set; }
//     public double Height { get; set; }
//     public double Girth { get; set; }
//     public string Disease { get; set; }
//     public string Pest { get; set; }
//     public string PhysicalDamage { get; set; }
//     public string Remarks { get; set; }
// }

// public class SensorTypeResponse
// {
//     public List<string> Data { get; set; }
//     public string Status { get; set; }
// }

// public class SensorListResponse
// {
//     public List<string> Data { get; set; }
//     public string Status { get; set; }
// }

// public class User
// {
//     public string UserID { get; set; }
//     public string Role { get; set; }
//     public string PhoneNumber { get; set; }
//     public string LastName { get; set; }
//     public string FirstName { get; set; }
//     public string Address { get; set; }
//     public string AdminID { get; set; }
//     public string State { get; set; }
//     public string LastAccessTime { get; set; }
//     public string Latitude { get; set; }
//     public string Longitude { get; set; }
// }

// public class UserResponse
// {
//     public string Status { get; set; }
//     public List<User> Data { get; set; }
// }

// public class AuditResponse
// {
//     public string Status { get; set; }
// }
