-- Add IsAssigned column to SENSORS table
-- Run this script against your ICAR database before deploying the API changes.

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.SENSORS')
      AND name = N'IsAssigned'
)
BEGIN
    ALTER TABLE dbo.SENSORS
    ADD IsAssigned BIT NOT NULL
        CONSTRAINT DF_SENSORS_IsAssigned DEFAULT 0;
END
GO

-- Backfill: sensors already linked to a tree should be marked as assigned
UPDATE s
SET IsAssigned = 1
FROM dbo.SENSORS s
INNER JOIN dbo.TREES t ON t.SENSORID = s.Id
WHERE t.SENSORID IS NOT NULL;
GO
