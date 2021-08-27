CREATE TABLE Students(
	StudentID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);		

CREATE TABLE Exams(
	ExamID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);		

CREATE TABLE StudentsExams(
	StudentID INT NOT NULL REFERENCES Students(StudentID),
	ExamID INT NOT NULL REFERENCES Exams(ExamID)
	CONSTRAINT PK_StudentID_ExamID
	PRIMARY KEY(StudentID, ExamID)
);

INSERT INTO Students([Name])
VALUES ('Mila'), ('Toni'), ('Ron')

INSERT INTO Exams([Name])
VALUES ('SpringMVC'), ('Neo4j'), ('Oracle 11g')

INSERT INTO StudentsExams(StudentID, ExamID)
VALUES (1, 1), (1, 2), (2, 1), (3, 3), (2, 2), (2, 3)

SELECT st.[Name] AS StudentName, ex.[Name] AS ExamName
FROM StudentsExams AS stEx
JOIN Students AS st ON st.StudentID = stEx.StudentID
JOIN Exams AS ex ON ex.ExamID = stEx.StudentID