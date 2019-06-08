IF OBJECT_ID('[dbo].[usp_SelectUser]', 'P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_SelectUser] AS SELECT ''STUB''')
END
GO
ALTER PROCEDURE [dbo].[usp_SelectUser]
 @Id INT = NULL, 
 @Email	VARCHAR(50) = NULL,
 @Password VARCHAR(50) = NULL,
 @UserType TINYINT = NULL,
 @IsActive BIT = NULL

AS      
BEGIN

		SELECT *, FullName UserName FROM DS_User  WHERE Id = COALESCE(@Id,Id) AND Email = COALESCE(@Email,Email) AND
		Password = COALESCE(@Password,Password) AND UserType = COALESCE(@UserType,UserType) AND IsActive = COALESCE(@IsActive,IsActive) 		

END