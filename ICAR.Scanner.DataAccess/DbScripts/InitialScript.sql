-- Create DATABASE ICAR

-- Use ICAR

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ScriptLog')
BEGIN
    CREATE TABLE ScriptLog (
        ScriptLogId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
        Remarks NVARCHAR(255) NULL
    );
END
GO

-- SELECT * FROM ScriptLog

BEGIN
    DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'a04eeee4-42ba-42aa-bbee-1080d530ccd6';
    IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
    BEGIN
        CREATE TABLE Countries
        (
            CountryId      INT IDENTITY(1,1) NOT NULL,
            CountryName    NVARCHAR(100) NOT NULL UNIQUE,
            CountryCode    NVARCHAR(10) NOT NULL UNIQUE,
            IsActive       BIT DEFAULT 1,
            CONSTRAINT PK_Countries PRIMARY KEY (CountryId)
        );

        INSERT INTO ScriptLog (ScriptLogId) VALUES (@ScriptLogId);
    END
END
GO

BEGIN
    DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'b15ffff5-53cb-53bb-ccef-2091e641dde7';
    IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
    BEGIN
        CREATE TABLE States
        (
            StateId        INT IDENTITY(1,1) NOT NULL,
            StateName      NVARCHAR(100) NOT NULL,
            StateCode      NVARCHAR(10) NOT NULL,
            CountryId      INT NOT NULL,
            IsActive       BIT DEFAULT 1,

            CONSTRAINT PK_States PRIMARY KEY (StateId),
            CONSTRAINT FK_States_Countries FOREIGN KEY (CountryId) REFERENCES Countries(CountryId)
        );

        INSERT INTO ScriptLog (ScriptLogId) VALUES (@ScriptLogId);
    END
END
GO



BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = '73e0409c-7b5b-4ea9-b116-4ca3e8b77a06';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
    INSERT INTO Countries (CountryName, CountryCode) VALUES
    ('India', 'IN'),
    ('United States', 'US'),
    ('United Kingdom', 'GB'),
    ('Australia', 'AU'),
    ('Canada', 'CA');

END
END
GO


BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'd2398726-c284-4204-a9e6-abdf91f3b508';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
 
   -- Get CountryId for India
    DECLARE @IndiaId INT;
    SELECT @IndiaId = CountryId FROM Countries WHERE CountryCode = 'IN';

    -- Insert all Indian states (excluding union territories)
    INSERT INTO States (StateName, StateCode, CountryId) VALUES
    ('Andhra Pradesh', 'AP', @IndiaId),
    ('Arunachal Pradesh', 'AR', @IndiaId),
    ('Assam', 'AS', @IndiaId),
    ('Bihar', 'BR', @IndiaId),
    ('Chhattisgarh', 'CG', @IndiaId),
    ('Goa', 'GA', @IndiaId),
    ('Gujarat', 'GJ', @IndiaId),
    ('Haryana', 'HR', @IndiaId),
    ('Himachal Pradesh', 'HP', @IndiaId),
    ('Jharkhand', 'JH', @IndiaId),
    ('Karnataka', 'KA', @IndiaId),
    ('Kerala', 'KL', @IndiaId),
    ('Madhya Pradesh', 'MP', @IndiaId),
    ('Maharashtra', 'MH', @IndiaId),
    ('Manipur', 'MN', @IndiaId),
    ('Meghalaya', 'ML', @IndiaId),
    ('Mizoram', 'MZ', @IndiaId),
    ('Nagaland', 'NL', @IndiaId),
    ('Odisha', 'OR', @IndiaId),
    ('Punjab', 'PB', @IndiaId),
    ('Rajasthan', 'RJ', @IndiaId),
    ('Sikkim', 'SK', @IndiaId),
    ('Tamil Nadu', 'TN', @IndiaId),
    ('Telangana', 'TG', @IndiaId),
    ('Tripura', 'TR', @IndiaId),
    ('Uttar Pradesh', 'UP', @IndiaId),
    ('Uttarakhand', 'UK', @IndiaId),
    ('West Bengal', 'WB', @IndiaId);


END
END
GO

BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'b3333333-3333-3333-3333-333333333333';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
    CREATE TABLE Addresses
    (
        AddressId       UNIQUEIDENTIFIER NOT NULL,
        AddressLine1    NVARCHAR(255) NOT NULL,
        AddressLine2    NVARCHAR(255),
        City            NVARCHAR(100),
        StateId         INT NOT NULL,
        PostalCode      NVARCHAR(20),
        CountryId       INT NOT NULL,
        IsActive        BIT DEFAULT 1,
        CONSTRAINT PK_Addresses PRIMARY KEY (AddressId),
        CONSTRAINT FK_Addresses_States FOREIGN KEY (StateId) REFERENCES States(StateId),
        CONSTRAINT FK_Addresses_Countries FOREIGN KEY (CountryId) REFERENCES Countries(CountryId)
    );
    INSERT INTO ScriptLog (ScriptLogId) VALUES (@ScriptLogId);
END
END
GO

BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'b4444444-4444-4444-4444-444444444444';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
    CREATE TABLE Users
    (
        UserId              UNIQUEIDENTIFIER NOT NULL,
        Username            NVARCHAR(50) NOT NULL UNIQUE,
        Email               NVARCHAR(255) NOT NULL UNIQUE,
        PasswordHash        NVARCHAR(255) NOT NULL,
        FirstName           NVARCHAR(50),
        LastName            NVARCHAR(50),
        DateOfBirth         DATE,
        PhoneNumber         NVARCHAR(20),
        IsEmailVerified     BIT DEFAULT 0,
        IsActive            BIT DEFAULT 1,
        IsLocked            BIT DEFAULT 0,
        LastLoginAt         DATETIME,
        CreatedOn           DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedOn           DATETIME NULL,
        CreatedBy           NVARCHAR(50),
        UpdatedBy           NVARCHAR(50),
        ResetToken          NVARCHAR(255),
        ResetTokenExpiry    DATETIME,
        MfaEnabled          BIT DEFAULT 0,
        MfaSecret           NVARCHAR(255),
        ProfilePictureUrl   NVARCHAR(255),
        AddressId           UNIQUEIDENTIFIER NULL, -- FK to Addresses
        CONSTRAINT PK_Users PRIMARY KEY (UserId),
        CONSTRAINT FK_Users_Addresses FOREIGN KEY (AddressId) REFERENCES Addresses(AddressId)
    );
    INSERT INTO ScriptLog (ScriptLogId) VALUES (@ScriptLogId);
END
END
GO

--TODO:: JP Need to remove later
BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = '2074c9af-e057-4b59-abf2-257cf1148115';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
        -- ===========================================
    -- Insert Dummy Address and User #1
    -- ===========================================
    DECLARE @AddressId1 UNIQUEIDENTIFIER = NEWID();
    DECLARE @UserId1    UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Addresses (AddressId, AddressLine1, AddressLine2, City, StateId, PostalCode, CountryId, IsActive) VALUES (
        @AddressId1, '123 Main St', 'Apt 101', 'Springfield', 1, '12345', 1, 1);

    INSERT INTO Users (UserId, Username, Email, PasswordHash, FirstName, LastName, DateOfBirth, PhoneNumber, IsEmailVerified, IsActive, IsLocked, LastLoginAt, CreatedOn, UpdatedOn, CreatedBy, UpdatedBy,
        ResetToken, ResetTokenExpiry, MfaEnabled, MfaSecret, ProfilePictureUrl, AddressId) 
        VALUES (@UserId1, 'JP', 'jp@jp.com', 'hashedpassword1', 'Jatin', 'P', '1990-01-01', '555-1234',
        1, 1, 0, GETDATE(), GETDATE(), NULL, 'system', NULL,
        NULL, NULL, 0, NULL, NULL, @AddressId1
    );

    -- ===========================================
    -- Insert Dummy Address and User #2
    -- ===========================================
    DECLARE @AddressId2 UNIQUEIDENTIFIER = NEWID();
    DECLARE @UserId2    UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Addresses (
        AddressId, AddressLine1, AddressLine2, City, StateId, PostalCode, CountryId, IsActive
    ) VALUES (
        @AddressId2, '456 Elm St', NULL, 'Shelbyville', 2, '67890', 1, 1
    );

    INSERT INTO Users (
        UserId, Username, Email, PasswordHash, FirstName, LastName, DateOfBirth, PhoneNumber,
        IsEmailVerified, IsActive, IsLocked, LastLoginAt, CreatedOn, UpdatedOn, CreatedBy, UpdatedBy,
        ResetToken, ResetTokenExpiry, MfaEnabled, MfaSecret, ProfilePictureUrl, AddressId
    ) VALUES (
        @UserId2, 'JP Test', 'jptest@gmail.com', 'hashedpassword2', 'JP', 'Test', '1995-05-15', '555-5678',
        0, 1, 0, GETDATE(), GETDATE(), NULL, 'system', NULL,
        NULL, NULL, 0, NULL, NULL, @AddressId2
    );

    INSERT INTO ScriptLog (ScriptLogId) VALUES (@ScriptLogId);
END
END
GO

----CHANGES BY RANJAN

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Institutions')
BEGIN
CREATE TABLE Institutions (
    InstitutionID INT IDENTITY(1,1) NOT NULL,
    InstitutionName VARCHAR(100) NOT NULL,
    InstitutionHead VARCHAR(100) NULL,
	InstitutionAdress VARCHAR(1000) NULL,
	Status BIT DEFAULT 1, 
    CreatedOn           DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedOn           DATETIME NULL,
	CreatedBy           NVARCHAR(50) NULL,
	UpdatedBy           NVARCHAR(50) NULL,
	CONSTRAINT PK_Institutions PRIMARY KEY (InstitutionID)
);
END
GO

BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'E0636C7C-4A02-41A2-8B27-C9EA30B08912';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
    INSERT INTO Institutions (InstitutionName, InstitutionHead) VALUES
    ('NBPGR', 'NBPGR'),
    ('CAFRI', 'CAFRI'),
    ('IIHR', 'IIHR')

END
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RoleMaster')
BEGIN
CREATE TABLE RoleMaster (
    RoleID  INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(100) NOT NULL,
	Status BIT DEFAULT 1,    
    CreatedOn           DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedOn           DATETIME NULL,
	CreatedBy           NVARCHAR(50) NULL,
	UpdatedBy           NVARCHAR(50) NULL,
	CONSTRAINT PK_RoleMaster PRIMARY KEY (RoleID)
);
END
GO

BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = 'AB03AF0A-5E38-4D6F-8EB3-3D8872428599';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
    INSERT INTO RoleMaster (Name) VALUES
    ('SUPER ADMIN'),
    ('ADMIN'),
    ('FIELD STAFF')

END
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
CREATE TABLE Users
(
    UserId              UNIQUEIDENTIFIER NOT NULL,
    Username            NVARCHAR(50) NOT NULL UNIQUE,
    Email               NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash        NVARCHAR(255) NOT NULL,
    FirstName           NVARCHAR(50),
    LastName            NVARCHAR(50),    
    Address             NVARCHAR(Max),
    DateOfBirth         DATE,
    PhoneNumber         NVARCHAR(20),
    IsEmailVerified     BIT DEFAULT 0,
    IsActive            BIT DEFAULT 1,
    IsLocked            BIT DEFAULT 0,
    LastLoginAt         DATETIME,
    CreatedOn           DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedOn           DATETIME NULL,
    CreatedBy           NVARCHAR(50),
    UpdatedBy           NVARCHAR(50),
    ResetToken          NVARCHAR(255),
    ResetTokenExpiry    DATETIME,
    MfaEnabled          BIT DEFAULT 0,
    MfaSecret           NVARCHAR(255),
    ProfilePictureUrl   NVARCHAR(255),
    AddressId           UNIQUEIDENTIFIER NULL, -- FK to Addresses
	RoleID				INT,
	InstitutionID		INT,
    CONSTRAINT PK_Users PRIMARY KEY (UserId),
    CONSTRAINT FK_Users_Addresses FOREIGN KEY (AddressId) REFERENCES Addresses(AddressId),
	CONSTRAINT FK_Users_RoleMaster FOREIGN KEY (RoleID) REFERENCES RoleMaster(RoleID),
	CONSTRAINT FK_Users_Institutions FOREIGN KEY (InstitutionID) REFERENCES Institutions(InstitutionID)
);
END
GO


BEGIN
DECLARE @ScriptLogId UNIQUEIDENTIFIER = '0C19BEB0-174D-4547-9E06-2F9E55381DCC';
IF NOT EXISTS (SELECT 1 FROM ScriptLog WHERE ScriptLogId = @ScriptLogId)
BEGIN
        -- ===========================================
    -- Insert Dummy Address and User #1
    -- ===========================================
    DECLARE @AddressId1 UNIQUEIDENTIFIER = NEWID();
    DECLARE @UserId1    UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Addresses (AddressId, AddressLine1, AddressLine2, City, StateId, PostalCode, CountryId, IsActive) VALUES (
        @AddressId1, '123 Main St', 'Apt 101', 'Springfield', 1, '12345', 1, 1);

    INSERT INTO Users (UserId, Username, Email, PasswordHash, FirstName, LastName,Address, DateOfBirth, PhoneNumber, IsEmailVerified, IsActive, IsLocked, LastLoginAt, CreatedOn, UpdatedOn, CreatedBy, UpdatedBy,
        ResetToken, ResetTokenExpiry, MfaEnabled, MfaSecret, ProfilePictureUrl, AddressId,RoleID,InstitutionID) 
        VALUES (@UserId1, 'JP', 'jp@jp.com', 'hashedpassword1', 'Jatin', 'P','Address-1', '1990-01-01', '555-1234',
        1, 1, 0, GETDATE(), GETDATE(), NULL, 'system', NULL,
        NULL, NULL, 0, NULL, NULL, @AddressId1,1,1
    );

    -- ===========================================
    -- Insert Dummy Address and User #2
    -- ===========================================
    DECLARE @AddressId2 UNIQUEIDENTIFIER = NEWID();
    DECLARE @UserId2    UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Addresses (
        AddressId, AddressLine1, AddressLine2, City, StateId, PostalCode, CountryId, IsActive
    ) VALUES (
        @AddressId2, '456 Elm St', NULL, 'Shelbyville', 2, '67890', 1, 1
    );

    INSERT INTO Users (
        UserId, Username, Email, PasswordHash, FirstName, LastName, Address, DateOfBirth, PhoneNumber,
        IsEmailVerified, IsActive, IsLocked, LastLoginAt, CreatedOn, UpdatedOn, CreatedBy, UpdatedBy,
        ResetToken, ResetTokenExpiry, MfaEnabled, MfaSecret, ProfilePictureUrl, AddressId,RoleID,InstitutionID
    ) VALUES (
        @UserId2, 'JP Test', 'jptest@gmail.com', 'hashedpassword2', 'JP', 'Test','Address-1', '1995-05-15', '555-5678',
        0, 1, 0, GETDATE(), GETDATE(), NULL, 'system', NULL,
        NULL, NULL, 0, NULL, NULL, @AddressId2,1,1
    );

    INSERT INTO ScriptLog (ScriptLogId) VALUES (@ScriptLogId);
END
END



---

-- SELECT * FROM Countries
-- SELECT * FROM States
-- SELECT * FROM Addresses
-- SELECT * FROM Users
-- SELECT * FROM ScriptLog