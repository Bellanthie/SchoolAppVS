--  HÖGSTADIET SCHOOL DATABASE
-- Purpose: School Database for Lab 2

CREATE DATABASE Hogstadiet;
GO

USE Hogstadiet;
GO


CREATE TABLE Titles (
    Id   INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);


CREATE TABLE Staff (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName  VARCHAR(50) NOT NULL,
    TitleId   INT NOT NULL REFERENCES Titles(Id)
);


CREATE TABLE Classes (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Name      VARCHAR(50) NOT NULL,
    TeacherId INT REFERENCES Staff(Id)   -- mentor teacher for this class
);


CREATE TABLE Students (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    FirstName         VARCHAR(50) NOT NULL,
    LastName          VARCHAR(50) NOT NULL,
    DateOfBirth       DATE        NOT NULL,
    Gender            VARCHAR(10) NOT NULL,
    SocialSecurityNo  VARCHAR(13) NOT NULL,   -- format: YYYYMMDD-XXXX
    ClassId           INT REFERENCES Classes(Id)
);


CREATE TABLE Subjects (
    Id   INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);


CREATE TABLE Grades (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT  NOT NULL REFERENCES Students(Id),
    StaffId   INT  NOT NULL REFERENCES Staff(Id),
    SubjectId INT  NOT NULL REFERENCES Subjects(Id),
    Grade     VARCHAR(1) NOT NULL,
    GradeDate DATE NOT NULL
);


INSERT INTO Titles (Name) VALUES
('Rektor'),
('Lärare'),
('Administration'),
('Kurator'),
('Receptionist'),
('Vaktmästare');


INSERT INTO Staff (FirstName, LastName, TitleId) VALUES
('Lars',   'Eriksson',   1),   -- Rektor
('Anna',   'Lindgren',   2),   -- Lärare
('Erik',   'Bergström',  2),   -- Lärare
('Maria',  'Svensson',   2),   -- Lärare
('Johan',  'Karlsson',   2),   -- Lärare
('Sara',   'Nilsson',    2),   -- Lärare
('Karin',  'Magnusson',  3),   -- Administration
('Emma',   'Olsson',     4),   -- Kurator
('Peter',  'Strand',     5),   -- Receptionist
('Björn',  'Holm',       6);   -- Vaktmästare


INSERT INTO Classes (Name, TeacherId) VALUES
('7A', 2),   -- Anna Lindgren är mentor
('7B', 3),   -- Erik Bergström är mentor
('8A', 4),   -- Maria Svensson är mentor
('8B', 5),   -- Johan Karlsson är mentor
('9A', 6),   -- Sara Nilsson är mentor
('9B', 2);   -- Anna Lindgren är mentor för 9B också


INSERT INTO Students (FirstName, LastName, DateOfBirth, Gender, SocialSecurityNo, ClassId) VALUES
('Maja',   'Andersson',  '2009-03-12', 'Kvinna', '200903121234', 1),
('Liam',   'Bergman',    '2008-07-24', 'Man',    '200807244321', 2),
('Ella',   'Lindqvist',  '2009-11-05', 'Kvinna', '200911051111', 3),
('Noah',   'Strand',     '2008-02-18', 'Man',    '200802182222', 4),
('Wilma',  'Holm',       '2010-06-30', 'Kvinna', '201006303333', 5),
('Oliver', 'Dahl',       '2009-09-14', 'Man',    '200909144444', 6),
('Astrid', 'Nyberg',     '2008-12-01', 'Kvinna', '200812015555', 1),
('Lucas',  'Björk',      '2010-04-22', 'Man',    '201004226666', 2),
('Klara',  'Sundström',  '2009-08-17', 'Kvinna', '200908177777', 3),
('Axel',   'Fors',       '2008-05-09', 'Man',    '200805098888', 4);


INSERT INTO Subjects (Name) VALUES
('Matematik'),
('Naturvetenskap'),
('Historia'),
('Engelska'),
('Musik'),
('Idrott och hälsa');


INSERT INTO Grades (StudentId, StaffId, SubjectId, Grade, GradeDate) VALUES
-- Äldre betyg
(1,  2, 1, 'A', '2025-10-10'),
(2,  3, 2, 'B', '2025-09-22'),
(3,  4, 3, 'C', '2025-08-01'),
(4,  5, 4, 'A', '2025-07-15'),
(5,  6, 5, 'B', '2025-06-30'),
-- Betyg satta den senaste månaden (inom 1 månad från 2026-02-27)
(6,  2, 1, 'C', '2026-02-10'),
(7,  3, 2, 'A', '2026-02-14'),
(8,  4, 3, 'B', '2026-01-30'),
(9,  5, 4, 'C', '2026-02-20'),
(10, 6, 6, 'A', '2026-02-25');


SELECT * FROM Titles;
SELECT * FROM Staff;
SELECT * FROM Classes;
SELECT * FROM Students;
SELECT * FROM Subjects;
SELECT * FROM Grades;

SELECT
    s.Id,
    s.FirstName,
    s.LastName,
    t.Name AS Title
FROM Staff s
INNER JOIN Titles t ON s.TitleId = t.Id
WHERE t.Name = 'Lärare';

SELECT
    Id,
    FirstName,
    LastName,
    DateOfBirth,
    Gender,
    SocialSecurityNo
FROM Students
ORDER BY LastName ASC;

SELECT
    st.Id,
    st.FirstName,
    st.LastName,
    st.DateOfBirth,
    st.Gender,
    c.Name AS ClassName
FROM Students st
INNER JOIN Classes c ON st.ClassId = c.Id
WHERE c.Name = '8A';

SELECT
    g.Id,
    st.FirstName + ' ' + st.LastName   AS StudentName,
    sub.Name                            AS Subject,
    g.Grade,
    g.GradeDate,
    sf.FirstName + ' ' + sf.LastName   AS GradedByTeacher
FROM Grades g
INNER JOIN Students st  ON g.StudentId = st.Id
INNER JOIN Subjects sub ON g.SubjectId = sub.Id
INNER JOIN Staff sf     ON g.StaffId   = sf.Id
WHERE g.GradeDate >= DATEADD(MONTH, -1, GETDATE());         

SELECT COUNT(*) AS StudentCount FROM dbo.Students;

CREATE PROCEDURE AddNewStudent
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DateOfBirth DATE,
    @Gender VARCHAR(10),
    @SocialSecurityNo VARCHAR(13),
    @ClassId INT
AS
BEGIN
    INSERT INTO Students (FirstName, LastName, DateOfBirth, Gender, SocialSecurityNo, ClassId)
    VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @SocialSecurityNo, @ClassId)
END

SELECT * FROM sys.procedures WHERE name = 'AddNewStudent';


INSERT INTO Grades (StudentId, StaffId, SubjectId, Grade, GradeDate) VALUES
(1, 2, 1, 'A', GETDATE()),
(2, 3, 2, 'B', DATEADD(DAY, -5, GETDATE())),
(3, 4, 3, 'C', DATEADD(DAY, -15, GETDATE()));

SELECT * FROM Grades ORDER BY GradeDate DESC


--mer dummydata

-- Fler elever
INSERT INTO Students (FirstName, LastName, DateOfBirth, Gender, SocialSecurityNo, ClassId) VALUES
('Erik',    'Johansson',  '2009-01-15', 'Man',    '200901151234', 1),
('Sofia',   'Larsson',    '2008-11-22', 'Kvinna', '200811221234', 2),
('Magnus',  'Pettersson', '2009-05-08', 'Man',    '200905081234', 3),
('Linnea',  'Gustafsson', '2008-09-30', 'Kvinna', '200809301234', 4),
('Oscar',   'Eriksson',   '2010-02-14', 'Man',    '201002141234', 5),
('Maja',    'Månsson',    '2009-07-19', 'Kvinna', '200907191234', 6),
('Isak',    'Lindgren',   '2008-03-25', 'Man',    '200803251234', 1),
('Alva',    'Berggren',   '2010-08-11', 'Kvinna', '201008111234', 2),
('Victor',  'Sjöberg',    '2009-12-03', 'Man',    '200912031234', 3),
('Nellie',  'Håkansson',  '2008-06-27', 'Kvinna', '200806271234', 4);

-- Fler betyg (äldre)
INSERT INTO Grades (StudentId, StaffId, SubjectId, Grade, GradeDate) VALUES
(1,  2, 2, 'B', '2025-11-10'),
(2,  3, 3, 'A', '2025-10-15'),
(3,  4, 4, 'C', '2025-09-20'),
(4,  5, 5, 'B', '2025-08-12'),
(5,  6, 6, 'A', '2025-11-30'),
(6,  2, 1, 'D', '2025-10-05'),
(7,  3, 2, 'C', '2025-09-15'),
(8,  4, 3, 'B', '2025-08-20'),
(9,  5, 4, 'A', '2025-11-25'),
(10, 6, 5, 'C', '2025-10-30');

-- Fler betyg (senaste månaden)
INSERT INTO Grades (StudentId, StaffId, SubjectId, Grade, GradeDate) VALUES
(4,  5, 1, 'A', DATEADD(DAY, -3,  GETDATE())),
(5,  6, 2, 'B', DATEADD(DAY, -7,  GETDATE())),
(6,  2, 3, 'C', DATEADD(DAY, -10, GETDATE())),
(7,  3, 4, 'A', DATEADD(DAY, -20, GETDATE())),
(8,  4, 5, 'B', DATEADD(DAY, -25, GETDATE()));