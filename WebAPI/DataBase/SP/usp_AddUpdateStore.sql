IF OBJECT_ID('[dbo].[usp_AddUpdateStore]', 'P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_AddUpdateStore] AS SELECT ''STUB''')
END
GO
ALTER PROCEDURE [dbo].[usp_AddUpdateStore]
@Id	BIGINT ,
@StoreName	VARCHAR(250),
@Descripton	VARCHAR(550),
@Email      VARCHAR(50),
@PhoneNo    VARCHAR(50),
@Address	VARCHAR(250),
@Comment	VARCHAR(250),
@IsActive   BIT
AS      
BEGIN
MERGE [DS_Store] AS target
USING (SELECT @Id) AS source (Id)
ON target.Id = source.Id
WHEN MATCHED THEN
  UPDATE 
  SET 
		StoreName = @StoreName ,
		Descripton= @Descripton ,
		Email= @Email ,
		PhoneNo= @PhoneNo,
		[Address] = @Address,
		Comment = @Comment,
		IsActive = @IsActive 

WHEN NOT MATCHED THEN
  INSERT (
			StoreName,
			Descripton,
			Email,
			PhoneNo,
			[Address],
			Comment,
			IsActive
		)
  VALUES (
			@StoreName,
			@Descripton,
			@Email,
			@PhoneNo,
			@Address,
			@Comment,
			@IsActive
		)
 OUTPUT inserted.Id;

END

       
--endregion  

