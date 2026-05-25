SELECT s.FirstName, s.LastName, d.Name AS Department, 
       s.HireDate, s.Salary
FROM Staff s
INNER JOIN Departments d ON s.DepartmentId = d.Id