IF OBJECT_ID('[dbo].[usp_SelectStore]', 'P') IS NULL
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[usp_SelectStore] AS SELECT ''STUB''')
END
GO
ALTER PROCEDURE [dbo].[usp_SelectStore]

 @Id	BIGINT = NULL

AS      
BEGIN

		SELECT * FROM DS_Store  WHERE Id = COALESCE(@Id,Id) Order BY Id	

END