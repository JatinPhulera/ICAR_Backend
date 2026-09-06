IF NOT EXISTS (SELECT 1 FROM [dbo].[RoleMaster] WHERE Name = 'Super Admin')
      INSERT INTO [dbo].[RoleMaster] (RoleID, Name, Status, CreatedOn, CreatedBy)
      VALUES (NEWID(), 'Super Admin', 1, GETUTCDATE(), 'system');                                                                 
                                                                                                                                  
  IF NOT EXISTS (SELECT 1 FROM [dbo].[RoleMaster] WHERE Name = 'Admin')                                                           
      INSERT INTO [dbo].[RoleMaster] (RoleID, Name, Status, CreatedOn, CreatedBy)                                                 
      VALUES (NEWID(), 'Admin', 1, GETUTCDATE(), 'system');

  IF NOT EXISTS (SELECT 1 FROM [dbo].[RoleMaster] WHERE Name = 'Operator')                                                        
      INSERT INTO [dbo].[RoleMaster] (RoleID, Name, Status, CreatedOn, CreatedBy)
      VALUES (NEWID(), 'Operator', 1, GETUTCDATE(), 'system');                                                                    
                                                                                                                                  
  IF NOT EXISTS (SELECT 1 FROM [dbo].[RoleMaster] WHERE Name = 'Viewer')
      INSERT INTO [dbo].[RoleMaster] (RoleID, Name, Status, CreatedOn, CreatedBy)                                                 
      VALUES (NEWID(), 'Viewer', 1, GETUTCDATE(), 'system');
                                                                                                                                  
  IF NOT EXISTS (SELECT 1 FROM [dbo].[RoleMaster] WHERE Name = 'Surveyor')
      INSERT INTO [dbo].[RoleMaster] (RoleID, Name, Status, CreatedOn, CreatedBy)                                                 
      VALUES (NEWID(), 'Surveyor', 1, GETUTCDATE(), 'system');