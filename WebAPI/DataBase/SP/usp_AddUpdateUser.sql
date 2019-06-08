IF OBJECT_ID('[dbo].[usp_AddUpdateUser]', 'P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_AddUpdateUser] AS SELECT ''STUB''')
END
GO
ALTER PROCEDURE [dbo].[usp_AddUpdateUser]
@Id	BIGINT = NULL ,
@FirstName	VARCHAR(50) = NULL,
@LastName	VARCHAR(50) = NULL,
@UserType  TINYINT = NULL,
@Email	VARCHAR(50) = NULL,
@Password	VARCHAR(50) = NULL,
@Phone	VARCHAR(20) = NULL,
@Address	VARCHAR(127) = NULL,
@City	NVARCHAR(50) = NULL,
@Country INT = NULL,
@State	VARCHAR(50) = NULL,
@IsActive	BIT = NULL,
@Tokan VARCHAR(MAX) = NULL
AS      
BEGIN
MERGE [DS_User] AS target
USING (SELECT @Id) AS source (Id)
ON target.Id = source.Id
WHEN MATCHED THEN
  UPDATE 
  SET 
		FirstName= @FirstName ,
		LastName= @LastName ,
		Email = @Email,
		[Password] = @Password ,
		Phone= @Phone ,
		[Address] = @Address,
		City = @City,
		[State] = @State,
		Country = @Country,
		IsActive = @IsActive,
		Tokan = @Tokan

WHEN NOT MATCHED THEN
  INSERT (
			FirstName,
			LastName,
			Email,
			[Password],
			Phone,
			[Address],			
			City,
			[State],
			Country,
			IsActive,
			Tokan		)
  VALUES (
			@FirstName,
			@LastName,
			@Email,
			@Password,
			@Phone,
			@Address,
			@City,
			@State,
			@Country,			
			@IsActive,
			@Tokan
		)
 OUTPUT inserted.Id;
 
 END

       
--endregion  

