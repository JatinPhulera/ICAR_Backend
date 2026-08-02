using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICAR.Scanner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    InstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstitutionName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    InstitutionHead = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    InstitutionAdress = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.InstitutionID);
                });

            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ScriptLog",
                columns: table => new
                {
                    ScriptLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ScriptLo__F1F5130E00344A9D", x => x.ScriptLogId);
                });

            migrationBuilder.CreateTable(
                name: "SENSORTYPE",
                columns: table => new
                {
                    SENSORTYPEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SENSORTYPE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SENSORUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSORTYPE", x => x.SENSORTYPEID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Countries",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "SENSORS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Installation_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AssetID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Accession_Number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SensorUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Sensitivity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CommonName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    batteryPercentage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    messageType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    isHooterOn = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    isSensitivity = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    sensitivityValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsAssigned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SENSORTYPEID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSORS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SENSORS_SENSORTYPE",
                        column: x => x.SENSORTYPEID,
                        principalTable: "SENSORTYPE",
                        principalColumn: "SENSORTYPEID");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Addresses_States",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "Tree",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssetType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Alerts = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SENSORID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessionNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssetId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RfidTagCreatedOn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastAuditTime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssetSubType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SensorType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OperatorId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddedByName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Age = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AgeUnits = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BotanicalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpiryDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UniqueImportance = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AccessionOrigin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CommonName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ScientificName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CultiverName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DonorOrganization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Importance = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PlaceOfOrigin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PlantationYear = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OperatorFirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OperatorLastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OperatorPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OperatorState = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TREES_SENSORID",
                        column: x => x.SENSORID,
                        principalTable: "SENSORS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResetToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime", nullable: true),
                    MfaEnabled = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    MfaSecret = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstitutionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Users_Institutions",
                        column: x => x.InstitutionID,
                        principalTable: "Institutions",
                        principalColumn: "InstitutionID");
                    table.ForeignKey(
                        name: "FK_Users_RoleMaster",
                        column: x => x.RoleID,
                        principalTable: "RoleMaster",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "AuditTree",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AuditId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Girth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Height = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Disease = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Pest = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhysicalDamage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    V = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReviewedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReviewedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    AccessionNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Deletable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Editable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Acceptable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TreeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTree_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTree_Id",
                        column: x => x.TreeId,
                        principalTable: "Tree",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FileDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Filetype = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TreeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetail_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tree_Id",
                        column: x => x.TreeId,
                        principalTable: "Tree",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateId",
                table: "Addresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTree_TreeId",
                table: "AuditTree",
                column: "TreeId");

            migrationBuilder.CreateIndex(
                name: "UQ__Countrie__5D9B0D2C16F29CB1",
                table: "Countries",
                column: "CountryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Countrie__E056F20108B0A5BC",
                table: "Countries",
                column: "CountryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileDetail_TreeId",
                table: "FileDetail",
                column: "TreeId");

            migrationBuilder.CreateIndex(
                name: "IX_SENSORS_SENSORTYPEID",
                table: "SENSORS",
                column: "SENSORTYPEID");

            migrationBuilder.CreateIndex(
                name: "UQ__SENSORTY__B839916C8D1926B1",
                table: "SENSORTYPE",
                column: "SENSORTYPE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tree_SENSORID",
                table: "Tree",
                column: "SENSORID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InstitutionID",
                table: "Users",
                column: "InstitutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__536C85E45B58185A",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534DEFBBB53",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTree");

            migrationBuilder.DropTable(
                name: "FileDetail");

            migrationBuilder.DropTable(
                name: "ScriptLog");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tree");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "RoleMaster");

            migrationBuilder.DropTable(
                name: "SENSORS");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "SENSORTYPE");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
