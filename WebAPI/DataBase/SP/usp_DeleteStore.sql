IF OBJECT_ID('[dbo].[usp_DeleteStore]', 'P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_DeleteStore] AS SELECT ''STUB''')
END
GO
ALTER PROCEDURE [dbo].[usp_DeleteStore]

 @Id BIGINT

AS      
BEGIN

		DELETE DS_Store  WHERE Id = @Id 
	

END