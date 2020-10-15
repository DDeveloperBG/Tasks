-- CREATE DATABASE Users AND IN IT CREATE TABLE Useres AND POPULATE IT WITH 5 EXAMPLE ROWS, task 8

USE [master]

CREATE DATABASE Users

USE Users

CREATE TABLE Users
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture IMAGE,
	LastLoginTime TIME,
	IsDeleted BINARY
)

INSERT INTO Users (Username, [Password])
VALUES ('User 1', 123), 
	('User 2', 456), 
	('User 3', 789), 
	('User 4', 101112), 
	('User 5', 131415)