CREATE PROCEDURE [dbo].[LoginUser]
	@email VARCHAR(50),
	@passwrd VARCHAR(50)
AS
BEGIN
	SELECT Id, email, isAdmin, nickname FROM AppUser WHERE email = @email AND passwd = @passwrd
END