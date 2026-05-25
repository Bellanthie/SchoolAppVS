-- skapa departments-tabellen och lägg till en kolumn i Staff-tabellen för att referera till department
CREATE TABLE Departments (
	Id   INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

--lägg till några avdelningar
INSERT INTO Departments (Name) VALUES
('Matematik och Naturvetenskap'),
('Språk och Humaniora'),
('Idrott och Hälsa'),
('Administration och Ledning');


--lägg till kolumner i Staff
ALTER TABLE Staff ADD DepartmentId INT REFERENCES Departments(Id);
ALTER TABLE Staff ADD HireDate DATE NOT NULL DEFAULT GETDATE();
ALTER TABLE Staff ADD Salary DECIMAL(10,2) NOT NULL DEFAULT 0;

--uppdatering av befintlig personal med avdelning, anställningsdatum och lön
-- Lars Eriksson - Rektor
UPDATE Staff SET DepartmentId=4, HireDate='2010-08-15', Salary=45000 WHERE Id=1;
-- Anna Lindgren - Lärare
UPDATE Staff SET DepartmentId=2, HireDate='2015-01-10', Salary=32000 WHERE Id=2;
UPDATE Staff SET DepartmentId=1, HireDate='2018-03-22', Salary=32000 WHERE Id=3;
UPDATE Staff SET DepartmentId=1, HireDate='2012-08-01', Salary=34000 WHERE Id=4;
UPDATE Staff SET DepartmentId=2, HireDate='2016-06-15', Salary=31000 WHERE Id=5;
UPDATE Staff SET DepartmentId=3, HireDate='2019-08-20', Salary=30000 WHERE Id=6;
UPDATE Staff SET DepartmentId=4, HireDate='2011-02-01', Salary=28000 WHERE Id=7;
UPDATE Staff SET DepartmentId=4, HireDate='2017-09-01', Salary=33000 WHERE Id=8;
UPDATE Staff SET DepartmentId=4, HireDate='2020-01-15', Salary=25000 WHERE Id=9;
UPDATE Staff SET DepartmentId=3, HireDate='2013-05-10', Salary=27000 WHERE Id=10;