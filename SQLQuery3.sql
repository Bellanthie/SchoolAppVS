CREATE PROCEDURE GetStudentById
	@StudentId INT
AS
BEGIN
	SELECT s.FirstName,s.LastName, s.DateOfBirth, s.Gender, c.Name AS ClassName
	FROM Students s
	INNER JOIN Classes c ON s.ClassId = c.Id
	WHERE s.Id = @StudentId
END